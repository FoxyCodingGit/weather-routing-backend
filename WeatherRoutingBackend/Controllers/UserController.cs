using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
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
            string salt = _context.Salt.FromSqlInterpolated($"EXECUTE dbo.GetUserSalt {loginDetails.UserId}").ToList().ElementAt(0).Salt;
            string hashed = GenerateHash(loginDetails.Password, Convert.FromBase64String(salt));

            if (DoesUserExist(loginDetails.UserId, hashed))
            {
                return Ok("\"" + GenerateToken(loginDetails.UserId) + "\"");
            }
            return Unauthorized();
        }

        [Route("register")]
        [HttpPost]
        public ActionResult Register([FromBody]AuthoriseRequest loginDetails)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string hashed = GenerateHash(loginDetails.Password, salt);

            _ = _context.Database.ExecuteSqlInterpolated($"EXECUTE dbo.CreateUser {loginDetails.UserId}, {hashed}, {Convert.ToBase64String(salt)}"); // this is used for no return values.

            if (DoesUserExist(loginDetails.UserId, hashed))
            {
                return Ok("\"" + GenerateToken(loginDetails.UserId) + "\"");
            }
            return Unauthorized();
        }

        private string GenerateHash(string password, byte[] salt)
        {
            return Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8)
                );
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

            var claims = new List<Claim> { new Claim("Username", username) };

            var token = new JwtSecurityToken(
                "WeatherRoutingBackend",
                "WeatherRoutingFrontend",
                expires: DateTime.Now.AddHours(1),
                signingCredentials: signingCredentials,
                claims: claims
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}