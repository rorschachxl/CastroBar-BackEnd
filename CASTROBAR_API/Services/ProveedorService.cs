using CASTROBAR_API.Dtos;
using CASTROBAR_API.Repositories;

namespace CASTROBAR_API.Services
{
    public class ProveedorService
    {
        private readonly IProveedorRepository _proveedorRepository;

        public ProveedorService(IProveedorRepository proveedorRepository)
        {
            _proveedorRepository = proveedorRepository;
        }

        public async Task<int> AgregarProveedorAsync(ProveedorRequestDto proveedorRequest)
        {
            // Validar el DTO según sea necesario
            if (string.IsNullOrWhiteSpace(proveedorRequest.NumeroDocumento))
            {
                throw new ArgumentException("El número de documento es obligatorio.");
            }

            // Agregar el proveedor usando el repositorio
            var nuevoProveedorId = await _proveedorRepository.AgregarProveedorAsync(proveedorRequest);
            return nuevoProveedorId;
        }
        public async Task<List<PreveedorResponseDto>> ObtenerTodosLosProveedoresAsync()
        {
            return await _proveedorRepository.ObtenerTodosLosProveedoresAsync();
        }

        public async Task EditarProveedorAsync(int idProveedor, ProveedorRequestDto proveedorDto)
        {
            // Validación básica para asegurar que el DTO tenga un número de documento
            if (string.IsNullOrEmpty(proveedorDto.NumeroDocumento))
            {
                throw new ArgumentException("El número de documento es obligatorio.");
            }

            // Llamar al repositorio para editar el proveedor
            await _proveedorRepository.EditarProveedorAsync(idProveedor, proveedorDto);
        }
        public async Task<bool> EliminarProveedorAsync(int idProveedor)
        {
            // Llama al método de eliminación en el repositorio
            return await _proveedorRepository.EliminarProveedorAsync(idProveedor);
        }
    }
}