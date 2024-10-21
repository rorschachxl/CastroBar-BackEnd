using CASTROBAR_API.Dtos;
using CASTROBAR_API.Repositories;
using CASTROBAR_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CASTROBAR_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaService _categoriaService;

        public CategoriaController( CategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ObtenerCategoria()
        {
            var estados = await _categoriaService.ObtenerCategorias();
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
        public async Task<ActionResult> CrearCategoria(CategoriaRequestDto categoriaRequestDto)
        {
            {
                int id = await _categoriaService.CrearCategoria(categoriaRequestDto);

                if (id > 0)
                {
                    return Ok(new { id });
                }
                else
                {
                    return BadRequest("no se puedo agregar la categoria");
                }
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarEstado(int id, CategoriaRequestDto categoriaRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int resultado = await _categoriaService.ActualizarCategoria(id, categoriaRequestDto);

            if (resultado == id)
            {
                return Ok(new { Message = "La categoria se ha actualizado correctamente" });
            }
            else
            {
                return BadRequest(new object[] { resultado });
            }

        }
    }
}
