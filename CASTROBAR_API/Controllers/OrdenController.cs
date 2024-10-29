using CASTROBAR_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CASTROBAR_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdenController : ControllerBase
    {
        private readonly OrdenService _ordenService;

        public OrdenController(OrdenService ordenService)
        {
            _ordenService = ordenService;
        }

        [HttpPost("CrearOrden")]
        public async Task<IActionResult> CrearOrden([FromBody] int numeroMesa)
        {
            if (numeroMesa <= 0)
            {
                return BadRequest("El número de mesa es inválido.");
            }

            int idOrden = await _ordenService.CrearOrdenAsync(numeroMesa);
            return CreatedAtAction(nameof(CrearOrden), new { id = idOrden }, new { idOrden = idOrden });
        }
    }
}
