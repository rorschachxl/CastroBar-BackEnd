using CASTROBAR_API.Dtos;
namespace CASTROBAR_API.Repositories
{
    public interface IRecetaRepository
    {
        Task<int> AgregarRecetaAsync(RecetaRequestDto recetaRequest);
        Task<List<RecetaResponseDto>> ObtenerTodasLasRecetas();
        Task EliminarRecetaYActualizarProductos(int idReceta);
    }
}
