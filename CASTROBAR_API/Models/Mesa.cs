using System;
using System.Collections.Generic;

namespace CASTROBAR_API.Models;

public partial class Mesa
{
    public int NumeroMesa { get; set; }

    public int? Capacidad { get; set; }
    public int? ORDENIdOrden { get; set; }  
    public int? ESTADOIdEstado { get; set; }  
    public virtual Orden? ORDENIdOrdenNavigation { get; set; } 
    public virtual Estado? ESTADOIdEstadoNavigation { get; set; }  

    public virtual ICollection<Orden> Ordens { get; set; } = new List<Orden>();  
}
