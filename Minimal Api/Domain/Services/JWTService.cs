using Microsoft.IdentityModel.Tokens;
using MinimalApi.Domain.Entities;
using MinimalApi.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MinimalApi.Domain.Services
{
    public class JWTService : IJWTService
    {
        private readonly IConfiguration _configuration;

        public JWTService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(Administrator administrator)
        {
            string key = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key) + " is null");
            }
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>();
            claims.Add(new Claim("Email", administrator.Email));
            claims.Add(new Claim("Perfil", administrator.Profile.ToString()));
            claims.Add(new Claim(ClaimTypes.Role, administrator.Profile.ToString()));

            var token = new JwtSecurityToken(
                issuer: administrator.Name,
                claims: claims,
                expires: DateTime.Now.AddDays(1.0), 
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
