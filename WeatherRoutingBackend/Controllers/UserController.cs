using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WeatherRoutingBackend.DataLayer;
using WeatherRoutingBackend.Model.Request;

namespace WeatherRoutingBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly string _securityKey;
        private readonly DatabaseContext _context;

        public UserController(IConfiguration config, DatabaseContext context)
        {
            _securityKey = config.GetValue<string>("AppSettings:SecurityKey");
            _context = context;

        }

        [Route("login")]
        [HttpPost]
        public ActionResult Login([FromBody]AuthoriseRequest loginDetails)
        {
            if (DoesUserExist(loginDetails.UserId, loginDetails.Password))
            {
                return Ok(GenerateToken(loginDetails.UserId));
            }
            return Unauthorized();
        }

        private bool DoesUserExist(string username, string password)
        {
            // sql injection attack. Need to check username and password for malicious code.
            var students = _context.Users.FromSqlInterpolated($"EXECUTE dbo.IsUserValid {username}, {password}").ToList(); 

            return students.Count > 0;
        }

        private string GenerateToken(string username)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_securityKey));

            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

           // var claims = new List<Claim>();
           // //claims.Add(new Claim("Username", username));

            var token = new JwtSecurityToken(
                "WeatherRoutingBackend",
                "WeatherRoutingFrontend",
                expires: DateTime.Now.AddHours(1),
                signingCredentials: signingCredentials//,
                //claims: claims
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}