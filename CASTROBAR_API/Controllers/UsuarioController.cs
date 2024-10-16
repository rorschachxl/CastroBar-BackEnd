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
using Microsoft.AspNetCore.Authorization;

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


        // GET: api/Usuario/5
        [HttpGet]
        [Route("/GetAllUsers")]
        public async Task<IActionResult> GetUsuarios()
        {
            try
            {
                var users = await _service.GetUserAll();
                if (users == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Hubo un error al consultar los datos");
                }
                else
                {
                    return Ok(users);
                }
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
                throw;
            }
        }

        [HttpGet]
        [Route("/GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            try
            {
                var user = await _service.GetUserByID(id);
                var result = JsonSerializer.Serialize(user);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex);
                throw;
            }
        }

        [HttpPut]
        [Route("/UpdateUser/{id}")]
        public async Task<IActionResult> PutUsuarioModel(string id, UpdateUserDto usuarioModel)
        {
            var Res = await _service.UpdateUsers(usuarioModel, id);
            if (Res == 1)
            {
                var ress = new JsonFile
                {
                    Message = "Usuario Actualizado correctamente"
                };
                var result = JsonSerializer.Serialize(ress);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            else if (Res == 2)
            {
                var ress = new JsonFile
                {
                    Message = "El usuario no existe en el sistema"
                };
                var result = JsonSerializer.Serialize(ress);
                return StatusCode(StatusCodes.Status400BadRequest, result);
            }
            else if (Res == 3)
            {
                var ress = new JsonFile
                {
                    Message = "El usuario o id no puede ser nulo "
                };
                var result = JsonSerializer.Serialize(ress);
                return StatusCode(StatusCodes.Status400BadRequest, result);
            }
            else
            {
                var ress = new JsonFile
                {
                    Message = "Algo ocurrio en el sistema"
                };
                var result = JsonSerializer.Serialize(ress);
                return StatusCode(StatusCodes.Status400BadRequest, result);
            }
        }

        // POST: api/Usuario
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("RegistrarUsuario")]
        public async Task<IActionResult> CrearUsuario(UsuarioDto usuario)
        {
            var Res = await _service.CrearUser(usuario);
            if (Res == 1)
            {
                var ress = new JsonFile
                {
                    Message = "Bienvenido al sistema"
                };
                var result = JsonSerializer.Serialize(ress);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            else if (Res == 2)
            {
                var ress = new JsonFile
                {
                    Message = "El usuario ya existe en el sistema"
                };
                var result = JsonSerializer.Serialize(ress);
                return StatusCode(StatusCodes.Status400BadRequest, result);
            }
            else if (Res == 3)
            {
                var ress = new JsonFile
                {
                    Message = "El usuario no puede ser nulo"
                };
                var result = JsonSerializer.Serialize(ress);
                return StatusCode(StatusCodes.Status400BadRequest, result);
            }
            else {
                var ress = new JsonFile
                {
                    Message = "Algo ocurrio en el sistema"
                };
                var result = JsonSerializer.Serialize(ress);
                return StatusCode(StatusCodes.Status400BadRequest, result);
            }

        }

        // DELETE: api/Usuario/5
        [HttpDelete]
        [Route("/DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUsuarioModel(string id)
        {
            if (await _service.deleteUser(id))
            {
                return Ok("Usuario Eliminado correctamente");
            }
            else
            {
                return NotFound("algo fallo al eliminar el usuario");
            }
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
