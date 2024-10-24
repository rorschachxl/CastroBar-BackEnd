using CASTROBAR_API.Dtos;
using CASTROBAR_API.Repositories;

namespace CASTROBAR_API.Services
{
    public class ProductoRecetaService
    {
        private readonly IProductoRecetaRepository _productoRecetaRepository;

        public ProductoRecetaService(IProductoRecetaRepository productoRecetaRepository)
        {
            _productoRecetaRepository = productoRecetaRepository;
        }


        public async Task AgregaProductosAReceta(int idReceta, List<ProductoRecetaRequestDto> recetaDto)
        {

            // Luego, agregar los productos a la receta usando el ID obtenido
            await _productoRecetaRepository.AgregarProductosAReceta(idReceta, recetaDto);

        }
        public async Task<List<ProductoRecetaResponseDto>> ObtenerProductosPorReceta(int idReceta)
        {
            return await _productoRecetaRepository.ObtenerProductosPorRecetaId(idReceta);
        }
        public async Task<bool> EliminarProductoRecetaPorId(int idProductoReceta)
        {
            return await _productoRecetaRepository.EliminarProductoRecetaPorId(idProductoReceta);
        }
    }
}
