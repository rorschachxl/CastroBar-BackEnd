
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CASTROBAR_API.Utilities
{
    public class TokenAndEncript
    {
        //import config data
        private readonly IConfiguration _config;
        public TokenAndEncript(IConfiguration config) {
            _config = config;
        }
        // hash to password with bcrypt
        public static string HashPass(string password)
        {
            string PassEn = BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt());
            return PassEn;
        }

        // verify pass with Bcrypt 
        public bool VerifyPassword(string password, string hashedPassword)
        {
            if (BCrypt.Net.BCrypt.Verify(password, hashedPassword)==true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Generate token with claims and secret key access
        public string GenerarToken(string CC, string rol)
        {
            var secretKey = _config.GetValue<string>("Key:secretKey");
            var security = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
            new Claim("unique_name", CC), // Cambiado a "unique_name"
            new Claim(ClaimTypes.Role, rol),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(security), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string ObtenerUsuarioDesdeToken(string token)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(token))
                {
                    throw new Exception("El token está vacío o no es válido.");
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                

                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                {
                    throw new Exception("El token no es válido.");
                }

                // Obtener el campo unique_name
                var usuarios = jwtToken.Claims
                    .Where(c => c.Type == "unique_name") // Cambiado a "unique_name"
                    .Select(c => c.Value)
                    .ToList();

                return usuarios.Any() ? string.Join(", ", usuarios) : "Usuario no encontrado";
            }
            catch (Exception ex)
            {
                // Mejor manejo de errores
                throw new Exception($"Error al decodificar el token: {ex.Message}", ex);
            }
        }
    }
}
