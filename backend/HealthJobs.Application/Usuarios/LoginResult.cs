using Microsoft.AspNetCore.Identity;

namespace HealthJobs.Application.Usuarios
{
    public class LoginResult
    {
        public IdentityUser User { get; set; }
        public SignInResult Result { get; set; }
    }
}
