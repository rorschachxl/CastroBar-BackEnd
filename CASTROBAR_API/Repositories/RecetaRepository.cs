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

        public async Task AgregarRecetaConProductosAsync(RecetaDto recetaDto)
        {
            var nuevaReceta = ConvertirDtoAReceta(recetaDto);
            _context.Receta.Add(nuevaReceta);

            try
            {
                await _context.SaveChangesAsync(); 

                foreach (var productoDto in recetaDto.Productos)
                {
                    var productoReceta = new ProductoRecetum
                    {
                        RECETAIdReceta = nuevaReceta.IdReceta, 
                        ProductoIdProducto = productoDto.IdProducto,
                        Cantidad = productoDto.Cantidad
                    };

                    _context.ProductoReceta.Add(productoReceta);
                }

                await _context.SaveChangesAsync(); 
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException;
                Console.WriteLine(innerException?.Message);
                throw; 
            }
        }

        private Recetum ConvertirDtoAReceta(RecetaDto recetaDto)
        {
            return new Recetum
            {
                Preparacion = recetaDto.Preparacion
            };
        }
    }
}
