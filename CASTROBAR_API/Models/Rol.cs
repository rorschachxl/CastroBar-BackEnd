using System;
using System.Collections.Generic;

namespace CASTROBAR_API.Models;

public partial class Rol
{
    public int IdRol { get; set; }

    public string? Rol1 { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
