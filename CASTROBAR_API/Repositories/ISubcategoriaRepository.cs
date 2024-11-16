using CASTROBAR_API.Dtos;
using CASTROBAR_API.DTOs;

namespace CASTROBAR_API.Repositories
{
    public interface ISubcategoriaRepository
    {
        Task<IEnumerable<SubcategoriaResponseDto>> ObtenerTodasSubcategorias();
        Task<int> CrearSubCategoriaAsync(SubcategoriaRequestDto subcategoriaDto);
        Task<int> EditarSubcategoriaAsync(int idSubcategoria, SubcategoriaRequestDto subcategoriaDto);
    }
}
