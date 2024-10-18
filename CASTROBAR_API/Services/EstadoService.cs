using CASTROBAR_API.Dtos;
using CASTROBAR_API.Repositories;

namespace CASTROBAR_API.Services
{
    public class EstadoService
    {
        private readonly IEstadoRepository _estadoRepository;

        public EstadoService(IEstadoRepository estadoRepository)
        {
            _estadoRepository = estadoRepository;
        }
        public async Task<IEnumerable<EstadoDto>> ObtenerEstados()
        {
            var estados = await _estadoRepository.ObtenerTodosEstados();
            return estados;
        }
        public async Task<int> CrearEstado(EstadoDto estadoDto)
        {
            int estado = await _estadoRepository.AgregarEstadoAsync(estadoDto);
            return estado;
        }
        public async Task<int> ActualizarEstado(int id, EstadoDto estadoDto)
        {
            int respuesta = await _estadoRepository.ActualizarEstadoAsync(id, estadoDto);
            return respuesta;
        }
        public async Task<int> EliminarEstado(int id)
        {
            int respuesta = await _estadoRepository.BorrarEstadoAsync(id);
            return respuesta;
        }
    }
}

