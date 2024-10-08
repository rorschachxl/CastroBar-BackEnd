using System;
using System.Collections.Generic;

namespace CASTROBAR_API.Models;

public partial class ProductoOrden
{
    public int IdProductoOrden { get; set; }

    public int? OrdenIdOrden { get; set; }

    public int? ProductoIdProducto { get; set; }

    public virtual Orden? OrdenIdOrdenNavigation { get; set; }

    public virtual Producto? ProductoIdProductoNavigation { get; set; }
}
