using System;
using System.Collections.Generic;

namespace CASTROBAR_API.Models;

public partial class Auditorium
{
    public int IdAuditoria { get; set; }

    public DateTime? FechaHora { get; set; }

    public string? Usuario { get; set; }

    public string? Accion { get; set; }

    public string? Descripcion { get; set; }

    public string? Antes { get; set; }

    public string? Despues { get; set; }
}
