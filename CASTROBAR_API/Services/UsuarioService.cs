using CASTROBAR_API.Dtos;
using CASTROBAR_API.Utilities;
using CASTROBAR_API.Repositories;

using System.Text;


namespace CASTROBAR_API.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepositorio _usuarioRepositorio;

        public UsuarioService(UsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<UsuarioRequestDto> RegistroUsuario(UsuarioRequestDto usuarioRequest)
        {
            UsuarioRequestDto usuario1 = new UsuarioRequestDto();
            ByCriptUtility byCriptUtility = new ByCriptUtility();
            usuarioRequest.contraseña = byCriptUtility.HashPassword(usuarioRequest.contraseña);
            usuario1 = await _usuarioRepositorio.RegistroUsuario(usuarioRequest);
            return usuarioRequest;

        }

        public async Task<int> ActualizarUSuario(String numeroDocumento, String nombre, String apellido, string telefono, string correo)
        {
            int actualizado = 0;
            actualizado = await _usuarioRepositorio.ActualizarUsuario(numeroDocumento, nombre, apellido, telefono, correo);
            return actualizado;
        }
    }


}
