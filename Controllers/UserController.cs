using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApplication4.Context;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class UserController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        [Route("api/v1/user/list")]
        [AcceptVerbs("GET")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<User> GetUsers()
        {
            IEnumerable<User> users = db.Users.Select(u => u);

            return users;
        }

        [Route("api/v1/user/register")]
        [AcceptVerbs("POST")]
        public User Register([FromBody] User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            db.Users.Add(user);
            db.SaveChanges();
            return user;
        }
    }
}
