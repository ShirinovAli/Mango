using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; } = null!;
    }
}
