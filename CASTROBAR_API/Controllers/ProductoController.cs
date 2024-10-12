using CASTROBAR_API.Models;
using CASTROBAR_API.Repositories;
using CASTROBAR_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CASTROBAR_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoRepository _productoRepository;
        private readonly ProductoService _productoService;

        public ProductoController(IProductoRepository productoRepository, ProductoService productoService)
        {
            _productoRepository = productoRepository;
            _productoService = productoService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productos = await _productoRepository.ObtenerTodosProductos();
            var productosDto = productos.Select(p => _productoService.convertirADto(p));
            return Ok(productosDto);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _productoRepository.ObtenerProductoIdAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateProducto(Producto producto)
        {
            await _productoRepository.AgregarProductoAsync(producto);
            return CreatedAtAction(nameof(GetProducto), new { id = producto.IdProducto }, producto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProducto(int id, Producto producto)
        {
            if (id != producto.IdProducto)
            {
                return BadRequest();
            }

            await _productoRepository.ActualizarProductoAsync(producto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProducto(int id)
        {
            await _productoRepository.BorrarProductoAsync(id);
            return NoContent();
        }
    }
}