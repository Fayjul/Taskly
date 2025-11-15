using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;
using Taskly.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Taskly.Infrastructure.Helpers
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly IConfiguration _config;
        public JwtGenerator(IConfiguration config) => _config = config;

        public string GenerateToken(User user, out DateTime expiresAt)
        {
            var jwtSection = _config.GetSection("Jwt");
            var secret = jwtSection["Secret"]!;
            var issuer = jwtSection["Issuer"];
            var audience = jwtSection["Audience"];
            var expiryMinutes = int.Parse(jwtSection["ExpiryMinutes"] ?? "60");
            expiresAt = DateTime.UtcNow.AddMinutes(expiryMinutes);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: expiresAt,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
