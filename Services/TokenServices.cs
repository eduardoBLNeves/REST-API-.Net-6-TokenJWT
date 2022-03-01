using API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Services
{
    public class TokenServices
    {
        public string GerarToken(Usuarios usuario, bool manterLogin)
        {
            var expiresToken = manterLogin ? DateTime.UtcNow.AddDays(30) : DateTime.UtcNow.AddHours(2);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.GetSecret());
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = expiresToken,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", usuario.Usuario_Id.ToString()),
                    new Claim(ClaimTypes.Name, usuario.nome),
                })
            };

            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            return token;
        }
    }
}
