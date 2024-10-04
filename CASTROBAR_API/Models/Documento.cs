using System;
using System.Collections.Generic;

namespace CASTROBAR_API.Models;

public partial class Documento
{
    public int IdDocumento { get; set; }

    public string? Documento1 { get; set; }

    public virtual ICollection<Proveedor> Proveedors { get; set; } = new List<Proveedor>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
