using CASTROBAR_API.Dtos;
using CASTROBAR_API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CASTROBAR_API.Repositories
{
    public interface IProductoRepository
    {
        Task<IEnumerable<ProductoResponseDto>> ObtenerTodosProductos();
        Task <int>AgregarProductoAsync(ProductoRequestDto producto);
        Task <int> ActualizarProductoAsync(int id, ProductoRequestDto producto);
        Task <int> BorrarProductoAsync(int id, string usuarioEliminacion);

    }
}
