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
                string url = "https://front-mave.vercel.app/ResetPassword/?token=" + tokenPass + "/?id=" + user.NumeroDocumento;
                var emailRequest = new EmailDto
                {
                    Addressee = user.Correo,
                    Affair = "Recovery Password Castro Bar",
                    Contain = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html dir=\"ltr\" xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\"><head><meta charset=\"UTF-8\"><meta content=\"width=device-width, initial-scale=1\" name=\"viewport\"><meta name=\"x-apple-disable-message-reformatting\"><meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\"><meta content=\"telephone=no\" name=\"format-detection\"><title></title><!--[if (mso 16)]><style type=\"text/css\">a {text-decoration: none;}</style><![endif]--><!--[if gte mso 9]><style>sup { font-size: 100% !important; }</style><![endif]--><!--[if gte mso 9]><xml><o:OfficeDocumentSettings><o:AllowPNG></o:AllowPNG><o:PixelsPerInch>96</o:PixelsPerInch></o:OfficeDocumentSettings></xml><![endif]--><!--[if mso]><style type=\"text/css\">ul {margin: 0 !important;}ol {margin: 0 !important;}li {margin-left: 47px !important;}</style><![endif]--></head><body class=\"body\"><div dir=\"ltr\" class=\"es-wrapper-color\"><!--[if gte mso 9]><v:background xmlns:v=\"urn:schemas-microsoft-com:vml\" fill=\"t\"><v:fill type=\"tile\" color=\"#f6f6f6\"></v:fill></v:background><![endif]--><table class=\"es-wrapper\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\"><tbody><tr><td class=\"esd-email-paddings\" valign=\"top\"><table class=\"esd-header-popover es-header\" cellspacing=\"0\" cellpadding=\"0\" align=\"center\"><tbody><tr><td class=\"esd-stripe\" align=\"center\"><table class=\"es-header-body\" width=\"600\" cellspacing=\"0\" cellpadding=\"0\" bgcolor=\"#ffffff\" align=\"center\"><tbody><tr><td class=\"es-p20t es-p20r es-p20l esd-structure\" align=\"left\" bgcolor=\"#1b5091\" style=\"background-color:#1b5091\"><!--[if mso]><table width=\"560\" cellpadding=\"0\" cellspacing=\"0\"><tr><td width=\"246\" valign=\"top\"><![endif]--><table class=\"es-left\" cellspacing=\"0\" cellpadding=\"0\" align=\"left\"><tbody><tr><td width=\"246\" class=\"esd-container-frame es-m-p20b\" align=\"left\"><table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"presentation\"><tbody><tr><td align=\"center\" class=\"esd-block-image\" style=\"font-size: 0\"><a target=\"_blank\"><img src=\"https://fiepcgl.stripocdn.email/content/guids/CABINET_28007b800008bc750ac791e848023f4fab0f12f58fa0f1925c5e5067b22cf37f/images/logo_1.png\" alt=\"\" width=\"246\"></a></td></tr></tbody></table></td></tr></tbody></table><!--[if mso]></td><td width=\"20\"></td><td width=\"294\" valign=\"top\"><![endif]--><table class=\"es-right\" cellspacing=\"0\" cellpadding=\"0\" align=\"right\"><tbody><tr><td class=\"esd-container-frame\" width=\"294\" align=\"left\"><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\"><tbody><tr><td align=\"left\" class=\"esd-block-text\"><p></p></td></tr><tr><td align=\"left\" class=\"esd-block-text es-text-7400\"><h1 style=\"color:#ffffff;font-family:tahoma,verdana,segoe,sans-serif\"><strong style=\"font-size:72px;line-height:150%\">MAVE</strong></h1><h1 style=\"color:#ffffff\"><strong style=\"font-size:24px;line-height:150%\">Mente en Armonia, &nbsp;</strong></h1><h1 style=\"color:#ffffff\"><strong><span style=\"font-size:24px;line-height:150%\" class=\"es-text-mobile-size-24\">Vida en Equilibrio.</span><span style=\"font-size:36px;line-height:150%\"></span><span class=\"es-text-mobile-size-36\"></span></strong></h1></td></tr></tbody></table></td></tr></tbody></table><!--[if mso]></td></tr></table><![endif]--></td></tr></tbody></table></td></tr></tbody></table><table class=\"es-content\" cellspacing=\"0\" cellpadding=\"0\" align=\"center\"><tbody><tr><td class=\"esd-stripe\" align=\"center\"><table class=\"es-content-body\" width=\"600\" cellspacing=\"0\" cellpadding=\"0\" bgcolor=\"#ffffff\" align=\"center\"><tbody><tr><td class=\"es-p20t es-p20r es-p20l esd-structure\" align=\"left\" bgcolor=\"#6ea1d4\" style=\"background-color:#6ea1d4\"><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\"><tbody><tr><td class=\"esd-container-frame\" width=\"560\" valign=\"top\" align=\"center\"><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\"><tbody><tr><td align=\"center\" class=\"esd-block-spacer es-p20\" style=\"font-size: 0\"><table border=\"0\" width=\"100%\" height=\"100%\" cellpadding=\"0\" cellspacing=\"0\" class=\"es-spacer\"><tbody><tr><td style=\"border-bottom: 1px solid #cccccc;; background: none; height: 1px; width: 100%; margin: 0px 0px 0px 0px\"></td></tr></tbody></table></td></tr><tr><td align=\"left\" class=\"esd-block-text\"><h2 align=\"center\" style=\"color:#1b5091;font-family:tahoma,verdana,segoe,sans-serif\"><strong>Parece que quieres recuperar tu Contraseña, si es así da clic en el botón \"Recuperar\", si no ignora este correo.</strong></h2></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table><table class=\"esd-footer-popover es-footer\" cellspacing=\"0\" cellpadding=\"0\" align=\"center\"><tbody><tr><td class=\"esd-stripe\" align=\"center\"><table class=\"es-footer-body\" width=\"600\" cellspacing=\"0\" cellpadding=\"0\" bgcolor=\"#ffffff\" align=\"center\"><tbody><tr><td class=\"esd-structure es-p20t es-p20b es-p20r es-p20l\" align=\"left\" bgcolor=\"#6ea1d4\" style=\"background-color:#6ea1d4\"><table class=\"es-right\" cellspacing=\"0\" cellpadding=\"0\" align=\"right\"><tbody><tr><td class=\"esd-container-frame\" width=\"560\" align=\"left\"><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\"><tbody><tr><td align=\"center\" class=\"esd-block-button\"><!--[if mso]><a href=" + url + " target=\"_blank\" hidden><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" esdevVmlButton href=" + url + " style=\"height:49px; v-text-anchor:middle; width:189px\" arcsize=\"50%\" strokecolor=\"#ce3375\" strokeweight=\"2px\" fillcolor=\"#1b5091\"><w:anchorlock></w:anchorlock><center style='color:#ffffff; font-family:tahoma, verdana, segoe, sans-serif; font-size:20px; font-weight:700; line-height:20px;  mso-text-raise:1px'>Recuperar</center></v:roundrect></a><![endif]--><!--[if !mso]><!-- --><span class=\"es-button-border\" style=\"background:#1b5091;border-color:#CE3375\"><a href=" + url + " class=\"es-button\" target=\"_blank\" style=\"background:#1b5091;mso-border-alt:10px solid #1b5091;font-weight:bold;font-size:26px;font-family:tahoma,verdana,segoe,sans-serif\">Recuperar</a></span><!--<![endif]--></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody>"
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
