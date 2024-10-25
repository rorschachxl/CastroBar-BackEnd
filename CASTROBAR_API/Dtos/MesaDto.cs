using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CASTROBAR_API.Dtos
{
    public class MesaDto
    {
        string? NumMesa{ get; set; }
        List<string>? Productos { get; set; }
        public DateOnly? Fecha { get; set; }
        public TimeOnly? Hora { get; set; }
        public int? MetodoPago { get; set; }
    }
}