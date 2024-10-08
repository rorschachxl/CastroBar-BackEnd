using System;
using System.Collections.Generic;

namespace CASTROBAR_API.Models;

public partial class ProductoProveedor
{
    public int IdProductoProveedor { get; set; }

    public int? ProductoIdProducto { get; set; }

    public int? ProveedorIdProveedor { get; set; }

    public virtual Producto? ProductoIdProductoNavigation { get; set; }

    public virtual Proveedor? ProveedorIdProveedorNavigation { get; set; }
}
