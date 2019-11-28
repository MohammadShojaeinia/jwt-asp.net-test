using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication4.Models;

namespace WebApplication4.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("DefaultConnection") { }
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }

    }
}