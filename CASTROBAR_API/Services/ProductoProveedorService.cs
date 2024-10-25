using CASTROBAR_API.Dtos;
using CASTROBAR_API.Repositories;

namespace CASTROBAR_API.Services
{
    public class ProductoProveedorService
    {
        private readonly IProductoProveedorRepository _productoProveedorRepository;

        public ProductoProveedorService(IProductoProveedorRepository productoProveedorRepository)
        {
            _productoProveedorRepository = productoProveedorRepository;
        }
        public async Task AgregarProductoProveedorAsync(ProductoProveedorRequestDto productoProveedorDto)
        {
            await _productoProveedorRepository.AgregarProductoProveedorAsync(productoProveedorDto);
        }
        public async Task<List<ProductoResponseDto>> ObtenerProductosPorProveedorAsync(int proveedorId)
        {
            var productos = await _productoProveedorRepository.ObtenerProductosPorProveedorAsync(proveedorId);
            return productos;
        }
        public async Task<bool> EliminarProductoProveedorAsync(int idProductoProveedor)
        {
            return await _productoProveedorRepository.EliminarProductoProveedorAsync(idProductoProveedor);
        }
    }
}
