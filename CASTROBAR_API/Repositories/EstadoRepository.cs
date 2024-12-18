﻿using CASTROBAR_API.Dtos;
using CASTROBAR_API.Models;
using CASTROBAR_API.Utilities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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
            try
            {
                var estado = _estadoUtilities.ConvertirEnEntidad(estadoDto);

                var validationResults = new List<ValidationResult>();
                var validationContext = new ValidationContext(estado);
                bool isValid = Validator.TryValidateObject(estado, validationContext, validationResults, true);

                if (!isValid)
                {
                    string errores = string.Join("; ", validationResults.Select(r => r.ErrorMessage));
                    throw new ValidationException($"La validación del estado falló: {errores}");
                }

                await _context.Estados.AddAsync(estado);
                await _context.SaveChangesAsync();

                return estado.IdEstado;
            }
            catch (ValidationException vex)
            {
                throw;
            }
            catch (DbUpdateException dbex)
            {
                throw new Exception("Hubo un error al intentar guardar el estado en la base de datos.", dbex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado.", ex);
            }
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
