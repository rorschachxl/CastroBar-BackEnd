using System;
using System.Collections.Generic;

namespace CASTROBAR_API.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string? NombreProducto { get; set; }

    public string? DescripcionProducto { get; set; }

    public double PrecioVenta { get; set; }

    public double? PrecioCompra { get; set; }

    public byte[]? Imagen { get; set; }

    public int? Cantidad { get; set; }

    public int? RecetaIdReceta { get; set; }

    public int? EstadoIdEstado { get; set; }

    public int? CategoriaIdCategoria { get; set; }

    public int? SubcategoriaIdSubcategoria { get; set; }

    public virtual Categorium? CategoriaIdCategoriaNavigation { get; set; }

    public virtual Estado? EstadoIdEstadoNavigation { get; set; }

    public virtual ICollection<ProductoOrden> ProductoOrdens { get; set; } = new List<ProductoOrden>();

    public virtual ICollection<ProductoProveedor> ProductoProveedors { get; set; } = new List<ProductoProveedor>();

    public virtual Recetum? RecetaIdRecetaNavigation { get; set; }

    public virtual Subcategorium? SubcategoriaIdSubcategoriaNavigation { get; set; }
}
