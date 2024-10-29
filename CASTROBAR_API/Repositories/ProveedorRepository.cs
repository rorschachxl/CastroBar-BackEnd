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
        public async Task<bool> EliminarProveedorAsync(int idProveedor)
        {
            // Buscar el proveedor por su ID
            var proveedor = await _context.Proveedors.FindAsync(idProveedor);

            if (proveedor == null)
            {
                // Retornar false si el proveedor no se encuentra
                return false;
            }

            // Obtener los valores actuales para la auditoría
            var documentoAnterior = proveedor.NumeroDocumento;
            var direccionAnterior = proveedor.Direccion;
            var telefonoAnterior = proveedor.Telefono;
            var correoAnterior = proveedor.Correo;
            var estadoAnterior = proveedor.EstadoIdEstado;

            // Actualizar el proveedor con valores nulos y cambiar estado a 32
            proveedor.NumeroDocumento = null;
            proveedor.Direccion = null;
            proveedor.Telefono = null;
            proveedor.Correo = null;
            proveedor.EstadoIdEstado = 32;

            // Guardar cambios en la base de datos
            await _context.SaveChangesAsync();

            // Registrar la acción en la tabla de auditoría
            var auditoria = new Auditorium
            {
                FechaHora = DateTime.Now,
                Usuario = "1007153250", // Usuario estático
                Accion = "Eliminación Lógica de Proveedor",
                Descripcion = "Se eliminó lógicamente el proveedor y se actualizó su estado a 32.",
                Antes = $"{{\"NumeroDocumento\": \"{documentoAnterior}\", \"Direccion\": \"{direccionAnterior}\", \"Telefono\": \"{telefonoAnterior}\", \"Correo\": \"{correoAnterior}\", \"EstadoIdEstado\": \"{estadoAnterior}\"}}",
                Despues = "{\"NumeroDocumento\": null, \"Direccion\": null, \"Telefono\": null, \"Correo\": null, \"EstadoIdEstado\": 32}"
            };

            // Agregar la auditoría al contexto
            await _context.Auditoria.AddAsync(auditoria);

            // Guardar cambios en la base de datos para la auditoría
            await _context.SaveChangesAsync();

            // Retornar true si la eliminación lógica fue exitosa
            return true;
        }

    }
}
