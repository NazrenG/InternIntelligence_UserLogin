using Microsoft.AspNetCore.Identity;

namespace InternIntelligence_UserLogin.Models
{
    public class User:IdentityUser
    {
        public string? Fullname { get; set; }
    }
}
