using System.ComponentModel.DataAnnotations;

namespace CASTROBAR_API.DTOs
{
    public class SubcategoriaRequestDto
    {
        [Required(ErrorMessage = "El nombre de la subcategoría es requerido.")]
        [MaxLength(50, ErrorMessage = "El nombre de la subcategoría no puede exceder los 50 caracteres.")]
        public string Subcategoria { get; set; } = null!;
    }
}