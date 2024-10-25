using CASTROBAR_API.Dtos;
using CASTROBAR_API.Repositories;

namespace CASTROBAR_API.Services
{
    public class MesaService
    {
        private readonly IMesaRepository _mesaRepositorio;

        public MesaService(IMesaRepository mesaRepositorio)
        {
            _mesaRepositorio = mesaRepositorio;
        }

        public async Task<List<MesaResponseDto>> ObtenerTodasLasMesasAsync()
        {
            return await _mesaRepositorio.ObtenerTodasLasMesasAsync();
        }
        public async Task<int> AgregarMesa(MesaRequestDto mesaDto)
        {
            return await _mesaRepositorio.AgregarMesa(mesaDto);
        }
        public async Task<bool> EditarMesaAsync(int numeroMesa, MesaRequestDto mesaDto)
        {
            return await _mesaRepositorio.EditarMesaAsync(numeroMesa, mesaDto);
        }
    }
}
