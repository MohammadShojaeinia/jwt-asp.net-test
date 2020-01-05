using System.Data.Entity;
using WebApplication4.Models;

namespace WebApplication4.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("DefaultConnection") { }
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Permission> Permissions { get; set; }
    }
}