using CASTROBAR_API.Dtos;
using CASTROBAR_API.Models;

namespace CASTROBAR_API.Utilities
{
    public class SubcategoriaUtilities
    {
        private readonly SubcategoriaUtilities _subcategoriaUtilities;
        public SubcategoriaResponseDto ConvertirADtos(Subcategorium subcategoria)
        {
            if (subcategoria == null)
            {
                throw new ArgumentNullException(nameof(subcategoria), "El producto no puede ser nulo.");
            }

            return new SubcategoriaResponseDto
            {
                IdSubcategoria = subcategoria.IdSubcategoria,
                Subcategoria = subcategoria.Subcategoria,
            };
        }
    }
}
