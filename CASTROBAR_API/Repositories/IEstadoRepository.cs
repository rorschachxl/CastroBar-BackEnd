using CASTROBAR_API.Dtos;

namespace CASTROBAR_API.Repositories
{
    public interface IEstadoRepository
    {
        Task<IEnumerable<EstadoDto>> ObtenerTodosEstados();
        Task<int> AgregarEstadoAsync(EstadoDto estadoDto);
        Task<int> ActualizarEstadoAsync(int id, EstadoDto estadoDto);

        Task<int> BorrarEstadoAsync(int id);
    }
}
