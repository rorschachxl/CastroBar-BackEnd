using CASTROBAR_API.Repositories;
using CASTROBAR_API.Dtos;

namespace CASTROBAR_API.Services
{
    public class RecetaService
    {
        private readonly IRecetaRepository _recetaRepository;

        public RecetaService(IRecetaRepository recetaRepository)
        {
            _recetaRepository = recetaRepository;
        }

        public async Task<int> AgregarReceta(RecetaRequestDto recetaDto)
        {
            int result = await _recetaRepository.AgregarRecetaAsync(recetaDto);
            return result;
        }
        public async Task<List<RecetaResponseDto>> ObtenerTodasLasRecetas()
        {
            return await _recetaRepository.ObtenerTodasLasRecetas();
        }
        public async Task EliminarRecetaYActualizarProductos(int idReceta)
        {
            await _recetaRepository.EliminarRecetaYActualizarProductos(idReceta);
        }
    }
}
