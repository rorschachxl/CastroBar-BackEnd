namespace CASTROBAR_API.Utilities
{
    public class ByCriptUtility
    {
        public string HashPassword(string password)
        {
            // Hash con un salt automático generado por BCrypt
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        // Método para verificar si la contraseña coincide con el hash almacenado
        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
