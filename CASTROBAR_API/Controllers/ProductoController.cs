using CASTROBAR_API.Models;
using CASTROBAR_API.Repositories;
using CASTROBAR_API.Services;
using Microsoft.AspNetCore.Mvc;
using CASTROBAR_API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

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
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var productos = await _productoService.ObtenerProductos();
            if (productos == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(productos);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateProducto(ProductoRequestDto productoRequestDto)
        {
            int id= await _productoService.CrearProducto(productoRequestDto);

            if (id > 0) {
                return Ok(new { id });
            } else {
                return BadRequest("no se puedo agregar el producto");
                    }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProducto(int id, ProductoRequestDto productoRequestDto)
        {
            int resultado = await _productoService.ActualizarProducto(id, productoRequestDto);

            if (resultado == 1)
            {
                return Ok(new { Message = "El producto se ha actualizado correctamente" });
            }
            else
            {
                return BadRequest(new object[] { resultado });
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProducto(int id)
        {
            await _productoRepository.BorrarProductoAsync(id);
            return NoContent();
        }
    }
}