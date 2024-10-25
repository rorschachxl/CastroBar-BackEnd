using CASTROBAR_API.Dtos;
using CASTROBAR_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CASTROBAR_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesaController : ControllerBase
    {
        private readonly MesaService _mesaService;

        public MesaController(MesaService mesaService)
        {
            _mesaService = mesaService;
        }

        [HttpGet("ObtenerMesas")]
        public async Task<ActionResult<List<MesaResponseDto>>> ObtenerMesas()
        {
            var mesas = await _mesaService.ObtenerTodasLasMesasAsync();
            return Ok(mesas);
        }

        [HttpPost("AgregarMesa")]
        public async Task<ActionResult<int>> AgregarMesa([FromBody] MesaRequestDto mesaDto)
        {
            if (mesaDto == null)
            {
                return BadRequest("Los datos de la mesa no puede ser nulo.");
            }

            int nuevoIdMesa = await _mesaService.AgregarMesa(mesaDto);

            return CreatedAtAction(nameof(AgregarMesa), new { id = nuevoIdMesa }, nuevoIdMesa);
        }

        [HttpPut("EditarMesa/{numeroMesa}")]
        public async Task<IActionResult> EditarMesa(int numeroMesa, [FromBody] MesaRequestDto mesaDto)
        {
            if (mesaDto == null)
            {
                return BadRequest("Los datos de la mesa son requeridos.");
            }

            var resultado = await _mesaService.EditarMesaAsync(numeroMesa, mesaDto);

            if (!resultado)
            {
                return NotFound($"La mesa con el número {numeroMesa} no fue encontrada.");
            }

            return NoContent(); 
        }
    }
}
