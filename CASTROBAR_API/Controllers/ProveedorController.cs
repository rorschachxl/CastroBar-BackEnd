using CASTROBAR_API.Dtos;
using CASTROBAR_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CASTROBAR_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly ProveedorService _proveedorService;

        public ProveedorController(ProveedorService proveedorService)
        {
            _proveedorService = proveedorService;
        }

        [HttpPost]
        public async Task<IActionResult> AgregarProveedor([FromBody] ProveedorRequestDto proveedorRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int nuevoProveedorId = await _proveedorService.AgregarProveedorAsync(proveedorRequest);
            return CreatedAtAction(nameof(AgregarProveedor), new { id = nuevoProveedorId }, proveedorRequest);
        }
        [HttpGet]
        public async Task<IActionResult> ObtenerTodosLosProveedores()
        {
            var proveedores = await _proveedorService.ObtenerTodosLosProveedoresAsync();
            return Ok(proveedores);
        }

        [HttpPut("{idProveedor}")]
        public async Task<IActionResult> EditarProveedor(int idProveedor, [FromBody] ProveedorRequestDto proveedorDto)
        {
            try
            {
                await _proveedorService.EditarProveedorAsync(idProveedor, proveedorDto);
                return Ok(new { mensaje = "Proveedor actualizado correctamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
