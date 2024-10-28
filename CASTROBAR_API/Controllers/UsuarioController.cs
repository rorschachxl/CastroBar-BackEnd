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
        [Authorize(Roles = "2")]
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
        [Authorize(Roles = "2")]
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
        [Authorize(Roles = "2")]
        [Route("/UpdateUser/{id}")]
        public async Task<IActionResult> PutUsuarioModel(string id, UpdateUserDto usuarioModel)
        {
            var Res = await _service.UpdateUsers(usuarioModel, id);
            if (Res == 1)
            {
                var ress = new JsonFile
                {
                    Message = "Usuario Actualizado Correctamente"
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
        [Authorize(Roles = "2")]
        [Route("RegistrarUsuario")]
        public async Task<IActionResult> CrearUsuario(UsuarioDto usuario)
        {
            var Res = await _service.CrearUser(usuario);
            if (Res == 1)
            {
                var ress = new JsonFile
                {
                    Message = "Bienvenido al Sistema"
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
        [Authorize(Roles = "2")]
        [Route("/DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUsuarioModel(string id)
        {
            if (await _service.deleteUser(id))
            {
                return Ok("Usuario Eliminado Correctamente");
            }
            else
            {
                return NotFound("Algo fallo al eliminar el usuario");
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
                    Message = "Bienvenido al sistema",
                    User = user.Nombre
                };
                var result = JsonSerializer.Serialize(userToken);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }
        [HttpPost]
        [Route("PasswordRecovery")]
        public async Task<IActionResult> RecoveryPass([FromBody] RecoveryPassDto email)
        {
            try
            {
                await _service.RecoveryPass(email.Data);
                return Ok("Correo enviado Revisa tu correo electronico");
            }
            catch (System.Exception ex)
            {
                return BadRequest("El correo no existe en el sistema" + ex);
                throw;
            }
        }
        [HttpPut]
        [Authorize]
        [Route("PasswordReset/{id}")]
        public async Task<IActionResult> PasswordReset([FromBody] RecoveryPassDto rest, int? id)
        {
            if (await _service.ResetPass(id, rest.Data) == 1)
            {
                return Ok("Contraseña Cambiada Correctamente");
            }
            else
            {
                return BadRequest("Error al Actualizar");
            }
        }
        private bool UsuarioModelExists(string id)
        {
            return _context.Usuarios.Any(e => e.NumeroDocumento == id);
        }

    }
}
