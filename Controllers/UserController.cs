using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApplication4.Context;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    [RoutePrefix("api/v1/user")]
    public class UserController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        [Route("list")]
        [AcceptVerbs("GET")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<User> GetUsers()
        {
            IEnumerable<User> users = db.Users.Select(u => u);

            return users;
        }

        [Route("lists")]
        [AcceptVerbs("GET")]
        [Authorize(Roles = "Us")]
        public IEnumerable<User> GetUserss()
        {
            IEnumerable<User> users = db.Users.Select(u => u);

            return users;
        }

        [Route("register")]
        [AcceptVerbs("POST")]
        public User Register([FromBody] User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Permissions = new List<Permission> { new Permission("User") };

            db.Users.Add(user);
            db.SaveChanges();
            return user;
        }
    }
}
