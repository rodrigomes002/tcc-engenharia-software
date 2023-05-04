using Microsoft.AspNetCore.Identity;

namespace HealthJobs.Application.Usuarios
{
    public class CadastrarResult
    {
        public IdentityUser User { get; set; }
        public IdentityResult Result { get; set; }
    }
}
