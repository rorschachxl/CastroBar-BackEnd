using System;
using System.Collections.Generic;

namespace CASTROBAR_API.Models;

public partial class ProductoRecetum
{
    public int? IdProductoReceta { get; set; }

    public int? ProductoIdProducto { get; set; }

    public int? RecetaIdReceta { get; set; }

    public int? Cantidad { get; set; }

    public virtual Producto? ProductoIdProductoNavigation { get; set; }

    public virtual Recetum? RecetaIdRecetaNavigation { get; set; }
}
