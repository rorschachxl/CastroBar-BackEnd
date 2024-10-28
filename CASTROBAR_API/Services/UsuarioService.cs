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
        private readonly EmailUtility _mail;

        public UsuarioService (UsuariosRepository repo, TokenAndEncript token, EmailUtility mail )
        {
            _mail = mail;
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
        public async Task<int> RecoveryPass(string mail)
        {
            var user = await _repo.GetUserByMail(mail);
            if (user == null)
            {
                return 0;
            }
            else
            {
                var tokenPass = _token.GenerarToken(mail, Convert.ToString(user.NumeroDocumento));
                string url = "https://h1p4g6w6-5173.use.devtunnels.ms/ResetPassword/?token=" + tokenPass + "/?id=" + user.NumeroDocumento;
                var emailRequest = new EmailDto
                {
                    Addressee = user.Correo,
                    Affair = "Recuperar Contraseña Castro Bar",
                    Contain = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"><meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\"><meta name=\"x-apple-disable-message-reformatting\"><meta content=\"telephone=no\" name=\"format-detection\"><title>Recuperación de Contraseña - CastroBar</title><!--[if (mso 16)]><style type=\"text/css\">a {text-decoration: none;}</style><![endif]--><!--[if gte mso 9]><style>sup { font-size: 100% !important; }</style><![endif]--><!--[if gte mso 9]><xml><o:OfficeDocumentSettings><o:AllowPNG></o:AllowPNG><o:PixelsPerInch>96</o:PixelsPerInch></o:OfficeDocumentSettings></xml><![endif]--><style>body { margin: 0; padding: 0; }.es-wrapper { width: 100%; background-color: #dde3ca; }.es-header-body { background-color: #3d3c3b; }.es-content-body { background-color: #dde3ca; }.es-footer-body { background-color: #3d3c3b; }.es-button { color: #dde3ca; background-color: #3d3c3b; text-decoration: none; font-size: 20px; font-weight: bold; padding: 10px 20px; border-radius: 5px; }.es-button:hover,:focus { text-decoration: none; }h1 { color: #dde3ca; font-family: Arial, sans-serif; }h2 { color: #1c1c1c; font-family: Arial, sans-serif; text-align: center; }</style></head><body><div class=\"es-wrapper\"><table class=\"es-header\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" align=\"center\"><tr><td align=\"center\"><table class=\"es-header-body\" width=\"600\" cellspacing=\"0\" cellpadding=\"0\" bgcolor=\"#d62828\" align=\"center\"><tr><td align=\"center\" style=\"padding: 20px;\"><img src=\"https://yourlogo.com/logo.png\" alt=\"CastroBar Logo\" width=\"100\" style=\"display: block;\"><h1>CastroBar</h1></td></tr></table></td></tr></table><table class=\"es-content\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" align=\"center\"><tr><td align=\"center\"><table class=\"es-content-body\" width=\"600\" cellspacing=\"0\" cellpadding=\"0\" bgcolor=\"#ffffff\" align=\"center\"><tr><td align=\"center\" style=\"padding: 20px;\"><h2>¿Olvidaste tu contraseña?</h2><p>Si solicitaste recuperar tu contraseña, haz clic en el botón a continuación. Si no solicitaste este cambio, ignora este correo.</p><a href=\""+url+"\" class=\"es-button\" target=\"_blank\">Recuperar Contraseña</a></td></tr></table></td></tr></table><table class=\"es-footer\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" align=\"center\"><tr><td align=\"center\"><table class=\"es-footer-body\" width=\"600\" cellspacing=\"0\" cellpadding=\"0\" bgcolor=\"#d62828\" align=\"center\"><tr><td align=\"center\" style=\"padding: 20px; color: #dde3ca; font-family: Arial, sans-serif; font-size: 12px;\"><p>&copy; 2024 CastroBar. Todos los derechos reservados.</p></td></tr></table></td></tr></table></div></body></html>",
                };
                _mail.SendEmail(emailRequest);
                return 1;
            }
        }
        public async Task<int> ResetPass(int? id, string pass)
        {
            var userA = await _repo.GetUserByID(id);
            userA.Contraseña = TokenAndEncript.HashPass(pass);
            await _repo.UpdateUserPass(userA);
            return 1;
        }
        public async Task<Usuario> GetUserById(int? id)
        {
            var Id =Convert.ToString(id);
            return await _repo.GetUserById(Id);
        }
    }
}
