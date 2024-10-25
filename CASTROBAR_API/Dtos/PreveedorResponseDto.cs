namespace CASTROBAR_API.Dtos
{
    public class PreveedorResponseDto
    {
        public int IdProveedor { get; set; }
        public string NumeroDocumento { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
    }
}
