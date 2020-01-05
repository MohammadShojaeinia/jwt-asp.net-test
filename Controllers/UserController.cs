using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using WebApplication4.Context;
using WebApplication4.Models;
using WebApplication4.Models.Auth;

namespace WebApplication4.Controllers
{
    [RoutePrefix("api/v1/user")]
    public class UserController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        [Route("register")]
        [AcceptVerbs("POST")]
        public User Register([FromBody] User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Permissions = db.Permissions.Where(p => p.Name == "User").ToArray();

            db.Users.Add(user);
            db.SaveChanges();
            return user;
        }

        [Route("login")]
        [AcceptVerbs("POST")]
        public IHttpActionResult Authenticate([FromBody] LoginRequest login)
        {
            var loginResponse = new LoginResponse { };

            IHttpActionResult response;
            HttpResponseMessage responseMsg = new HttpResponseMessage();
            User user = null;

            if (login != null)
            {
                user = db.Users.Where(u => u.Username == login.Username).FirstOrDefault();
            }

            if (user != null)
            {
                AuthToken token = createToken(user);

                return Ok<AuthToken>(token);
            }
            else
            {
                loginResponse.responseMsg.StatusCode = HttpStatusCode.NotFound;
                response = ResponseMessage(loginResponse.responseMsg);

                return response;
            }
        }

        private AuthToken createToken(User user)
        {
            DateTime issuedAt = DateTime.UtcNow;
            DateTime expires = DateTime.UtcNow.AddDays(90);

            var tokenHandler = new JwtSecurityTokenHandler();

            List<Claim> claims = new List<Claim>();
            claims.AddRange(GetUserPermissions(user.Permissions));

            const string sec = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
            //var now = DateTime.UtcNow;
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);

            var signedToken = new JwtSecurityToken(
                notBefore: issuedAt,
                expires: expires,
                signingCredentials: signingCredentials,
                claims: claims
            );

            AuthToken token = new AuthToken
            {
                Token = "Bearer " + tokenHandler.WriteToken(signedToken)
            };

            return token;
        }

        private List<Claim> GetUserPermissions(IEnumerable<Permission> permissions)
        {
            List<Claim> result = new List<Claim>();

            foreach (Permission p in permissions)
            {
                result.Add(new Claim(ClaimTypes.Role, p.Name));
            }

            return result;
        }

        [Route("list")]
        [AcceptVerbs("GET")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<User> GetUsers()
        {
            IEnumerable<User> users = db.Users.Include("Permissions").ToList();

            return users;
        }

        [Route("lists")]
        [AcceptVerbs("GET")]
        [Authorize(Roles = "Us")]
        public IEnumerable<User> GetUserss()
        {
            IEnumerable<User> users = db.Users.Include("Permissions").ToList();

            return users;
        }
    }
}
