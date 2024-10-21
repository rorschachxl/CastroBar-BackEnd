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
    public class EstadoController : ControllerBase
    {
        
        private readonly EstadoService _estadoService;

        public EstadoController( EstadoService estadoService)
        {
            _estadoService = estadoService;
        }

        [HttpGet]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> ObtenerEstados()
        {
            var estados = await _estadoService.ObtenerEstados();
            if (estados == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(estados);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CrearEstado(EstadoDto estadoDto)
        {
            {
                int id = await _estadoService.CrearEstado(estadoDto);

                if (id > 0)
                {
                    return Ok(new { id });
                }
                else
                {
                    return BadRequest("no se puedo agregar el estado");
                }
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarEstado (int id, EstadoDto estadoDto)
        {
            int resultado = await _estadoService.ActualizarEstado(id, estadoDto);

            if (resultado == 1)
            {
                return Ok(new { Message = "El estado se ha actualizado correctamente" });
            }
            else
            {
                return BadRequest(new object[] { resultado });
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> BorrarEstado(int id)
        {
            await _estadoService.EliminarEstado(id);
            return NoContent();
        }
    }
}
