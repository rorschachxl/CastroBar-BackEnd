using CASTROBAR_API.Repositories;

namespace CASTROBAR_API.Services
{
    public class OrdenService
    {
        private readonly IOrdenRepository _ordenRepositorio;

        public OrdenService(IOrdenRepository ordenRepositorio)
        {
            _ordenRepositorio = ordenRepositorio;
        }

        public async Task<int> CrearOrdenAsync(int numeroMesa)
        {
            int idOrden = await _ordenRepositorio.AgregarOrdenAsync(numeroMesa);
            return idOrden; 
        }
    }
}
