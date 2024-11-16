using CASTROBAR_API.Dtos;

namespace CASTROBAR_API.Repositories
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<CategoriaResponseDto>> ObtenerTodasCategorias();
        Task<int> CrearCategoriaAsync(CategoriaRequestDto categoriaDto);
        Task<int> EditarCategoriaAsync(int idCategoria, CategoriaRequestDto categoriaDto);
    }
}
