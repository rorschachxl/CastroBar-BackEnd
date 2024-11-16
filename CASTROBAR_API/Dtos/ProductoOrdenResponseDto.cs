namespace CASTROBAR_API.Dtos
{
    public class ProductoOrdenResponseDto
    {
        public int IdProductoOrden { get; set; }
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public int? Cantidad { get; set; }
        public int Estado { get; set; }
        public int PrecioVenta { get; set; }
    }
}
