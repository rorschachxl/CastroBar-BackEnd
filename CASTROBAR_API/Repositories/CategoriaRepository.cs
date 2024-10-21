using CASTROBAR_API.Dtos;
using CASTROBAR_API.Models;
using CASTROBAR_API.Utilities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CASTROBAR_API.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly DbAadd54CastrobarContext _context;
        private readonly CategoriaUtilities _categoriaUtilities;

        public CategoriaRepository(DbAadd54CastrobarContext context, CategoriaUtilities categoriaUtilities)
        {
            _context = context;
            _categoriaUtilities = categoriaUtilities;
        }

        public async Task<IEnumerable<CategoriaResponseDto>> ObtenerTodasCategorias()
        {
            try
            {
                var categorias = await _context.Categoria.ToListAsync();

                if (categorias == null || !categorias.Any())
                {
                    return new List<CategoriaResponseDto>();
                }

                var categoriasDto = categorias.Select(item =>
                {
                    try
                    {
                        return _categoriaUtilities.ConvertirADto(item);
                    }
                    catch (Exception ex)
                    {
                        return null; 
                    }
                }).Where(dto => dto != null).ToList();

                return categoriasDto;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error al obtener las categorías desde la base de datos.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al obtener las categorías.", ex);
            }
        }
        public async Task<int> CrearCategoriaAsync(CategoriaRequestDto categoriaDto)
        {
            if (string.IsNullOrEmpty(categoriaDto.Categoria))
            {
                throw new ArgumentException("El nombre de la categoría no puede estar vacío.");
            }
            var nuevaCategoria = new Categorium
            {
                Categoria = categoriaDto.Categoria
            };

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(nuevaCategoria);
            if (!Validator.TryValidateObject(nuevaCategoria, validationContext, validationResults, true))
            {
                var errors = string.Join("; ", validationResults.Select(r => r.ErrorMessage));
                throw new ValidationException($"Errores de validación: {errors}");
            }

            _context.Categoria.Add(nuevaCategoria);
            await _context.SaveChangesAsync();

            return nuevaCategoria.IdCategoria;
        }
        public async Task<int> EditarCategoriaAsync(int idCategoria, CategoriaRequestDto categoriaDto)
        {
            if (string.IsNullOrEmpty(categoriaDto.Categoria))
            {
                throw new ArgumentException("El nombre de la categoría no puede estar vacío.");
            }

            var categoriaExistente = await _context.Categoria.FindAsync(idCategoria);
            if (categoriaExistente == null)
            {
                throw new KeyNotFoundException("Categoría no encontrada.");
            }

            categoriaExistente.Categoria = categoriaDto.Categoria;

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(categoriaExistente);

            if (!Validator.TryValidateObject(categoriaExistente, validationContext, validationResults, true))
            {
                var errors = string.Join("; ", validationResults.Select(r => r.ErrorMessage));
                throw new ValidationException($"Errores de validación: {errors}");
            }

            _context.Categoria.Update(categoriaExistente);
            await _context.SaveChangesAsync();

            return categoriaExistente.IdCategoria;
        }
    }
}
