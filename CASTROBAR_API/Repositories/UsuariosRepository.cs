using Microsoft.EntityFrameworkCore;
using CASTROBAR_API.Dtos;
using CASTROBAR_API.Models;

namespace CASTROBAR_API.Repositories
{
    public class UsuariosRepository
    {
        private readonly DbAadd54CastrobarContext _context;
            public UsuariosRepository(DbAadd54CastrobarContext contexto)
        {
            _context = contexto;
        }

        //public bool CreateUser()

        //metodo para acceder <
        public async Task<Usuario> SigIn (LoginDto usuario)
        {
            var user = await _context.Usuarios.Where(s => s.NumeroDocumento == usuario.numeroDocumento && s.Contraseña == usuario.contraseña).FirstOrDefaultAsync();
            return user;
        }
        public async Task<Usuario> GetUser (LoginDto usuario)
        {
            var user = await _context.Usuarios.Where(s => s.NumeroDocumento == usuario.numeroDocumento).FirstOrDefaultAsync();
            return user;
        }
    }
}
