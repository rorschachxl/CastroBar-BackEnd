using CASTROBAR_API.Dtos;
using CASTROBAR_API.DTOs;
using CASTROBAR_API.Repositories;

namespace CASTROBAR_API.Services
{
    public class SubcategoriaService
    {
        private readonly ISubcategoriaRepository _subcategoriaRepository;
        public SubcategoriaService(ISubcategoriaRepository subcategoriaRepository)
        {
            _subcategoriaRepository = subcategoriaRepository;
        }
        public async Task<IEnumerable<SubcategoriaResponseDto>> ObtenerSubcategorias()
        {
            var subcategoria = await _subcategoriaRepository.ObtenerTodasSubcategorias();
            return subcategoria;
        }
        public async Task<int> CrearSubcategoria(SubcategoriaRequestDto subcategoriaRequestDto)
        {
            int idCategoria = await _subcategoriaRepository.CrearSubCategoriaAsync(subcategoriaRequestDto);
            return idCategoria;
        }
        public async Task<int> ActualizarSubcategoria(int id, SubcategoriaRequestDto subcategoriaRequestDto)
        {
            int respuesta = await _subcategoriaRepository.EditarSubcategoriaAsync(id, subcategoriaRequestDto);
            return respuesta;
        }
    }
}
