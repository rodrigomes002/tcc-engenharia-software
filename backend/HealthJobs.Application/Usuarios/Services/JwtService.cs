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
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),                
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("role", roles[0])
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddHours(double.Parse(_configuration["TokenConfiguration:ExpireHours"]));

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["TokenConfiguration:Issuer"],
                audience: _configuration["TokenConfiguration:Audience"],
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
