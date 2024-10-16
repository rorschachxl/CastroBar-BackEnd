using CASTROBAR_API.Dtos;
using CASTROBAR_API.Models;
using CASTROBAR_API.Repositories;
using CASTROBAR_API.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using System.Text;

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
        public async Task<int> CrearUser(UsuarioDto user)
        {
            try
            {
                if (user != null)
                {
                    var numDoc = await _repo.obtenerUsuario(user.NumeroDocumento);

                    if (numDoc == null)
                    {

                        var userNew = new Usuario
                        {
                            NumeroDocumento = user.NumeroDocumento,
                            Nombre = user.Nombre,
                            Apellido = user.Apellido,
                            Contraseña = user.Contraseña,
                            Correo = user.Correo,
                            Telefono = user.Telefono,
                            RolIdRol = user.Rol,
                            DocumentoIdDocumento=1,
                            EstadoIdEstado = 1
                        };
                        userNew.Contraseña = TokenAndEncript.HashPass(user.Contraseña);
                        await _repo.CrearUsuario(userNew);
                        return 1;
                    }
                    else { return 2; }

                }
                else { return 3; }
            }
            catch (Exception )
            {
                return 4;
            }
        }
        public async Task<int> UpdateUsers(UpdateUserDto user, string id)
        {
            try
            {
                if (user != null || id != null)
                {
                    Usuario userA = await _repo.obtenerUsuario(id);

                    user.Contraseña = TokenAndEncript.HashPass(user.Contraseña);
                    await _repo.UpdateUser(userA,user);
                    return 1;
                    
                    
                }
                else
                {
                    return 3;
                }

            }
            catch { return 4; }
        }
        public async Task<List<Usuario>> GetUserAll()
        {
            var users = await _repo.GetAllUsers();
            return users;
        }
        public async Task<Usuario> GetUserByID(string id)
        {
            var user = await _repo.GetUserById(id);
            return user;
        }
        public async Task<bool> deleteUser(string id)
        {
            var userDelete = await _repo.GetUserById(id);
            if (id == null || userDelete == null) //verify id or user not null
            {
                return false;
            }
            if (userDelete != null)
            {
                await _repo.DeleteUser(userDelete); // delete user of the database 
            }
            return true;
        }
    }
}
