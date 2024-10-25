using CASTROBAR_API.Dtos;
using CASTROBAR_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CASTROBAR_API.Repositories
{
    public class ProveedorRepository : IProveedorRepository
    {
        private readonly DbAadd54CastrobarContext _context;

        public ProveedorRepository(DbAadd54CastrobarContext context)
        {
            _context = context;
        }
        public async Task<int> AgregarProveedorAsync(ProveedorRequestDto proveedorRequest)
        {
            // Crear una nueva instancia de Proveedor
            var proveedor = new Proveedor
            {
                NumeroDocumento = proveedorRequest.NumeroDocumento,
                Direccion = proveedorRequest.Direccion,
                Telefono = proveedorRequest.Telefono,
                Correo = proveedorRequest.Correo,
                DocumentoIdDocumento = proveedorRequest.DocumentoIdDocumento,
                EstadoIdEstado = proveedorRequest.EstadoIdEstado
            };

            // Agregar el proveedor al contexto
            await _context.Proveedors.AddAsync(proveedor);

            // Guardar cambios en la base de datos
            await _context.SaveChangesAsync();

            // Retornar el Id del proveedor agregado
            return proveedor.IdProveedor;
        }
        public async Task<List<PreveedorResponseDto>> ObtenerTodosLosProveedoresAsync()
        {
            return await _context.Proveedors
                .Select(p => new PreveedorResponseDto
                {
                    IdProveedor = p.IdProveedor,
                    NumeroDocumento = p.NumeroDocumento,
                    Direccion = p.Direccion,
                    Telefono = p.Telefono,
                    Correo = p.Correo
                })
                .ToListAsync();
        }
        public async Task EditarProveedorAsync(int idProveedor, ProveedorRequestDto proveedorDto)
        {
            // Buscar el proveedor existente por su ID
            var proveedor = await _context.Proveedors.FindAsync(idProveedor);

            if (proveedor == null)
            {
                throw new Exception("Proveedor no encontrado.");
            }

            // Actualizar los campos del proveedor solo si no son null
            if (!string.IsNullOrEmpty(proveedorDto.NumeroDocumento))
            {
                proveedor.NumeroDocumento = proveedorDto.NumeroDocumento;
            }

            if (!string.IsNullOrEmpty(proveedorDto.Direccion))
            {
                proveedor.Direccion = proveedorDto.Direccion;
            }

            if (!string.IsNullOrEmpty(proveedorDto.Telefono))
            {
                proveedor.Telefono = proveedorDto.Telefono;
            }

            if (!string.IsNullOrEmpty(proveedorDto.Correo))
            {
                proveedor.Correo = proveedorDto.Correo;
            }

            if (proveedorDto.DocumentoIdDocumento.HasValue && proveedorDto.DocumentoIdDocumento!=0)
            {
                proveedor.DocumentoIdDocumento = proveedorDto.DocumentoIdDocumento;
            }

            if (proveedorDto.EstadoIdEstado.HasValue && proveedorDto.EstadoIdEstado!=0)
            {
                proveedor.EstadoIdEstado = proveedorDto.EstadoIdEstado;
            }

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();
        }
    }
}
