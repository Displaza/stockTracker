using fin_backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Identity;

namespace fin_backend.Data
{
    public class FinDbContext : DbContext
    {
        public FinDbContext(DbContextOptions<FinDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //User user = new User();
            //// Seed a default user
            //var passwordHash = new PasswordHasher<User>()
            //    .HashPassword(user, "Parker");


            //user.Username = "Peter";
            //user.PasswordHash = passwordHash;
            //user.Role = "Admin";


            //modelBuilder.Entity<User>().HasData(new User
            //{
            //    Id = 1,
            //    Username = "Peter",
            //    PasswordHash = passwordHash,
            //    Role = "Admin"
            //});
        }
    }
}
