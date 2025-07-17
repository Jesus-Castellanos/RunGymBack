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
            try
            {
                usuario.Contraseña = RunGym.Utils.Encriptador.Encriptar(usuario.Contraseña);
                var response = await _usuarios.PostUsuarios(usuario);

                if (response == true)
                {
                    return Ok(new
                    {
                        Exito = true,
                        Mensaje = "El usuario fue insertado correctamente."
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        Exito = false,
                        Mensaje = "No se pudo insertar el usuario.",
                        Detalle = response
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Exito = false,
                    Mensaje = "Error al insertar el usuario.",
                    Detalle = ex.Message
                });
            }
        }

        [HttpPut("PutUsuario")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutUsuario([FromBody] Usuarios usuario)
        {
            try
            {
                var resultado = await _usuarios.PutUsuarios(usuario);

                if (resultado)
                {
                    return Ok(new
                    {
                        Exito = true,
                        Mensaje = "Usuario actualizado correctamente."
                    });
                }
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