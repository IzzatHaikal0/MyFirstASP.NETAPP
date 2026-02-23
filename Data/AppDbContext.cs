using Microsoft.EntityFrameworkCore;
using MyMvcApp.Models; // This lets us see your User class

namespace MyMvcApp.Data
{
    // Inheriting from "DbContext" is what gives this class its database superpowers
    public class AppDbContext : DbContext
    {
        // This constructor passes the configuration (like the connection string) to the base EF Core class
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // IMPORTANT: Every DbSet you add here becomes a literal TABLE in your database!
        public DbSet<User> Users { get; set; }

        public DbSet<Sport> Sports { get; set; }
    }
}