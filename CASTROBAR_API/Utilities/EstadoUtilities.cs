using CASTROBAR_API.Dtos;
using CASTROBAR_API.Models;

namespace CASTROBAR_API.Utilities
{
    public class EstadoUtilities
    {
        private readonly EstadoUtilities _estadoUtilities;

        public EstadoDto ConvertirADtos(Estado estado)
        {
            if (estado == null)
            {
                throw new ArgumentNullException(nameof(estado), "El producto no puede ser nulo.");
            }

            return new EstadoDto
            {
                idEstado = estado.IdEstado,
                estado = estado.Estado1,
            };
        }

        internal object ConvertirADtos(List<Estado> estado)
        {
            throw new NotImplementedException();
        }
        public Estado ConvertirEnEntidad(EstadoDto estadoDto)
        {
            return new Estado
            {
                IdEstado = (int)estadoDto.idEstado,
                Estado1 = estadoDto.estado,
                
            };
        }
    }
}
