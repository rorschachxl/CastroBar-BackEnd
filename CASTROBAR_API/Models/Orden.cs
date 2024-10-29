using System;
using System.Collections.Generic;

namespace CASTROBAR_API.Models;

public partial class Orden
{
    public int IdOrden { get; set; }

    public int? EstadoIdEstado { get; set; }

    public DateOnly? Fecha { get; set; }

    public TimeOnly? Hora { get; set; }

    public int? MetodoPagoIdMetodoPago { get; set; }

    public int? UsuarioIdUsuario { get; set; }

    public int? MesaNumeroMesa { get; set; }  // Añadir esta propiedad

    public virtual Estado? EstadoIdEstadoNavigation { get; set; }

    public virtual MetodoPago? MetodoPagoIdMetodoPagoNavigation { get; set; }

    public virtual Usuario? UsuarioIdUsuarioNavigation { get; set; }

    public virtual ICollection<ProductoOrden> ProductoOrdens { get; set; } = new List<ProductoOrden>();
}

