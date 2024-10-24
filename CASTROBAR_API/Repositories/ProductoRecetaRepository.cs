using CASTROBAR_API.Dtos;
using CASTROBAR_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CASTROBAR_API.Repositories
{
    public class ProductoRecetaRepository : IProductoRecetaRepository
    {
        private readonly DbAadd54CastrobarContext _context;

        public ProductoRecetaRepository(DbAadd54CastrobarContext context)
        {
            _context = context;
        }
        public async Task AgregarProductosAReceta(int recetaId, List<ProductoRecetaRequestDto> productos)
        {

            {
                foreach (var productoDto in productos)
                {
                    var productoReceta = new ProductoRecetum
                    {
                        ProductoIdProducto = productoDto.ProductoIdProducto,
                        RECETAIdReceta = recetaId,  // Asignar el ID de la receta
                        Cantidad = productoDto.Cantidad,
                    };

                    _context.ProductoReceta.Add(productoReceta); // Agregar a la tabla ProductoReceta
                }

                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<ProductoRecetaResponseDto>> ObtenerProductosPorRecetaId(int idReceta)
        {
            return await _context.ProductoReceta
                .Where(pr => pr.RECETAIdReceta == idReceta)
                .Select(pr => new ProductoRecetaResponseDto
                {
                    idProductoReceta = (int)pr.IdProductoReceta,
                    ProductoIdProducto = pr.ProductoIdProducto ?? 0, // Manejar posibles valores null
                    Cantidad = pr.Cantidad ?? 0 // Manejar posibles valores null
                })
                .ToListAsync();
        }
        public async Task<bool> EliminarProductoRecetaPorId(int idProductoReceta)
        {
            var productoReceta = await _context.ProductoReceta.FindAsync(idProductoReceta);
            if (productoReceta == null)
            {
                return false; // No se encontró el producto de la receta
            }

            _context.ProductoReceta.Remove(productoReceta);
            await _context.SaveChangesAsync();
            return true; // Eliminación exitosa
        }
    }
}
