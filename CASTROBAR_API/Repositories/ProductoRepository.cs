
using CASTROBAR_API.Dtos;
using CASTROBAR_API.Models;
using CASTROBAR_API.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CASTROBAR_API.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly DbAadd54CastrobarContext _context;
        private readonly ProductoUtilities _productoUtilities;

        public ProductoRepository (DbAadd54CastrobarContext context, ProductoUtilities productoUtilities)
        {
            _context = context;
            _productoUtilities = productoUtilities;
        }
        public async Task<IEnumerable<ProductoResponseDto>> ObtenerTodosProductos()
        {
            var productos = await _context.Productos.ToListAsync();
            var productosResponseDto = productos.Select(producto => _productoUtilities.ConvertirADto(producto));
            return productosResponseDto.ToList();
        }

        public async Task<Producto> ObtenerProductoPorIdAsync(int id)
        {
            var producto = await _context.Productos
                .FirstOrDefaultAsync(p => p.IdProducto == id);
            
            return producto;
        }

        public async Task<int> AgregarProductoAsync(ProductoRequestDto productoDto)
        {
            var producto = _productoUtilities.ConvertirEnEntidad(productoDto);
            await _context.Productos.AddAsync(producto);
            await _context.SaveChangesAsync();
            return producto.IdProducto;
        }
        public async Task<int> ActualizarProductoAsync(int id, ProductoRequestDto productoRequestDto)
        {
            var producto = await ObtenerProductoPorIdAsync(id);
            if (producto != null)
            {
                producto.NombreProducto = !string.IsNullOrEmpty(productoRequestDto.nombreProducto) ? productoRequestDto.nombreProducto : producto.NombreProducto;
                producto.DescripcionProducto = !string.IsNullOrEmpty(productoRequestDto.descripcion) ? productoRequestDto.descripcion : producto.DescripcionProducto;
                producto.PrecioVenta = (double)(productoRequestDto.precioVenta != 0 ? productoRequestDto.precioVenta : producto.PrecioVenta);
                producto.PrecioCompra = productoRequestDto.precioCompra ?? producto.PrecioCompra;
                producto.Cantidad = productoRequestDto.cantidad ?? producto.Cantidad;

                
                if (productoRequestDto.RecetaIdReceta.HasValue)
                {
                    if (await _context.Receta.FindAsync(productoRequestDto.RecetaIdReceta.Value) != null)
                    {
                        producto.RecetaIdReceta = productoRequestDto.RecetaIdReceta.Value;
                    }
                    else
                    {
                        return 500;
                    }
                }
                else
                {
                    producto.RecetaIdReceta = 1; 
                }
                if (productoRequestDto.EstadoIdEstado != 0) 
                    producto.EstadoIdEstado = productoRequestDto.EstadoIdEstado;

                if (productoRequestDto.CategoriaIdCategoria != 0) // o puedes usar !productoRequestDto.CategoriaIdCategoria.HasValue
                    producto.CategoriaIdCategoria = productoRequestDto.CategoriaIdCategoria;

                if (productoRequestDto.SubcategoriaIdSubcategoria != 0) // o puedes usar !productoRequestDto.SubcategoriaIdSubcategoria.HasValue
                    producto.SubcategoriaIdSubcategoria = productoRequestDto.SubcategoriaIdSubcategoria;
                _context.Productos.Update(producto);
                int filas = await _context.SaveChangesAsync();
                return filas;
            }
            else
            {
                return 500;
            }
        }
        public async Task<int> BorrarProductoAsync(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
                return 200;
            }
            else
            {
                return 500;
            }
        }
    }
}
