using CASTROBAR_API.Dtos;
using CASTROBAR_API.Repositories;

namespace CASTROBAR_API.Services
{
    public class MetodoPagoService
    {
        private readonly IMetodoPagoRepository _metodoPagoRepositorio;

        public MetodoPagoService(IMetodoPagoRepository metodoPagoRepositorio)
        {
            _metodoPagoRepositorio = metodoPagoRepositorio;
        }

        public async Task<int> AgregarMetodoPagoAsync(MetodoPagoRequestDto metodoPagoDto)
        {
            if (string.IsNullOrWhiteSpace(metodoPagoDto.MetodoPago))
            {
                throw new ArgumentException("El nombre del método de pago no puede estar vacío.");
            }

            return await _metodoPagoRepositorio.AgregarMetodoPagoAsync(metodoPagoDto);
        }
        public async Task<List<MetodoPagoResponseDto>> ObtenerTodosLosMetodosPagoAsync()
        {
            return await _metodoPagoRepositorio.ObtenerTodosLosMetodosPagoAsync();
        }
        public async Task<bool> EditarMetodoPagoAsync(int idMetodoPago, MetodoPagoRequestDto metodoPagoRequest)
        {
            return await _metodoPagoRepositorio.EditarMetodoPagoAsync(idMetodoPago, metodoPagoRequest);
        }
    }
}
