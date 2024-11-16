using CASTROBAR_API.Models;
using CASTROBAR_API.Dtos;

namespace CASTROBAR_API.Utilities
{
    public class ProductoUtilities
    {
      
         private readonly ProductoUtilities _productoUtilities;

        public ProductoResponseDto ConvertirADto(Producto producto)
        {
            if (producto == null)
            {
                throw new ArgumentNullException(nameof(producto), "El producto no puede ser nulo.");
            }

            return new ProductoResponseDto
            {
                idProducto = producto.IdProducto,
                nombreProducto = producto.NombreProducto,
                descripcion = producto.DescripcionProducto,
                precioVenta = producto.PrecioVenta,
                precioCompra = producto.PrecioCompra.HasValue ? (double)producto.PrecioCompra.Value : 0.0,
                cantidad = producto.Cantidad.HasValue ? (int)producto.Cantidad.Value : 0,
                RecetaIdReceta = producto.RecetaIdReceta.HasValue ? (int)producto.RecetaIdReceta.Value : 0,
                EstadoIdEstado = producto.EstadoIdEstado.HasValue ? (int)producto.EstadoIdEstado.Value : 0,
                CategoriaIdCategoria = producto.CategoriaIdCategoria.HasValue ? (int)producto.CategoriaIdCategoria.Value : 0,
                SubcategoriaIdSubcategoria = producto.SubcategoriaIdSubcategoria.HasValue ? (int)producto.SubcategoriaIdSubcategoria.Value : 0
            };
        }
        public Producto ConvertirEnEntidad(ProductoRequestDto productoRequestDto)
        {
            return new Producto
            {
                NombreProducto = productoRequestDto.nombreProducto,
                DescripcionProducto = productoRequestDto.descripcion,
                PrecioVenta = (double)productoRequestDto.precioVenta,
                PrecioCompra = productoRequestDto.precioCompra,
                Cantidad = productoRequestDto.cantidad,
                RecetaIdReceta = productoRequestDto.RecetaIdReceta,
                EstadoIdEstado = productoRequestDto.EstadoIdEstado,
                CategoriaIdCategoria = productoRequestDto.CategoriaIdCategoria,
                SubcategoriaIdSubcategoria = productoRequestDto.SubcategoriaIdSubcategoria,
            };
        }
        
    }
}
