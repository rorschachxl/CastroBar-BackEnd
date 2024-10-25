namespace CASTROBAR_API.Dtos
{
    public class ProveedorRequestDto
    {
        public string NumeroDocumento { get; set; } = null!;
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public int? DocumentoIdDocumento { get; set; }
        public int? EstadoIdEstado { get; set; }
    }
}
