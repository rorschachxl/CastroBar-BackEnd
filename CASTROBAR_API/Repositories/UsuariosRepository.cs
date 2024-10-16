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
        public async Task<Usuario> SigIn(LoginDto usuario)
        {
            var user = await _context.Usuarios.Where(s => s.NumeroDocumento == usuario.numeroDocumento && s.Contraseña == usuario.contraseña).FirstOrDefaultAsync();
            return user;
        }
        public async Task<Usuario> GetUser(LoginDto usuario)
        {
            var user = await _context.Usuarios.Where(s => s.NumeroDocumento == usuario.numeroDocumento).FirstOrDefaultAsync();
            return user;
        }
        public async Task CrearUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }
        
        public async Task<Usuario> obtenerUsuario(string numDoc)
        {
            var result = await _context.Usuarios.Where(s => s.NumeroDocumento == numDoc).FirstOrDefaultAsync();
            return result;
        }
        public async Task UpdateUser(Usuario usuario, UpdateUserDto user)
        {
            usuario.Nombre = user.Nombre;
            usuario.Apellido = user.Apellido;
            usuario.Contraseña = user.Contraseña;
            usuario.Telefono = user.Telefono;
            usuario.Correo = user.Correo;
            usuario.RolIdRol = user.Rol;
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Usuario>> GetAllUsers()
        {
            var users = await _context.Usuarios.ToListAsync();
            return users;
        }
        public async Task<Usuario> GetUserById(string id)
        {
            var user = await _context.Usuarios.Where(s=>s.NumeroDocumento == id).FirstOrDefaultAsync();
            return user;
        }
        public async Task<bool> DeleteUser(Usuario user)
        { 
            _context.Usuarios.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
