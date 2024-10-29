using System;
using System.Collections.Generic;

namespace CASTROBAR_API.Models;

public partial class Mesa
{
    public int NumeroMesa { get; set; }

    public int? Capacidad { get; set; }
    public int? ESTADOIdEstado { get; set; }  
    public virtual Estado? ESTADOIdEstadoNavigation { get; set; }  

}
