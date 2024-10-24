using CASTROBAR_API.Dtos;

namespace CASTROBAR_API.Repositories
{
    public interface IProductoRecetaRepository
    {
        Task AgregarProductosAReceta(int recetaId, List<ProductoRecetaRequestDto> productos);
        Task<List<ProductoRecetaResponseDto>> ObtenerProductosPorRecetaId(int idReceta);
        Task<bool> EliminarProductoRecetaPorId(int idProductoReceta);
    }
}
