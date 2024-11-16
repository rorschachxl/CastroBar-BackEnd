using CASTROBAR_API.Dtos;

namespace CASTROBAR_API.Repositories
{
    public interface IMetodoPagoRepository
    {
        Task<int> AgregarMetodoPagoAsync(MetodoPagoRequestDto metodoPagoDto);
        Task<List<MetodoPagoResponseDto>> ObtenerTodosLosMetodosPagoAsync();
        Task<bool> EditarMetodoPagoAsync(int idMetodoPago, MetodoPagoRequestDto metodoPagoRequest);
    }
}
