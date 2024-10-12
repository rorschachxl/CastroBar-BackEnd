using CASTROBAR_API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CASTROBAR_API.Repositories
{
    public interface IProductoRepository
    {
        Task<IEnumerable<Producto>> ObtenerTodosProductos();
        Task<List<Producto>> ObtenerProductosPorNombreAsync(string nombre);
        Task AgregarProductoAsync(Producto producto);
        Task ActualizarProductoAsync(Producto producto);
        Task BorrarProductoAsync(int id);
    }
}
