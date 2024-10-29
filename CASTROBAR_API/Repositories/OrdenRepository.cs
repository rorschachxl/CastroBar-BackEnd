using CASTROBAR_API.Models;

namespace CASTROBAR_API.Repositories
{
    public class OrdenRepository : IOrdenRepository
    {
        private readonly DbAadd54CastrobarContext _context;

        public OrdenRepository(DbAadd54CastrobarContext context)
        {
            _context = context;
        }

        public async Task<int> AgregarOrdenAsync(int numeroMesa)
        {
            var mesa = await _context.Mesas.FindAsync(numeroMesa);

            if (mesa == null)
            {
                throw new Exception("Mesa no encontrada.");
            }

            if (mesa.ESTADOIdEstado != 21)
            {
                throw new Exception("No se puede crear la orden porque el estado de la mesa no es 21.");
            }

            var nuevaOrden = new Orden
            {
                MesaNumeroMesa = numeroMesa,
                EstadoIdEstado = 31,
                Fecha = DateOnly.FromDateTime(DateTime.Now),
                Hora = TimeOnly.FromDateTime(DateTime.Now)
            };

            _context.Ordens.Add(nuevaOrden);

            mesa.ESTADOIdEstado = 22;

            await _context.SaveChangesAsync();

            return nuevaOrden.IdOrden;
        }
    }
}
