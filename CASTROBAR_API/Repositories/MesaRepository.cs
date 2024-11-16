using CASTROBAR_API.Dtos;
using CASTROBAR_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CASTROBAR_API.Repositories
{
    public class MesaRepository : IMesaRepository
    {
        private readonly DbAadd54CastrobarContext _context;

        public MesaRepository(DbAadd54CastrobarContext context)
        {
            _context = context;
        }

        public async Task<List<MesaResponseDto>> ObtenerTodasLasMesasAsync()
        {
            var mesas = await _context.Mesas
                .Select(m => new
                {
                    Mesa = m,
                    OrdenId = _context.Ordens
                        .Where(o => o.MesaNumeroMesa == m.NumeroMesa)
                        .Select(o => o.IdOrden)
                        .FirstOrDefault() // Obtener el ID de la primera orden o null
                })
                .Select(x => new MesaResponseDto
                {
                    NumeroMesa = x.Mesa.NumeroMesa,
                    Capacidad = x.Mesa.Capacidad,
                    EstadoIdEstado = x.Mesa.ESTADOIdEstado,
                    OrdenId = x.OrdenId // Agregar el ID de la orden
                })
                .ToListAsync();

            return mesas;
        }

        public async Task<int> AgregarMesa(MesaRequestDto mesaDto)
        {
            var nuevaMesa = new Mesa
            {
                Capacidad = mesaDto.Capacidad,
                ESTADOIdEstado = 21, 
            };

            _context.Mesas.Add(nuevaMesa);
            _context.SaveChanges();

            return nuevaMesa.NumeroMesa; 
        }
        public async Task<bool> EditarMesaAsync(int numeroMesa, MesaRequestDto mesaDto)
        {
            var mesa = await _context.Mesas.FindAsync(numeroMesa);
            if (mesa == null)
            {
                return false; 
            }

            mesa.Capacidad = mesaDto.Capacidad;

            await _context.SaveChangesAsync();
            return true; 
        }
        public async Task<bool> EliminarMesaAsync(int numeroMesa)
        {
            var mesa = await _context.Mesas.FindAsync(numeroMesa);
            if (mesa == null)
            {
                return false;
            }

            _context.Mesas.Remove(mesa);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
