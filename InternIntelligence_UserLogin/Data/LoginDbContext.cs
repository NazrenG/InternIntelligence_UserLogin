using InternIntelligence_UserLogin.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InternIntelligence_UserLogin.Data
{
    public class LoginDbContext : IdentityDbContext<User>
    {
        public LoginDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
