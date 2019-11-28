using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication4.Models;

namespace WebApplication4.Context
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            base.Seed(context);

            Book book = new Book();
            book.Title = "folan o bisar";

            User user = new User();
            user.Name = "Mahsa";
            user.Book = book;
            user.Username = "mahsa123";
            user.Password = "123123";

            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}