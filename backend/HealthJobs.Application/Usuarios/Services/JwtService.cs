using HealthJobs.Application.Autenticacao.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HealthJobs.Application.Usuarios.Services
{
    public class JwtService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        public JwtService(IConfiguration configuration, UserManager<IdentityUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<UsuarioToken> GeraToken(IdentityUser user)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var jwtAudience = env == "Production" ? Environment.GetEnvironmentVariable("Audience") : _configuration["TokenConfiguration:Audience"];
            var jwtIssuer = env == "Production" ? Environment.GetEnvironmentVariable("Issuer") : _configuration["TokenConfiguration:Issuer"];
            var jwtKey = env == "Production" ? Environment.GetEnvironmentVariable("JwtKey") : _configuration["Jwt:Key"];
            var jwtExpires = env == "Production" ? Environment.GetEnvironmentVariable("ExpireHours") : _configuration["TokenConfiguration:ExpireHours"];

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("role", roles[0])
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddHours(double.Parse(jwtExpires));

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
                );

            return new UsuarioToken()
            {
                Authenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
                Message = "Token gerado."
            };
        }
    }
}
