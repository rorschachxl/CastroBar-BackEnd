
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
            // Obtén la clave secreta correctamente desde la configuración
            var SecretKey = _config.GetSection("Key").GetValue<string>("secretKey");

            // Convierte la clave secreta en un array de bytes
            var security = Encoding.ASCII.GetBytes(SecretKey);

            // Generar los datos del token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]{
            new Claim(ClaimTypes.Name, CC),
            new Claim(ClaimTypes.Role, rol)
        }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(security), SecurityAlgorithms.HmacSha256Signature)
            };

            var TokenHandler = new JwtSecurityTokenHandler();

            // Generar y retornar el token
            var token = TokenHandler.CreateToken(tokenDescriptor);
            return TokenHandler.WriteToken(token);
        }
    }
}
