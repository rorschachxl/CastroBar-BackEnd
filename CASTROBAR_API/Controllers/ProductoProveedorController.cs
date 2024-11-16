using CASTROBAR_API.Dtos;
using CASTROBAR_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CASTROBAR_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoProveedorController : ControllerBase
    {
        private readonly ProductoProveedorService _productoProveedorService;

        public ProductoProveedorController(ProductoProveedorService productoProveedorService)
        {
            _productoProveedorService = productoProveedorService;
        }

        [HttpPost("AgregarProductoAlProveedor")]
        public async Task<IActionResult> AgregarProductoProveedor([FromBody] ProductoProveedorRequestDto productoProveedorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Validar el modelo
            }

            try
            {
                await _productoProveedorService.AgregarProductoProveedorAsync(productoProveedorDto);
                return Ok("Producto-Proveedor agregado correctamente.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); // Si los IDs no son válidos
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error: {ex.Message}"); // Error genérico
            }
        }
        [HttpGet("ObtenerProductosDelProveedor/{proveedorId}")]
        public async Task<IActionResult> ObtenerProductosPorProveedor(int proveedorId)
        {
            try
            {
                var productos = await _productoProveedorService.ObtenerProductosPorProveedorAsync(proveedorId);
                return Ok(productos);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones generales
                return StatusCode(500, "Ocurrió un error en el servidor.");
            }
        }
        [HttpDelete("EliminarProductoProveedor/{id}")]
        public async Task<IActionResult> EliminarProductoProveedor(int id)
        {
            var result = await _productoProveedorService.EliminarProductoProveedorAsync(id);

            if (!result)
            {
                return NotFound(); // Retorna 404 si no se encuentra el registro
            }

            return NoContent(); // Retorna 204 si la eliminación fue exitosa
        }
    }
}
