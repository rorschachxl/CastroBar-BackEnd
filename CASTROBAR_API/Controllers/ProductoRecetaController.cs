using CASTROBAR_API.Dtos;
using CASTROBAR_API.Repositories;
using CASTROBAR_API.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CASTROBAR_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoRecetaController : ControllerBase
    {
        private readonly ProductoRecetaService _productoRecetaService;

        public ProductoRecetaController( ProductoRecetaService productoRecetaService)
        {
            
            _productoRecetaService = productoRecetaService;
        }
        [HttpPost("{id}")]
        [SwaggerOperation(Summary = "Agregar productos a la receta")]
        public async Task<IActionResult> AgregarProductosReceta([FromBody] List<ProductoRecetaRequestDto> recetaDto, int id)
        {
            try
            {
                // Llama al servicio y captura el ID de la receta
                await _productoRecetaService.AgregaProductosAReceta(id, recetaDto);
                return Ok(new { mensaje = "Productos agregados exitosamente" });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        [HttpGet("{idReceta}")]
        public async Task<IActionResult> ObtenerProductosPorReceta(int idReceta)
        {
            try
            {
                var productosReceta = await _productoRecetaService.ObtenerProductosPorReceta(idReceta);
                return Ok(productosReceta);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProductoReceta(int id)
        {
            try
            {
                bool eliminado = await _productoRecetaService.EliminarProductoRecetaPorId(id);
                if (eliminado)
                {
                    return Ok("Producto de la receta eliminado exitosamente");
                }
                else
                {
                    return NotFound("Producto de la receta no encontrado");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
