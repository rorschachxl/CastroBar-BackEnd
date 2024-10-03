using CASTROBAR_API.Services;
using Microsoft.AspNetCore.Mvc;
using CASTROBAR_API.Dtos;


namespace CASTROBAR_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("Registrar")]
        public async Task<IActionResult> RegistrarUsuario([FromBody] UsuarioRequestDto usuario)
        {
            try
            {
                await _usuarioService.RegistroUsuario(usuario);
                return Ok(new { mensaje = "Usuario registrado exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"ERROR: {ex.Message}");
            }
        }

        [HttpPost("Actualizar")]
        public async Task<IActionResult> ActualizarUSuario([FromForm] string nombre, [FromForm] string apellido, [FromForm] string telefono, [FromForm] string correo, [FromForm] string numeroDocumento)
        {
            try
            {
                await _usuarioService.ActualizarUSuario(numeroDocumento, nombre, apellido, telefono, correo);
                return Ok(new { mensaje = "Usuario actualizado con exito" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error" + ex.Message);
            }
        }
    }
}
