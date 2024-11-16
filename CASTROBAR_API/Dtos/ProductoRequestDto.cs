using System.Security.Cryptography.X509Certificates;

namespace CASTROBAR_API.Dtos
{
    public class ProductoRequestDto
    {
        public string? nombreProducto { get; set; }
        public string? descripcion { get; set; }
        public double? precioVenta { get; set; } // Hacerlo opcional si se desea
        public double? precioCompra { get; set; } // Hacerlo opcional si se desea
        public int? cantidad { get; set; } // Hacerlo opcional si se desea
        public int RecetaIdReceta { get; set; } = 1; // Valor por defecto
        public int EstadoIdEstado { get; set; }
        public int CategoriaIdCategoria { get; set; } = 1; // Valor por defecto
        public int SubcategoriaIdSubcategoria { get; set; } = 1;
    }
}
