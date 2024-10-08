using System;
using System.Collections.Generic;

namespace CASTROBAR_API.Models;

public partial class MetodoPago
{
    public int IdMetodoPago { get; set; }

    public string? MetodoPago1 { get; set; }

    public virtual ICollection<Orden> Ordens { get; set; } = new List<Orden>();
}
