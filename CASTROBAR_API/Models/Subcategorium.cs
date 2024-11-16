using System;
using System.Collections.Generic;

namespace CASTROBAR_API.Models;

public partial class Subcategorium
{
    public int IdSubcategoria { get; set; }

    public string? Subcategoria { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
