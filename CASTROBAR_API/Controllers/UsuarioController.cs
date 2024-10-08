using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CASTROBAR_API.Dtos;
using CASTROBAR_API.Models;
using CASTROBAR_API.Repositories;
using CASTROBAR_API.Services;
using CASTROBAR_API.Utilities;
using System.Text.Json;

namespace CASTROBAR_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly DbAadd54CastrobarContext _context;
        private readonly UsuarioService _service;
        private readonly TokenAndEncript _token;
        public UsuarioController(DbAadd54CastrobarContext context, UsuarioService service, TokenAndEncript token)
        {
            _context = context;
            _service = service;
            _token = token;
        }

        // GET: api/Usuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUSUARIO()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuarioModel(int id)
        {
            var usuarioModel = await _context.Usuarios.FindAsync(id);

            if (usuarioModel == null)
            {
                return NotFound();
            }

            return usuarioModel;
        }

        // PUT: api/Usuario/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarioModel(string id, Usuario usuarioModel)
        {
            if (id != usuarioModel.NumeroDocumento)
            {
                return BadRequest();
            }

            _context.Entry(usuarioModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Usuario
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuarioModel(Usuario usuarioModel)
        {
            _context.Usuarios.Add(usuarioModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuarioModel", new { id = usuarioModel.NumeroDocumento }, usuarioModel);
        }

        // DELETE: api/Usuario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuarioModel(int id)
        {
            var usuarioModel = await _context.Usuarios.FindAsync(id);
            if (usuarioModel == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuarioModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        [Route("/IniciarSesion")]
        public async Task<IActionResult> GetiniciarSesión(LoginDto log)
        {
            var user = await _service.Login(log);
            if(user != null)
            {
                var token = _token.GenerarToken(user.NumeroDocumento, Convert.ToString(user.RolIdRol));
                var userToken = new JsonFile
                {
                    Id = Convert.ToString(user.IdUsuario),
                    Token = token,
                    Message = "Bienvenido al sistema"
                };
                var result = JsonSerializer.Serialize(userToken);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }
        private bool UsuarioModelExists(string id)
        {
            return _context.Usuarios.Any(e => e.NumeroDocumento == id);
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
