namespace CASTROBAR_API.Dtos
{
    public class UsuarioDto
    {
        public string? NumeroDocumento { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Apellido { get; set; }

        public string? Correo { get; set; }

        public string? Telefono { get; set; }

        public string? Contraseña { get; set; }

        public int Rol { get; set; }

    }
}
