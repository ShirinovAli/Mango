using Mango.Services.AuthAPI.Entities;

namespace Mango.Services.AuthAPI.Services
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser user);
    }
}
