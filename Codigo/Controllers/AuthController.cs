using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using RunGym.Run;
using RunGym.Models;
using RunGym.Repositorios.Interfaces;
using Newtonsoft.Json;
using RunGym.Utils;
using Isopoh.Cryptography.Blake2b;
using Microsoft.AspNetCore.Http; // Necesario para StatusCodes

namespace RunGym.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly RunGymContext _context;

        private readonly IUsuariosRepository _usuariosRepository;
        private readonly IEmailServiceReposytory _emailService;

        public AuthController(IConfiguration configuration, RunGymContext context, IUsuariosRepository usuariosRepository, IEmailServiceReposytory emailService)
        {
            _configuration = configuration;
            _context = context;
            _usuariosRepository = usuariosRepository;
            _emailService = emailService;
        }

        // --- MÉTODO CORREGIDO: LOGIN (Solo Argon2) ---
        [HttpPost("Login")]
        public IActionResult Login([FromBody] Login login)
        {
            // 💡 Validación inicial de la entrada (se agrega)
            if (login == null || string.IsNullOrWhiteSpace(login.Correo) || string.IsNullOrWhiteSpace(login.Contraseña))
            {
                return BadRequest("Invalid client request");
            }

            // 1. Obtener usuario
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Correo == login.Correo);

            if (usuario == null)
            {
                // Devolvemos Unauthorized por seguridad (no confirmamos la existencia del correo)
                return Unauthorized("Invalid email or password");
            }

            // 2. Usar la función de VERIFICACIÓN de Argon2 (la corrección ya estaba implementada)
            string passwordPlano = login.Contraseña.Trim();
            string hashAlmacenado = usuario.Contraseña;

            if (!Argon2Hasher.Verificar(passwordPlano, hashAlmacenado))
            {
                return Unauthorized("Invalid email or password");
            }

            // 3. Generación del JWT (Correcto)
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha512);

            var tokeOptions = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.Correo),
                    new Claim(ClaimTypes.Role, usuario.RolId.ToString()),
                    new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(usuario))
                },
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return Ok(new { Token = tokenString });
        }

        // --- MÉTODO CORREGIDO: RECUPERAR CONTRASEÑA (Mejora en código) ---
        [HttpPost("RecuperarContraseña")]
        public async Task<IActionResult> RecuperarContraseña([FromBody] RecuperarContraseñaDTO dto)
        {
            var usuario = await _usuariosRepository.GetUsuarioByCorreoAsync(dto.Correo);

            // 💡 Por seguridad, siempre retornamos Ok si el correo no existe para no dar pistas.
            if (usuario == null)
            {
                return Ok("Se ha enviado un código de verificación a tu correo.");
            }

            // Generar código numérico aleatorio de 6 dígitos (Mejorado para asegurar "000123")
            var random = new Random();
            var codigo = random.Next(0, 1000000).ToString("D6");

            usuario.CodigoVerificacion = codigo;
            usuario.CodigoExpira = DateTime.Now.AddMinutes(30);
            await _usuariosRepository.UpdateUsuarioAsync(usuario);

            // Contenido del correo 
            string contenidoCorreo = $@"
            <p>Has solicitado recuperar tu contraseña.</p>
            <p>Tu código de verificación es: <strong>{codigo}</strong></p>
            <p>Este código expirará en 30 minutos.</p>";

            await _emailService.EnviarCorreoAsync(usuario.Correo, "Código de Verificación - Recuperación de Contraseña", contenidoCorreo);

            return Ok("Se ha enviado un código de verificación a tu correo.");
        }

        [HttpPost("VerificarCodigo")]
        public async Task<IActionResult> VerificarCodigo([FromBody] VerificarCodigoDTO dto)
        {
            var usuario = await _usuariosRepository.GetUsuarioByCorreoAsync(dto.Correo);

            if (usuario == null || usuario.CodigoVerificacion != dto.Codigo || usuario.CodigoExpira < DateTime.Now)
            {
                return BadRequest("El código no es válido o ha expirado.");
            }

            return Ok("Código verificado correctamente.");
        }

        // --- MÉTODO CORREGIDO: RESTABLECER CONTRASEÑA (Argon2 aplicado y try-catch) ---
        [HttpPost("RestablecerContraseña")]
        public async Task<IActionResult> RestablecerContraseña([FromBody] RestablecerContraseña model)
        {
            var usuario = await _usuariosRepository.GetUsuarioByCodigoAsync(model.Codigo);
            if (usuario == null || usuario.CodigoExpira < DateTime.Now)
            {
                var response = new RespuestaDTO { Exito = false, Mensaje = "El código no es válido o ha expirado." };
                return BadRequest(response);
            }

            if (model.NuevaContraseña != model.ConfirmarContraseña)
            {
                var response = new RespuestaDTO { Exito = false, Mensaje = "Las contraseñas no coinciden." };
                return BadRequest(response);
            }

            try
            {
                // 🔑 CORRECCIÓN CRÍTICA: Eliminado SHA256. Se usa Argon2Hasher.Hashear()
                string nuevaContraseñaPlana = model.NuevaContraseña.Trim();
                usuario.Contraseña = Argon2Hasher.Hashear(nuevaContraseñaPlana);

                // Limpiar el código de verificación
                usuario.CodigoVerificacion = null;
                usuario.CodigoExpira = null;

                await _usuariosRepository.UpdateUsuarioAsync(usuario);

                var successResponse = new RespuestaDTO { Exito = true, Mensaje = "Contraseña actualizada exitosamente." };
                return Ok(successResponse);
            }
            catch (Exception)
            {
                // Manejo de error del servidor
                var errorResponse = new RespuestaDTO { Exito = false, Mensaje = "Error interno del servidor al actualizar la contraseña." };
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }
    }
    public class Login
    {
        public string Correo { get; set; }
        public string Contraseña { get; set; }
    }
}