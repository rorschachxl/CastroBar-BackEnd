using System;
using System.Collections.Generic;

namespace CASTROBAR_API.Models;

public partial class Proveedor
{
    public int IdProveedor { get; set; }

    public string NumeroDocumento { get; set; } = null!;

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public int? DocumentoIdDocumento { get; set; }

    public int? EstadoIdEstado { get; set; }

    public virtual Documento? DocumentoIdDocumentoNavigation { get; set; }

    public virtual Estado? EstadoIdEstadoNavigation { get; set; }

    public virtual ICollection<ProductoProveedor> ProductoProveedors { get; set; } = new List<ProductoProveedor>();
}
