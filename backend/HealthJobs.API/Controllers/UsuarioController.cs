using HealthJobs.Application.Autenticacao.DTOs;
using HealthJobs.Application.Autenticacao.Services;
using HealthJobs.Application.Usuarios.DTOs;
using HealthJobs.Application.Usuarios.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthJobs.API.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;
        private readonly JwtService _jwtService;
        private readonly IConfiguration _configuration;
        public UsuarioController(UsuarioService usuarioService, JwtService jwtService, IConfiguration configuration)
        {
            this._usuarioService = usuarioService;
            this._configuration = configuration;
            this._jwtService = jwtService;
        }

        [AllowAnonymous]
        [HttpPost("cadastrar")]
        public async Task<IActionResult> Cadastrar([FromBody] UsuarioDTO dto)
        {
            var result = await this._usuarioService.Cadastrar(dto);

            if (!result.Result.Succeeded)
                return BadRequest(result.Result.Errors);

            return Ok(await _jwtService.GeraToken(result.User));
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var result = await this._usuarioService.Login(dto);

            if (!result.Result.Succeeded)
                return BadRequest();

            return Ok(await _jwtService.GeraToken(result.User));

        }
    }
}
