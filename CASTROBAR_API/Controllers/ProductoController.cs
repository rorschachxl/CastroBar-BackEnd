using CASTROBAR_API.Models;
using CASTROBAR_API.Repositories;
using CASTROBAR_API.Services;
using Microsoft.AspNetCore.Mvc;
using CASTROBAR_API.Dtos;

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
        [HttpGet("{nombre}")]
        public async Task<ActionResult<List<Producto>>> GetProducto(string nombre)
        {
            var productos = await _productoRepository.ObtenerProductosPorNombreAsync(nombre);
            if (productos == null || !productos.Any())
            {
                return NotFound(); 
            }

            return Ok(productos); 
        }

        [HttpPost]
        public async Task<ActionResult> CreateProducto(ProductoRequestDto productoRequestDto)
        {
            var producto = _productoService.ConvertirEnEntidad(productoRequestDto);
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