using RunGym.Repositorios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using RunGym.Utils;
using RunGym.Models;

namespace RunGym.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosRepository _usuarios;

        public UsuariosController(IUsuariosRepository usuario)
        {
            _usuarios = usuario;
        }

        [HttpGet("GetUsuarios")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _usuarios.GetUsuarios();
            return Ok(usuarios);
        }

        [HttpPost("PostUsuarios")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostUsuarios([FromBody] Usuarios usuario)
        {
            // 💡 1. Validación de entrada (mínima, puedes mejorarla)
            if (usuario == null || string.IsNullOrWhiteSpace(usuario.Contraseña))
            {
                return BadRequest(new { Exito = false, Mensaje = "La contraseña es requerida." });
            }

            try
            {
                // 🔑 2. APLICAR ARGON2: Limpiar y Hashear la contraseña antes de guardar.
                // Esta es la única línea que crea el hash que se guardará en la DB.
                string passwordPlano = usuario.Contraseña.Trim();
                usuario.Contraseña = Argon2Hasher.Hashear(passwordPlano);

                var response = await _usuarios.PostUsuarios(usuario);

                if (response == true)
                {
                    return Created(string.Empty, new // 💡 Usar Created() para 201 Created
                    {
                        Exito = true,
                        Mensaje = "El usuario fue insertado correctamente."
                    });
                }
                else
                {
                    // Asumiendo que 'PostUsuarios' retorna un bool, si es false indica fallo en la DB.
                    return BadRequest(new
                    {
                        Exito = false,
                        Mensaje = "No se pudo insertar el usuario, posiblemente el correo ya existe."
                    });
                }
            }
            catch (Exception ex)
            {
                // 💡 Usar Status500InternalServerError para errores de servidor no previstos
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Exito = false,
                    Mensaje = "Error interno del servidor al insertar el usuario.",
                    Detalle = ex.Message
                });
            }
        }

        // --- Otros métodos (PUT y DELETE están bien, pero se aplica una mejora de limpieza) ---

        [HttpPut("PutUsuario")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutUsuario([FromBody] Usuarios usuario)
        {
            try
            {
                // 💡 NOTA IMPORTANTE: Si este método permite actualizar la contraseña, 
                // DEBES HASHEARLA aquí también, tal como en PostUsuarios.

                // Ejemplo de manejo de contraseña en PUT:
                /* if (!string.IsNullOrWhiteSpace(usuario.Contraseña)) 
                {
                    usuario.Contraseña = Argon2Hasher.Hashear(usuario.Contraseña.Trim());
                }
                */

                var resultado = await _usuarios.PutUsuarios(usuario);

                if (resultado)
                {
                    return Ok(new
                    {
                        Exito = true,
                        Mensaje = "Usuario actualizado correctamente."
                    });
                }
                // ... (el resto del código Put y Delete es funcional)
                else
                {
                    return NotFound(new
                    {
                        Exito = false,
                        Mensaje = "Usuario no encontrado o no se pudo actualizar."
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Exito = false,
                    Mensaje = "Error al actualizar el usuario.",
                    Detalle = ex.Message
                });
            }
        }

        [HttpDelete("DeleteUsuario/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            try
            {
                var resultado = await _usuarios.DeleteUsuarios(id);

                if (resultado)
                {
                    return Ok(new
                    {
                        Exito = true,
                        Mensaje = "Usuario eliminado correctamente."
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        Exito = false,
                        Mensaje = "Usuario no encontrado o no se pudo eliminar."
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Exito = false,
                    Mensaje = "Error al eliminar el usuario.",
                    Detalle = ex.Message
                });
            }
        }
    }
}