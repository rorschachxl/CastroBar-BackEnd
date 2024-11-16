using CASTROBAR_API.Dtos;

namespace CASTROBAR_API.Repositories
{
    public interface IMesaRepository
    {
        Task<List<MesaResponseDto>> ObtenerTodasLasMesasAsync();
        Task<int> AgregarMesa(MesaRequestDto mesaDto);
        Task<bool> EditarMesaAsync(int numeroMesa, MesaRequestDto mesaDto);
        Task<bool> EliminarMesaAsync(int numeroMesa);
    }
}
