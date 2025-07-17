using Microsoft.AspNetCore.Mvc;
using RunGym.Models; // Ajusta el namespace según tu proyecto
using RunGym.Repositorios.Interfaces;
using Microsoft.AspNetCore.Http;

namespace RunGym.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoDietaController : ControllerBase
    {
        private readonly ITipoDietaRepository _tipoDietaRepository;

        public TipoDietaController(ITipoDietaRepository tipoDietaRepository)
        {
            _tipoDietaRepository = tipoDietaRepository;
        }

        [HttpGet("GetTipoDietas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTipoDietas()
        {
            try
            {
                var response = await _tipoDietaRepository.GetAllAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetTipoDieta/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTipoDieta(int id)
        {
            try
            {
                var response = await _tipoDietaRepository.GetByIdAsync(id);
                if (response == null)
                    return NotFound($"TipoDieta con id {id} no encontrada.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("PostTipoDieta")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostTipoDieta([FromBody] TipoDieta tipoDieta)
        {
            try
            {
                var created = await _tipoDietaRepository.CreateAsync(tipoDieta);
                return CreatedAtAction(nameof(GetTipoDieta), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("PutTipoDieta/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutTipoDieta(int id, [FromBody] TipoDieta tipoDieta)
        {
            try
            {
                if (id != tipoDieta.Id)
                    return BadRequest("El id del tipo de dieta no coincide con el id del cuerpo.");

                var existing = await _tipoDietaRepository.GetByIdAsync(id);
                if (existing == null)
                    return NotFound($"TipoDieta con id {id} no encontrada.");

                await _tipoDietaRepository.UpdateAsync(tipoDieta);
                return Ok("TipoDieta actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteTipoDieta/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTipoDieta(int id)
        {
            try
            {
                var existing = await _tipoDietaRepository.GetByIdAsync(id);
                if (existing == null)
                    return NotFound($"TipoDieta con id {id} no encontrada.");

                await _tipoDietaRepository.DeleteAsync(id);
                return Ok("TipoDieta eliminada con éxito.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}