using CASTROBAR_API.Models;
using CASTROBAR_API.Dtos;
using CASTROBAR_API.Repositories;
using CASTROBAR_API.Utilities;

namespace CASTROBAR_API.Services
{
    public class ProductoService
    {
        private readonly IProductoRepository _productoRepository;

        public ProductoService(IProductoRepository productoRepository) 
        {
            _productoRepository = productoRepository;
        }

        public async Task<IEnumerable<ProductoResponseDto>> ObtenerProductos()
        {
            var productos = await _productoRepository.ObtenerTodosProductos();
            return productos;
        }
        public async Task<int> CrearProducto(ProductoRequestDto productoRequestDto)
        {
            int idProducto = await _productoRepository.AgregarProductoAsync(productoRequestDto);
            return idProducto;
        }

        public async Task<int> ActualizarProducto(int id, ProductoRequestDto productoRequestDto)
        {
            int respuesta = await _productoRepository.ActualizarProductoAsync(id, productoRequestDto);
            return respuesta;
        }

        public async Task<int> EliminarProducto(int id)
        {
            int respuesta = await _productoRepository.BorrarProductoAsync(id);
            return respuesta;
        }
    }
}
