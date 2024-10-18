using CASTROBAR_API.Dtos;
using CASTROBAR_API.Models;
using CASTROBAR_API.Utilities;
using Microsoft.EntityFrameworkCore;

namespace CASTROBAR_API.Repositories
{
    public class EstadoRepository : IEstadoRepository
    {
        private readonly DbAadd54CastrobarContext _context;
        private readonly EstadoUtilities _estadoUtilities;

        public EstadoRepository(DbAadd54CastrobarContext context, EstadoUtilities estadoUtilities)
        {
            _context = context;
            _estadoUtilities = estadoUtilities;
        }
        public async Task<IEnumerable<EstadoDto>> ObtenerTodosEstados()
        {
            var estado = await _context.Estados.ToListAsync();
            var estadoDto = estado.Select(item => _estadoUtilities.ConvertirADtos(item)); 
            return estadoDto.ToList();
        }
        public async Task<int> AgregarEstadoAsync(EstadoDto estadoDto)
        {
            var estado = _estadoUtilities.ConvertirEnEntidad(estadoDto);
            await _context.Estados.AddAsync(estado);
            await _context.SaveChangesAsync();
            return estado.IdEstado;
        }
        public async Task<int> ActualizarEstadoAsync(int id, EstadoDto estadoDto)
        {
            
            var estadoExistente = await _context.Estados.FindAsync(id);

            if (estadoExistente == null)
            {
                return 0; 
            }


            if (estadoDto.idEstado.HasValue)
                estadoExistente.IdEstado = estadoExistente.IdEstado;

            if (!string.IsNullOrEmpty(estadoDto.estado))
                estadoExistente.Estado1 = estadoDto.estado;

            
            _context.Estados.Update(estadoExistente);

            return await _context.SaveChangesAsync();
        }
        public async Task<int> BorrarEstadoAsync(int id)
        {
            var estado = await _context.Estados.FindAsync(id);
            if (estado != null)
            {
                _context.Estados.Remove(estado);
                await _context.SaveChangesAsync();
                return 200;
            }
            else
            {
                return 500;
            }
        }
    }
}
