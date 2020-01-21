using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace WeatherRoutingBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private static readonly HttpClient
            Client = new HttpClient(); // needs to only be init once, so need to move out into own base service.

        [Route("login")]
        [HttpGet]
        public async Task<bool> Login(string username, string password)
        {
            return true;
        }

        [Route("Token")]
        [HttpGet]
        public ActionResult GetToken()
        {
            string securityKey = "ergrugfbfuiebfweufwefuasvefuefbaeuvfushfvsdfyef";

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken token = new JwtSecurityToken(
                "me",
                "you",
                expires: DateTime.Now.AddHours(1),
                signingCredentials: signingCredentials
            );

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}