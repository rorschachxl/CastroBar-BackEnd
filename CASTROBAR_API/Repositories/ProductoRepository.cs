
using CASTROBAR_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CASTROBAR_API.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly DbAadd54CastrobarContext _context;

        public ProductoRepository (DbAadd54CastrobarContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Producto>> ObtenerTodosProductos()
        {
            return await _context.Productos.ToListAsync();
        }

        public async Task<Producto> ObtenerProductoIdAsync(int id)
        {
            return await _context.Productos.FindAsync(id);
        }

        public async Task AgregarProductoAsync(Producto producto)
        {
            await _context.Productos.AddAsync(producto);
            await _context.SaveChangesAsync();
        }
        public async Task ActualizarProductoAsync(Producto producto)
        {
            _context.Productos.Update(producto);
            await _context.SaveChangesAsync();
        }
        public async Task BorrarProductoAsync(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
            }
        }
    }
}
