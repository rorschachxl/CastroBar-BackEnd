namespace CASTROBAR_API.Dtos
{
    public class ProductoResponseDto
    {
        public int idProducto { get; set; }
        public string nombreProducto { get; set; }
        public string descripcion { get; set; }
        public double precioVenta { get; set; }
        public double precioCompra { get; set; }
        public int cantidad { get; set; }
        public int RecetaIdReceta { get; set; }
        public int EstadoIdEstado { get; set; }
        public int CategoriaIdCategoria { get; set; }
        public int SubcategoriaIdSubcategoria { get; set; }
        public int IdProductoProveedor { get; set; }  
    }
}
