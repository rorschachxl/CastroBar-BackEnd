using CASTROBAR_API.Dtos;
using CASTROBAR_API.DTOs;
using CASTROBAR_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CASTROBAR_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubcategoriaController : ControllerBase
    {
        private readonly SubcategoriaService _subcategoriaService;

        public SubcategoriaController(SubcategoriaService subcategoriaService)
        {
            _subcategoriaService = subcategoriaService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerSubCategorias()
        {
            var estados = await _subcategoriaService.ObtenerSubcategorias();
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
        public async Task<ActionResult> CrearSubategoria(SubcategoriaRequestDto subcategoriaRequestDto)
        {
            {
                int id = await _subcategoriaService.CrearSubcategoria(subcategoriaRequestDto);

                if (id > 0)
                {
                    return Ok(new { id });
                }
                else
                {
                    return BadRequest("no se puedo agregar la subcategoria");
                }
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarEstado(int id, SubcategoriaRequestDto subcategoriaRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int resultado = await _subcategoriaService.ActualizarSubcategoria(id, subcategoriaRequestDto);

            if (resultado == id)
            {
                return Ok(new { Message = "La subcategoria se ha actualizado correctamente" });
            }
            else
            {
                return BadRequest(new object[] { resultado });
            }

        }
    }
}
