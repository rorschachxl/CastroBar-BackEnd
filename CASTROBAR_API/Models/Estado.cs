using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CASTROBAR_API.Models;


public partial class Estado
{
    [Required(ErrorMessage = "El ID del estado es obligatorio.")]
    [RegularExpression("^[0-9]+$", ErrorMessage = "El ID del estado solo puede contener números.")]
    [Range(1, int.MaxValue, ErrorMessage = "El ID del estado debe ser un número positivo mayor que cero.")]
    public int IdEstado { get; set; }

    [Required(ErrorMessage = "El estado es obligatorio.")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El estado solo puede contener letras y espacios.")]
    [StringLength(100, ErrorMessage = "El nombre del estado no puede tener más de 100 caracteres.")]
    public string? Estado1 { get; set; }

    public virtual ICollection<Orden> Ordens { get; set; } = new List<Orden>();
    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
    public virtual ICollection<Proveedor> Proveedors { get; set; } = new List<Proveedor>();
    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
