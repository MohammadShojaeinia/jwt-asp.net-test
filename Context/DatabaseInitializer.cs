using System.Collections.Generic;
using System.Data.Entity;
using WebApplication4.Models;

namespace WebApplication4.Context
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            base.Seed(context);

            Permission p1 = new Permission("Admin");
            Permission p2 = new Permission("Manager");
            Permission p3 = new Permission("User");

            Book book = new Book();
            book.Title = "folan o bisar";

            User user = new User();
            user.Name = "Writer";
            user.Book = book;
            user.Username = "writer123";
            user.Password = BCrypt.Net.BCrypt.HashPassword("123123");
            user.Permissions = new List<Permission> { p2, p3, p1 };

            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}