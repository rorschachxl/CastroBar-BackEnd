namespace CASTROBAR_API.Dtos
{
    public class ProductoOrdenRequestDto
    {
        public int OrdenIdOrden { get; set; }
        public int ProductoIdProducto { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
    }
}
