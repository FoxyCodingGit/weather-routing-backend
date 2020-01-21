using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WeatherRoutingBackend.DataLayer;
using WeatherRoutingBackend.Model.Request;

namespace WeatherRoutingBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [Route("login")]
        [HttpPost]
        public ActionResult Login(AuthoriseRequest loginDetails)
        {
            if (DoesUserExist(loginDetails.UserId, loginDetails.Password))
            {
                return Ok(GenerateToken(loginDetails.UserId));
            }
            
            throw new Exception("hello");
        }

        private static bool DoesUserExist(string username, string password)
        {
            var context = new DatabaseContext();
            // sql injection attack. Need to check username and password for malicious code.
            var students = context.Users.FromSqlInterpolated($"EXECUTE dbo.IsUserValid {username}, {password}").ToList(); 

            return students.Count > 0;
        }

        private static string GenerateToken(string username)
        {
            string securityKey = "ergrugfbfuiebfweufwefuasvefuefbaeuvfushfvsdfyef";

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

           // var claims = new List<Claim>();
           // //claims.Add(new Claim("Username", username));

            JwtSecurityToken token = new JwtSecurityToken(
                "me",
                "you",
                expires: DateTime.Now.AddHours(1),
                signingCredentials: signingCredentials//,
                //claims: claims
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}