using CASTROBAR_API.Repositories;
using CASTROBAR_API.Services;
using Microsoft.AspNetCore.Mvc;
using CASTROBAR_API.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Swashbuckle.AspNetCore.Annotations;


namespace CASTROBAR_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecetaController : ControllerBase
    {
        private readonly IRecetaRepository _recetaRepository;
        private readonly RecetaService _recetaService;

        public RecetaController(IRecetaRepository recetaRepository, RecetaService recetaService)
        {
            _recetaRepository = recetaRepository;
            _recetaService = recetaService;
        }
        [HttpPost]
        public async Task<IActionResult> CrearReceta([FromBody] RecetaRequestDto recetaDto)
        {
            try
            {
                // Llama al servicio y captura el ID de la receta
                int recetaId = await _recetaService.AgregarReceta(recetaDto);
                return Ok(new { mensaje = "Receta creada exitosamente", idReceta = recetaId });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        [HttpGet]
        public async Task<IActionResult> ObtenerRecetas()
        {
            try
            {
                var recetas = await _recetaService.ObtenerTodasLasRecetas();
                return Ok(recetas);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarReceta(int id)
        {
            try
            {
                await _recetaService.EliminarRecetaYActualizarProductos(id);
                return Ok("Receta eliminada y productos actualizados a NULL.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
