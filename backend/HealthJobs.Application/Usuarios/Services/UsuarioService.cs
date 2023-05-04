using HealthJobs.Application.Autenticacao.DTOs;
using HealthJobs.Application.Usuarios;
using HealthJobs.Application.Usuarios.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace HealthJobs.Application.Autenticacao.Services
{
    public class UsuarioService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public UsuarioService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._configuration = configuration;
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
            var user = new IdentityUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                EmailConfirmed = false,
            };

            var identityResult = await _userManager.CreateAsync(user, dto.Senha);

            if(identityResult.Succeeded)
                await _userManager.AddToRoleAsync(user, dto.Tipo);

            var result = new CadastrarResult();
            result.User = user;
            result.Result = identityResult;

            return result;
        }
    }
}

