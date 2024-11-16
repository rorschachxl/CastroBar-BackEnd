using CASTROBAR_API.Dtos;
using CASTROBAR_API.Repositories;

namespace CASTROBAR_API.Services
{
    public class CategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }
        public async Task<IEnumerable<CategoriaResponseDto>> ObtenerCategorias()
        {
            var categoria = await _categoriaRepository.ObtenerTodasCategorias();
            return categoria;
        }
        public async Task<int> CrearCategoria(CategoriaRequestDto categoriaRequestDto)
        {
            int idCategoria = await _categoriaRepository.CrearCategoriaAsync(categoriaRequestDto);
            return idCategoria;
        }
        public async Task<int> ActualizarCategoria(int id, CategoriaRequestDto categoriaRequestDto)
        {
            int respuesta = await _categoriaRepository.EditarCategoriaAsync(id, categoriaRequestDto);
            return respuesta;
        }
    }
}
