using fin_backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace fin_backend.Data
{
    public static class SeedData
    {
        public static async Task SeedDataAsync(FinDbContext context)
        {
            // Ensure database exists
            await context.Database.EnsureCreatedAsync();

            // Only seed if no users exist
            if (!await context.Users.AnyAsync())
            {
                User user = new User
                {
                    Username = "Peter",
                    Role = "Admin"
                };

                var passwordHash = new PasswordHasher<User>()
                    .HashPassword(user, "Parker");

                user.PasswordHash = passwordHash;

                context.Users.Add(user);

                await context.SaveChangesAsync();
            }
        }
    }
}
