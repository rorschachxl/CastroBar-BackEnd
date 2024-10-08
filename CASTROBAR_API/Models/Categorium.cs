using System;
using System.Collections.Generic;

namespace CASTROBAR_API.Models;

public partial class Categorium
{
    public int IdCategoria { get; set; }

    public string? Categoria { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
