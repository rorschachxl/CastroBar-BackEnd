using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CASTROBAR_API.Models;

public partial class Categorium
{
    public int IdCategoria { get; set; }

    [Required(ErrorMessage = "El nombre de la categoría es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre de la categoría no puede exceder los 100 caracteres.")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "La categoría solo puede contener letras y espacios.")]
    public string? Categoria { get; set; } = string.Empty;

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
