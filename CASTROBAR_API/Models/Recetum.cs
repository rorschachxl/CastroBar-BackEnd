using System;
using System.Collections.Generic;

namespace CASTROBAR_API.Models;

public partial class Recetum
{
    public int IdReceta { get; set; }

    public string? Preparacion { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
