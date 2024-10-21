namespace CASTROBAR_API.Dtos
{
    public class RecetaDto
    {
        public string Preparacion { get; set; }
        public List<ProductoRecetaDto> Productos { get; set; } 
    }

    public class ProductoRecetaDto
    {
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
    }
}