using CASTROBAR_API.Models;
using CASTROBAR_API.Dtos;
using CASTROBAR_API.Repositories;
using CASTROBAR_API.Utilities;

namespace CASTROBAR_API.Services
{
    public class ProductoService
    {
        private readonly IProductoRepository _productoRepository;
        private readonly TokenAndEncript _tokenAndEncript;

        public ProductoService(IProductoRepository productoRepository, TokenAndEncript tokenAndEncript)
        {
            _productoRepository = productoRepository;
            _tokenAndEncript = tokenAndEncript;
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

        public async Task<int> EliminarProducto(int id, string authHeader)
        {
            
            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                
                var token = authHeader.Substring("Bearer ".Length).Trim();


                var user = "1007153250";
                await _productoRepository.BorrarProductoAsync(id, user);
                return 200;
            }
            else
            {
                throw new Exception("Encabezado de autorización no válido o faltante.");
            }
        }
    }
}
