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

        [Route("")]
        [AcceptVerbs("GET")]
        [Authorize]
        public IEnumerable<User> GetUsers()
        {
            IEnumerable<User> users = db.Users.Select(u => u);

            return users;
        }
    }
}
