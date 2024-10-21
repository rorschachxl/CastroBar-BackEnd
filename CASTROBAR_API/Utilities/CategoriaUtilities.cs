using CASTROBAR_API.Dtos;
using CASTROBAR_API.Models;

namespace CASTROBAR_API.Utilities
{
    public class CategoriaUtilities
    {
        private readonly CategoriaUtilities _categoriaUtilities;
        public CategoriaResponseDto ConvertirADto(Categorium categoria)
        {
            return new CategoriaResponseDto
            {
                IdCategoria = categoria.IdCategoria,
                Categoria = categoria.Categoria
            };
        }
    }
}
