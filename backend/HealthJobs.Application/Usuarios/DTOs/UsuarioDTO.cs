using HealthJobs.Domain;

namespace HealthJobs.Application.Autenticacao.DTOs
{
    public class UsuarioDTO
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Tipo { get; set; }
    }
}