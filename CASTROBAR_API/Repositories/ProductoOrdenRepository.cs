using CASTROBAR_API.Dtos;
using CASTROBAR_API.Models;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using System.Linq;
using System.Threading.Tasks;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CASTROBAR_API.Repositories
{
    public class ProductoOrdenRepository : IProductoOrdenRepository
    {
        private const string V = "Efectivo";
        private readonly DbAadd54CastrobarContext _context;

        public ProductoOrdenRepository(DbAadd54CastrobarContext context)
        {
            _context = context;
        }

        // Función para agregar un ProductoOrden
        public async Task<int> AgregarProductoOrdenAsync(ProductoOrdenRequestDto productoOrdenRequest)
        {
            var nuevoProductoOrden = new ProductoOrden
            {
                OrdenIdOrden = productoOrdenRequest.OrdenIdOrden,
                ProductoIdProducto = productoOrdenRequest.ProductoIdProducto,
                Descripcion = productoOrdenRequest.Descripcion,
                Cantidad = productoOrdenRequest.Cantidad
            };

            _context.ProductoOrdens.Add(nuevoProductoOrden);
            await _context.SaveChangesAsync();
            return nuevoProductoOrden.IdProductoOrden; // Retorna el Id generado
        }

        // Función para obtener productos por ID de orden
        public async Task<List<ProductoOrdenResponseDto>> ObtenerProductosPorOrdenIdAsync(int ordenId)
        {
            try
            {
                return await _context.ProductoOrdens
                    .Where(po => po.OrdenIdOrden == ordenId)
                    .Select(po => new ProductoOrdenResponseDto
                    {
                        IdProductoOrden = po.IdProductoOrden,
                        NombreProducto = po.ProductoIdProductoNavigation != null ? po.ProductoIdProductoNavigation.NombreProducto ?? string.Empty : string.Empty,
                        Descripcion = po.Descripcion ?? string.Empty,
                        Cantidad = po.Cantidad, // Asegúrate de incluir el estado
                        PrecioVenta = (int)po.ProductoIdProductoNavigation.PrecioVenta // Incluye el precio de venta también
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Registra el error para obtener más detalles
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
        public async Task<byte[]> GenerarFacturaPdfAsync(int ordenId)
        {
            // Obtener los productos de la orden
            var productosOrden = await _context.ProductoOrdens
                .Where(po => po.OrdenIdOrden == ordenId)
                .Select(po => new
                {
                    po.ProductoIdProductoNavigation.NombreProducto,
                    po.Cantidad,
                    po.ProductoIdProductoNavigation.PrecioVenta,
                })
                .ToListAsync();

            // Obtener los datos de la orden
            var orden = await _context.Ordens
                .FirstOrDefaultAsync(o => o.IdOrden == ordenId);

            if (orden == null)
                throw new Exception("Orden no encontrada.");

            // Calcular subtotal y total
            decimal subtotal = (decimal)productosOrden.Sum(po => po.Cantidad * po.PrecioVenta);
            decimal iva = subtotal * 0.19m; // 19% IVA
            decimal total = subtotal + iva;

            // Crear el documento PDF usando QuestPDF
            var document = Document.Create(container =>
            {
                container
                    .Page(page =>
                    {
                        page.Margin(20);
                        page.Content().Column(column =>
                        {
                            column.Spacing(10);

                            // Encabezado del restaurante
                            column.Item().Row(row =>
                            {
                                row.RelativeItem().Column(header =>
                                {
                                    header.Item().Text("CASTROBAR").FontSize(24).Bold();
                                    header.Item().Text("Av. Principal, No. 123, Ciudad");
                                    header.Item().Text("Teléfono: (+123) 456-7890");
                                    header.Item().Text("Correo: castrobar@gmail.com");
                                });
                            });

                            column.Item().LineHorizontal(1);

                            // Información de la factura
                            column.Item().Text("Factura de Consumo").FontSize(20).Bold().AlignCenter();
                            column.Item().Text($"Número de Factura: {ordenId}");
                            column.Item().Text($"Fecha: {DateTime.Now:dd/MM/yyyy}");
                            column.Item().Text($"Número de Mesa: {orden.MesaNumeroMesa}");
                            column.Item().Text($"Atendido por: ");

                            column.Item().LineHorizontal(1);

                            // Tabla de productos
                            column.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(); // Producto
                                    columns.RelativeColumn(); // Cantidad
                                    columns.RelativeColumn(); // Precio Unitario
                                    columns.RelativeColumn(); // Subtotal
                                });

                                // Encabezado de la tabla
                                table.Header(header =>
                                {
                                    header.Cell().Text("Producto").Bold();
                                    header.Cell().Text("Cantidad").Bold();
                                    header.Cell().Text("Precio Unitario").Bold();
                                    header.Cell().Text("Subtotal").Bold();
                                });

                                // Filas de la tabla
                                foreach (var producto in productosOrden)
                                {
                                    decimal subtotalProducto = (decimal)(producto.Cantidad * producto.PrecioVenta);

                                    table.Cell().Text(producto.NombreProducto);
                                    table.Cell().Text(producto.Cantidad.ToString());
                                    table.Cell().Text($"${producto.PrecioVenta:F2}");
                                    table.Cell().Text($"${subtotalProducto:F2}");
                                }
                            });

                            column.Item().LineHorizontal(1);

                            // Subtotales y total
                            column.Item().AlignRight().Column(totals =>
                            {
                                totals.Item().Text($"Subtotal: ${subtotal:F2}");
                                totals.Item().Text($"IVA (19%): ${iva:F2}");
                                totals.Item().Text($"Total: ${total:F2}").Bold().FontSize(16);
                            });

                            column.Item().LineHorizontal(1);

                            // Método de pago y mensaje final
                            column.Item().Text($"Método de Pago: {orden.MetodoPagoIdMetodoPago}"); // Asume que hay un campo MetodoPago
                            column.Item().AlignCenter().Text("Gracias por su visita. ¡Esperamos verle pronto!").FontSize(12).Italic();
                        });
                    });
            });

            // Guardar el documento en un flujo de memoria
            using (var memoryStream = new MemoryStream())
            {
                document.GeneratePdf(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }

}
