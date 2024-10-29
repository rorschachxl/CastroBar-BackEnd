using CASTROBAR_API.Dtos;
using CASTROBAR_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CASTROBAR_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoOrdenController : ControllerBase
    {
        private readonly ProductoOrdenService _productoOrdenService;

        public ProductoOrdenController(ProductoOrdenService productoOrdenService)
        {
            _productoOrdenService = productoOrdenService;
        }

        [HttpPost("AgregarProductoOrden")]
        public async Task<IActionResult> AgregarProductoOrden(ProductoOrdenRequestDto productoOrdenRequestDto)
        {
            var idProductoOrden = await _productoOrdenService.AgregarProductoOrdenAsync(productoOrdenRequestDto);
            return Ok(new { IdProductoOrden = idProductoOrden });
        }                                               

        [HttpGet("ObtenerProductosPorOrdenId/{ordenId}")]
        public async Task<IActionResult> ObtenerProductosPorOrdenId(int ordenId)
        {
            var productos = await _productoOrdenService.ObtenerProductosPorOrdenIdAsync(ordenId);
            return Ok(productos);
        }
        [HttpGet("{ordenId}/factura")]
        public async Task<IActionResult> GenerarFactura(int ordenId)
        {
            var pdfBytes = await _productoOrdenService.GenerarFacturaAsync(ordenId);
            return File(pdfBytes, "application/pdf", "factura.pdf"); // Retorna el PDF como un archivo
        }
    }
}
