using System;
using System.Collections.Generic;

namespace CASTROBAR_API.Models;

public partial class Usuario
{
    public string? NumeroDocumento { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Apellido { get; set; }

    public string? Correo { get; set; }

    public string? Telefono { get; set; }

    public int DocumentoIdDocumento { get; set; }

    public int EstadoIdEstado { get; set; }

    public string? Contraseña { get; set; }

    public int RolIdRol { get; set; }

    public int IdUsuario { get; set; }

    public virtual Documento DocumentoIdDocumentoNavigation { get; set; } = null!;

    public virtual Estado EstadoIdEstadoNavigation { get; set; } = null!;

    public virtual ICollection<Orden> Ordens { get; set; } = new List<Orden>();

    public virtual Rol RolIdRolNavigation { get; set; } = null!;
}
