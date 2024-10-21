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

        public async Task AgregarRecetaConProductos(RecetaDto recetaDto)
        {
            await _recetaRepository.AgregarRecetaConProductosAsync(recetaDto);
        }
    }
}
