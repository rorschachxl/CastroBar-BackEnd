using CASTROBAR_API.Models;
using CASTROBAR_API.Dtos;

namespace CASTROBAR_API.Services
{
    public class ProductoService
    {
        private readonly ProductoService _productoService;

        public ProductoResponseDto convertirADto (Producto producto)
        {
            return new ProductoResponseDto{
                idProducto = producto.IdProducto,
                nombreProducto = producto.NombreProducto,
                descripcion = producto.DescripcionProducto,
                precioVenta = producto.PrecioVenta,
                precioCompra = (double)producto.PrecioCompra,
                cantidad = (int)producto.Cantidad, 
                RecetaIdReceta = (int)producto.RecetaIdReceta,
                EstadoIdEstado = (int)producto.EstadoIdEstado,
                CategoriaIdCategoria = (int)producto.CategoriaIdCategoria,
                SubcategoriaIdSubcategoria = (int)producto.CategoriaIdCategoria

            };
        }
        public Producto ConvertirEnEntidad(ProductoRequestDto productoRequestDto)
        {
            return new Producto
            {
                NombreProducto = productoRequestDto.nombreProducto,
                DescripcionProducto = productoRequestDto.descripcion,
                PrecioVenta = productoRequestDto.precioVenta,
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
