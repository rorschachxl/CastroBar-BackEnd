using System;
using System.Collections.Generic;

namespace CASTROBAR_API.Models;

public partial class Estado
{
    public int IdEstado { get; set; }

    public string? Estado1 { get; set; }

    public virtual ICollection<Orden> Ordens { get; set; } = new List<Orden>();

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();

    public virtual ICollection<Proveedor> Proveedors { get; set; } = new List<Proveedor>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
