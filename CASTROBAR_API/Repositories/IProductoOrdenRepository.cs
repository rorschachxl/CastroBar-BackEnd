using CASTROBAR_API.Dtos;

namespace CASTROBAR_API.Repositories
{
    public interface IProductoOrdenRepository
    {
        Task<List<ProductoOrdenResponseDto>> ObtenerProductosPorOrdenIdAsync(int ordenId);
        Task<int> AgregarProductoOrdenAsync(ProductoOrdenRequestDto productoOrdenRequestDto);
        Task<byte[]> GenerarFacturaPdfAsync(int ordenId);
    }
}
