using CASTROBAR_API.Repositories;
using CASTROBAR_API.Services;
using Microsoft.AspNetCore.Mvc;
using CASTROBAR_API.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;


namespace CASTROBAR_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecetaController : ControllerBase
    {
        private readonly IRecetaRepository _recetaRepository;
        private readonly RecetaService _recetaService;

        public RecetaController(IRecetaRepository recetaRepository, RecetaService recetaService)
        {
            _recetaRepository = recetaRepository;
            _recetaService = recetaService;
        }
        [HttpPost]
        public async Task<IActionResult> CrearRecetaConProductos([FromBody] RecetaDto recetaDto)
        {
            try
            {
                await _recetaService.AgregarRecetaConProductos(recetaDto);
                return Ok("Receta creada exitosamente");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
