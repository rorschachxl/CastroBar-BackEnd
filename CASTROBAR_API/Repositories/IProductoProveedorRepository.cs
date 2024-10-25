using CASTROBAR_API.Dtos;

namespace CASTROBAR_API.Repositories
{
    public interface IProductoProveedorRepository
    {
        Task AgregarProductoProveedorAsync(ProductoProveedorRequestDto productoProveedorDto);
        Task<List<ProductoResponseDto>> ObtenerProductosPorProveedorAsync(int proveedorId);
        Task<bool> EliminarProductoProveedorAsync(int idProductoProveedor);
    }
}
