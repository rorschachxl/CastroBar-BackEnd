using CASTROBAR_API.Dtos;
using CASTROBAR_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CASTROBAR_API.Repositories
{
    public class ProductoProveedorRepository: IProductoProveedorRepository
    {
        private readonly DbAadd54CastrobarContext _context;

        public ProductoProveedorRepository(DbAadd54CastrobarContext context)
        {
            _context = context;
        }
        public async Task AgregarProductoProveedorAsync(ProductoProveedorRequestDto productoProveedorDto)
        {
            // Validar si el producto y el proveedor existen
            var producto = await _context.Productos.FindAsync(productoProveedorDto.ProductoIdProducto);
            if (producto == null)
            {
                throw new Exception("El producto no existe.");
            }

            var proveedor = await _context.Proveedors.FindAsync(productoProveedorDto.ProveedorIdProveedor);
            if (proveedor == null)
            {
                throw new Exception("El proveedor no existe.");
            }

            // Crear una nueva instancia de ProductoProveedor
            var productoProveedor = new ProductoProveedor
            {
                ProductoIdProducto = productoProveedorDto.ProductoIdProducto,
                ProveedorIdProveedor = productoProveedorDto.ProveedorIdProveedor
            };

            // Agregar a la base de datos
            _context.ProductoProveedors.Add(productoProveedor);

            // Guardar los cambios
            await _context.SaveChangesAsync();
        }
        public async Task<List<ProductoResponseDto>> ObtenerProductosPorProveedorAsync(int proveedorId)
        {
            var productos = await _context.ProductoProveedors
                .Where(pp => pp.ProveedorIdProveedor == proveedorId)
                .Select(pp => new ProductoResponseDto
                {
                    idProducto = pp.ProductoIdProductoNavigation.IdProducto,
                    nombreProducto = pp.ProductoIdProductoNavigation.NombreProducto,
                    descripcion = pp.ProductoIdProductoNavigation.DescripcionProducto,
                    precioVenta = pp.ProductoIdProductoNavigation.PrecioVenta,
                    precioCompra = (double)pp.ProductoIdProductoNavigation.PrecioCompra,
                    cantidad = (int)pp.ProductoIdProductoNavigation.Cantidad,
                    RecetaIdReceta = pp.ProductoIdProductoNavigation.RecetaIdReceta ?? 0,
                    EstadoIdEstado = pp.ProductoIdProductoNavigation.EstadoIdEstado ?? 0,
                    CategoriaIdCategoria = pp.ProductoIdProductoNavigation.CategoriaIdCategoria ?? 0,
                    SubcategoriaIdSubcategoria = pp.ProductoIdProductoNavigation.SubcategoriaIdSubcategoria ?? 0,
                    IdProductoProveedor = pp.IdProductoProveedor  // Asignar el IdProductoProveedor
                })
                .ToListAsync();

            if (!productos.Any())
            {
                throw new KeyNotFoundException($"No se encontraron productos para el proveedor con ID {proveedorId}.");
            }

            return productos;
        }
        public async Task<bool> EliminarProductoProveedorAsync(int idProductoProveedor)
        {
            var productoProveedor = await _context.ProductoProveedors.FindAsync(idProductoProveedor);

            if (productoProveedor == null)
            {
                return false;
            }

            _context.ProductoProveedors.Remove(productoProveedor);

            await _context.SaveChangesAsync();

            return true; 
        }
    }
}
