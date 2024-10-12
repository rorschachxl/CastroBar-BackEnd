using CASTROBAR_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CASTROBAR_API.Repositories
{
    public interface IProductoRepository
    {
        Task<IEnumerable<Producto>> ObtenerTodosProductos();
        Task<Producto> ObtenerProductoIdAsync(int id);
        Task AgregarProductoAsync(Producto producto);
        Task ActualizarProductoAsync(Producto producto);
        Task BorrarProductoAsync(int id);
    }
}
