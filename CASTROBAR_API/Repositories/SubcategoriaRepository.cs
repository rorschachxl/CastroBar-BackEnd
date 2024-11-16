using CASTROBAR_API.Dtos;
using CASTROBAR_API.DTOs;
using CASTROBAR_API.Models;
using CASTROBAR_API.Utilities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CASTROBAR_API.Repositories
{
    public class SubcategoriaRepository : ISubcategoriaRepository
    {
        private readonly DbAadd54CastrobarContext _context;
        private readonly SubcategoriaUtilities _subcategoriaUtilities;

        public SubcategoriaRepository(DbAadd54CastrobarContext context, SubcategoriaUtilities subcategoriaUtilities)
        {
            _context = context;
            _subcategoriaUtilities = subcategoriaUtilities;
        }
        public async Task<IEnumerable<SubcategoriaResponseDto>> ObtenerTodasSubcategorias()
        {
            var subcategoria = await _context.Subcategoria.ToListAsync();
            var SubcategoriaresponseDto = subcategoria.Select(item => _subcategoriaUtilities.ConvertirADtos(item));
            return SubcategoriaresponseDto.ToList();
        }
        public async Task<int> CrearSubCategoriaAsync(SubcategoriaRequestDto subcategoriaDto)
        {
            if (string.IsNullOrEmpty(subcategoriaDto.Subcategoria))
            {
                throw new ArgumentException("El nombre de la subcategoría no puede estar vacío.");
            }
            var nuevaSubcategoria = new Subcategorium
            {
                Subcategoria = subcategoriaDto.Subcategoria
            };

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(nuevaSubcategoria);
            if (!Validator.TryValidateObject(nuevaSubcategoria, validationContext, validationResults, true))
            {
                var errors = string.Join("; ", validationResults.Select(r => r.ErrorMessage));
                throw new ValidationException($"Errores de validación: {errors}");
            }

            _context.Subcategoria.Add(nuevaSubcategoria);
            await _context.SaveChangesAsync();

            return nuevaSubcategoria.IdSubcategoria;
        }
        public async Task<int> EditarSubcategoriaAsync(int idSubcategoria, SubcategoriaRequestDto subcategoriaDto)
        {
            if (string.IsNullOrEmpty(subcategoriaDto.Subcategoria))
            {
                throw new ArgumentException("El nombre de la subcategoría no puede estar vacío.");
            }

            var subcategoriaExistente = await _context.Subcategoria.FindAsync(idSubcategoria);
            if (subcategoriaExistente == null)
            {
                throw new KeyNotFoundException("Categoría no encontrada.");
            }

            subcategoriaExistente.Subcategoria = subcategoriaDto.Subcategoria;

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(subcategoriaExistente);

            if (!Validator.TryValidateObject(subcategoriaExistente, validationContext, validationResults, true))
            {
                var errors = string.Join("; ", validationResults.Select(r => r.ErrorMessage));
                throw new ValidationException($"Errores de validación: {errors}");
            }

            _context.Subcategoria.Update(subcategoriaExistente);
            await _context.SaveChangesAsync();

            return subcategoriaExistente.IdSubcategoria;
        }
    }
}
