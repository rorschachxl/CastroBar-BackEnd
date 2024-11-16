using CASTROBAR_API.Dtos;
using CASTROBAR_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CASTROBAR_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetodoPagoController : ControllerBase
    {
        private readonly MetodoPagoService _metodoPagoService;

        public MetodoPagoController(MetodoPagoService metodoPagoService)
        {
            _metodoPagoService = metodoPagoService;
        }

        [HttpPost("AgregarMetodoPago")]
        public async Task<IActionResult> AgregarMetodoPago([FromBody] MetodoPagoRequestDto metodoPagoDto)
        {
            if (metodoPagoDto == null)
            {
                return BadRequest("El método de pago no puede ser nulo.");
            }

            try
            {
                int idMetodoPago = await _metodoPagoService.AgregarMetodoPagoAsync(metodoPagoDto);
                return CreatedAtAction(nameof(AgregarMetodoPago), new { id = idMetodoPago }, metodoPagoDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al crear el método de pago: " + ex.Message);
            }
        }
        [HttpGet("ObtenerMetodosPago")]
        public async Task<ActionResult<List<MetodoPagoResponseDto>>> ObtenerMetodosPago()
        {
            var metodosPago = await _metodoPagoService.ObtenerTodosLosMetodosPagoAsync();

            if (metodosPago == null || metodosPago.Count == 0)
            {
                return NotFound("No se encontraron métodos de pago.");
            }

            return Ok(metodosPago);
        }
        [HttpPut("EditarMetodoPago/{idMetodoPago}")]
        public async Task<IActionResult> EditarMetodoPago(int idMetodoPago, [FromBody] MetodoPagoRequestDto metodoPagoRequest)
        {
            if (metodoPagoRequest == null)
            {
                return BadRequest("El objeto no puede ser nulo.");
            }

            var resultado = await _metodoPagoService.EditarMetodoPagoAsync(idMetodoPago, metodoPagoRequest);

            if (resultado)
            {
                return Ok(new { mensaje = "Método de pago actualizado con éxito." });
            }
            else
            {
                return NotFound("Método de pago no encontrado."); 
            }
        }
    }
}
