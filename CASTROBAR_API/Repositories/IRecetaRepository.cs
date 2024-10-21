using CASTROBAR_API.Dtos;
namespace CASTROBAR_API.Repositories
{
    public interface IRecetaRepository
    {
        Task AgregarRecetaConProductosAsync(RecetaDto recetaDto);
    }
}
