using CASTROBAR_API.Dtos;
using CASTROBAR_API.Models;
using CASTROBAR_API.Repositories;
using CASTROBAR_API.Utilities;
using Microsoft.AspNetCore.Identity;

namespace CASTROBAR_API.Services
{
    public class UsuarioService
    {
        private readonly UsuariosRepository _repo;
        private readonly TokenAndEncript _token;

        public UsuarioService (UsuariosRepository repo, TokenAndEncript token)
        {
            _repo = repo;
            _token = token;
        }
        
        public async Task<Usuario> Login(LoginDto user)
        {
            var usuario = new Usuario();
            try
            {
                if (user != null)
                {
                    var userAct = await _repo.GetUser(user);
                    if (userAct != null)
                    {
                        var PasswordHash = _token.VerifyPassword(user.contraseña, userAct.Contraseña);
                        if (PasswordHash == true)
                        {
                            return userAct;
                        }
                        else { return null; }
                    }
                    else
                    {
                        return null;
                    }  
                }
                else { return null; }

            }catch(Exception ex)
            {
                usuario = null;
                return usuario;
            }
        }
    }
}
