﻿namespace CASTROBAR_API.Dtos
{
    public class UsuarioRequestDto
    {
        public string numeroDocumento { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public int idDocumento { get; set; }
        public int idEstado { get; set; }
        public string contraseña { get; set; }
        public int idRol { get; set; }
    }
}