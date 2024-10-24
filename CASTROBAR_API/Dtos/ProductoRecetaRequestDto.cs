namespace CASTROBAR_API.Dtos
{
    public class ProductoRecetaRequestDto
    {
        public int ProductoIdProducto { get; set; }  // ID del producto
        public int Cantidad { get; set; }             // Cantidad del producto en la receta
    }
}
