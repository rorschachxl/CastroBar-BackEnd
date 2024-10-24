using CASTROBAR_API.Models;
using CASTROBAR_API.Repositories;
using CASTROBAR_API.Services;
using Microsoft.AspNetCore.Mvc;
using CASTROBAR_API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using CASTROBAR_API.Utilities;

namespace CASTROBAR_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoRepository _productoRepository;
        private readonly ProductoService _productoService;
        private readonly TokenAndEncript _tokenAndEncript;

        public ProductoController(IProductoRepository productoRepository, ProductoService productoService, TokenAndEncript tokenAndEncript)
        {
            _productoRepository = productoRepository;
            _productoService = productoService;
            _tokenAndEncript = tokenAndEncript;
        }
        [HttpGet]
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
        [Authorize]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            try
            {
                // Obtiene el token del encabezado sin el prefijo "Bearer"
                var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);

                // Llama al servicio para eliminar el producto
                int resultado = await _productoService.EliminarProducto(id, token);

                // Si la eliminación es exitosa
                if (resultado == 200)
                {
                    return Ok(new { message = "Producto eliminado exitosamente." });
                }
                else
                {
                    // Maneja si el producto no fue encontrado o no se pudo eliminar
                    return NotFound(new { message = "Producto no encontrado o no pudo eliminarse." });
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Devuelve un error 401 si la autenticación falló
                return Unauthorized(new { message = "Acceso no autorizado." });
            }
            catch (Exception ex)
            {
                // Devuelve un error 500 si ocurrió una excepción no controlada
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error interno del servidor.", details = ex.Message });
            }
        }
    }
}