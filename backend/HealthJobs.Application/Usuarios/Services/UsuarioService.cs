using HealthJobs.Application.Autenticacao.DTOs;
using HealthJobs.Application.Usuarios;
using HealthJobs.Application.Usuarios.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HealthJobs.Application.Autenticacao.Services
{
    public class UsuarioService
    {
        private readonly ILogger<UsuarioService> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IConfiguration _configuration;

        public UsuarioService(ILogger<UsuarioService> logger, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this._logger = logger;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._configuration = configuration;
            this._roleManager = roleManager;
        }

        public async Task<LoginResult> Login(LoginDTO dto)
        {
            var signIn = await _signInManager.PasswordSignInAsync(dto.Email,
                 dto.Senha, isPersistent: false, lockoutOnFailure: false);
            var usuario = await _signInManager.UserManager.FindByEmailAsync(dto.Email);

            var result = new LoginResult();
            result.Result = signIn;
            result.User = usuario;

            return result;

        }

        public async Task<CadastrarResult> Cadastrar(UsuarioDTO dto)
        {
            _logger.LogInformation("Cadastrando usuário...");
            var user = new IdentityUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                EmailConfirmed = false,

            };

            var identityResult = await _userManager.CreateAsync(user, dto.Senha);

            if (identityResult.Succeeded)
            {
                var role = new IdentityRole
                {
                    Name = dto.Tipo
                };
                
                var roleDb = await _roleManager.GetRoleNameAsync(role);
                if(roleDb != role.Name)
                    await _roleManager.CreateAsync(role);

                await _userManager.AddToRoleAsync(user, role.Name);
            }

            var result = new CadastrarResult();
            result.User = user;
            result.Result = identityResult;

            return result;
        }
    }
}

