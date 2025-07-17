using RunGym.Repositorios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RunGym.Models;

[ApiController]
[Route("api/[controller]")]
public class DietaController : ControllerBase
{
    private readonly IDietaRepository _dieta;

    public DietaController(IDietaRepository dieta)
    {
        _dieta = dieta;
    }

    [HttpGet("GetDieta")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetDieta()
    {
        var response = await _dieta.GetDieta();
        return Ok(response);
    }
    
    [HttpGet("GetTipoDieta/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetTipoDieta(int id)
    {
        var dieta = await _dieta.GetTipoDieta(id);
        return Ok(dieta);
    }

    [HttpPost("PostDieta")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostDieta([FromBody] Dieta dieta)
    {
        try
        {
            var response = await _dieta.PostDieta(dieta);
            if (response == true)
                return Ok("Categoria Insertada correctamente");
            else
                return BadRequest(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("PutDieta/{id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutDieta(int id, [FromBody] Dieta dieta)
    {
        try
        {
            var response = await _dieta.PutDieta(dieta);
            if (response)
                return Ok("Categoria actualizado correctamente.");
            else
                return NotFound("Categoria no encontrado.");
        }
        catch (Exception ex)
        {
            var error = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            return BadRequest($"Error: {error}");
        }
    }

    [HttpDelete("DeleteDieta/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteDieta(int id)
    {
        try
        {
            var response = await _dieta.DeleteDieta(id);

            if (response)
                return Ok("Categoría eliminada con éxito.");
            else
                return NotFound("La categoría no fue encontrada.");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
