using CASTROBAR_API.Dtos;
using CASTROBAR_API.Repositories;

namespace CASTROBAR_API.Services
{
    public class ProductoOrdenService
    {
        private readonly IProductoOrdenRepository _productoOrdenRepositorio;

        public ProductoOrdenService(IProductoOrdenRepository productoOrdenRepositorio)
        {
            _productoOrdenRepositorio = productoOrdenRepositorio;
        }

        // Método para agregar un ProductoOrden
        public async Task<int> AgregarProductoOrdenAsync(ProductoOrdenRequestDto productoOrdenRequestDto)
        {
            // Validación o lógica adicional antes de agregar el ProductoOrden
            return await _productoOrdenRepositorio.AgregarProductoOrdenAsync(productoOrdenRequestDto);
        }

        // Método para obtener los productos de una orden específica
        public async Task<List<ProductoOrdenResponseDto>> ObtenerProductosPorOrdenIdAsync(int ordenId)
        {
            // Validación o lógica adicional antes de obtener los productos de una orden
            return await _productoOrdenRepositorio.ObtenerProductosPorOrdenIdAsync(ordenId);
        }
        public async Task<byte[]> GenerarFacturaAsync(int ordenId)
        {
            // Lógica adicional si es necesario

            // Llamar al repositorio para generar el PDF
            return await _productoOrdenRepositorio.GenerarFacturaPdfAsync(ordenId);
        }
    }
}
