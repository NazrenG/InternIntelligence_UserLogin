using InternIntelligence_UserLogin.Data;
using InternIntelligence_UserLogin.Models;
using Microsoft.EntityFrameworkCore;

namespace InternIntelligence_UserLogin.Services
{
    public class UserService
    {

        private readonly LoginDbContext _context;

        public UserService(LoginDbContext context)
        {
            _context = context;
        }
        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<User> GetByIdUser(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
