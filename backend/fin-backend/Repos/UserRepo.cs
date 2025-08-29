using Microsoft.EntityFrameworkCore;
using fin_backend.Data;
using fin_backend.Models;
using Microsoft.AspNetCore.Identity;

namespace fin_backend.Repos
{
    public class UserRepo : IUserRepo
    {
        private readonly FinDbContext _context;
        public UserRepo(FinDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task AddAsync(User user)
        {
            //I need to add a check to see if the user already exists

            var passwordHasher = new PasswordHasher<User>().HashPassword(user, user.PasswordHash);
            user.PasswordHash = passwordHasher; // Store the hashed password

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
