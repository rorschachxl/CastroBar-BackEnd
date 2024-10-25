using CASTROBAR_API.Dtos;
using CASTROBAR_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CASTROBAR_API.Repositories
{
    public class MetodoPagoRepository : IMetodoPagoRepository
    {
        private readonly DbAadd54CastrobarContext _context;

        public MetodoPagoRepository(DbAadd54CastrobarContext context)
        {
            _context = context;
        }

        public async Task<int> AgregarMetodoPagoAsync(MetodoPagoRequestDto metodoPagoDto)
        {
            var nuevoMetodoPago = new MetodoPago
            {
                MetodoPago1 = metodoPagoDto.MetodoPago
            };

            await _context.MetodoPagos.AddAsync(nuevoMetodoPago);

            await _context.SaveChangesAsync();

            return nuevoMetodoPago.IdMetodoPago;
        }
        public async Task<List<MetodoPagoResponseDto>> ObtenerTodosLosMetodosPagoAsync()
        {
            var metodosPago = await _context.MetodoPagos
                .Select(mp => new MetodoPagoResponseDto
                {
                    IdMetodoPago = mp.IdMetodoPago,
                    MetodoPago1 = mp.MetodoPago1
                })
                .ToListAsync();

            return metodosPago;
        }
        public async Task<bool> EditarMetodoPagoAsync(int idMetodoPago, MetodoPagoRequestDto metodoPagoRequest)
        {
            var metodoPago = await _context.MetodoPagos.FindAsync(idMetodoPago);

            if (metodoPago == null)
            {
                return false; 
            }

            metodoPago.MetodoPago1 = metodoPagoRequest.MetodoPago;

            await _context.SaveChangesAsync();
            return true; 
        }
    }
}
