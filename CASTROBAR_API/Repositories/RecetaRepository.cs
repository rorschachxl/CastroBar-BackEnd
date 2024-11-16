using CASTROBAR_API.Models;
using CASTROBAR_API.Utilities;
using CASTROBAR_API.Dtos;
using Microsoft.EntityFrameworkCore;


namespace CASTROBAR_API.Repositories
{
    public class RecetaRepository : IRecetaRepository
    {
        private readonly DbAadd54CastrobarContext _context;

        public RecetaRepository(DbAadd54CastrobarContext context)
        {
            _context = context;
        }

        public async Task<int> AgregarRecetaAsync(RecetaRequestDto recetaRequest)
        {
            var receta = new Recetum
            {
                Preparacion = recetaRequest.Preparacion,
            };
            _context.Receta.Add(receta);
            await _context.SaveChangesAsync();
            return receta.IdReceta;
        }
        public async Task<List<RecetaResponseDto>> ObtenerTodasLasRecetas()
        {
            return await _context.Receta
                .Select(r => new RecetaResponseDto
                {
                    IdReceta = r.IdReceta,
                    Preparacion = r.Preparacion
                })
                .ToListAsync();
        }
        public async Task EliminarRecetaYActualizarProductos(int idReceta)
        {
            // Obtener todos los productos relacionados con la receta
            var productosReceta = await _context.ProductoReceta
                .Where(pr => pr.RECETAIdReceta == idReceta)
                .ToListAsync();

            await CambiarRecetaIdArounullPorIdReceta(idReceta);
           

            // Ahora eliminar todos los registros de ProductoReceta relacionados
            if (productosReceta.Any())
            {
                _context.ProductoReceta.RemoveRange(productosReceta);
                // Guardar cambios después de eliminar ProductoReceta
                await _context.SaveChangesAsync();
            }

            // Ahora eliminar la receta
            var receta = await _context.Receta.FindAsync(idReceta);
            if (receta == null)
            {
                throw new Exception("La receta no existe.");
            }

            _context.Receta.Remove(receta);

            // Guardar todos los cambios finales en la base de datos
            await _context.SaveChangesAsync(); // Guardar cambios en la eliminación de la receta
        }
        private async Task CambiarRecetaIdArounullPorIdReceta(int idReceta)
        {
            // Obtener todos los productos que tienen el RecetaIdReceta igual al idReceta proporcionado
            var productos = await _context.Productos
                .Where(p => p.RecetaIdReceta == idReceta)
                .ToListAsync();

            // Actualizar RecetaIdReceta a null
            foreach (var producto in productos)
            {
                producto.RecetaIdReceta = null; // Establecer RecetaIdReceta a null
            }

            // Guardar cambios en la base de datos
            await _context.SaveChangesAsync();
        }

    }
}
