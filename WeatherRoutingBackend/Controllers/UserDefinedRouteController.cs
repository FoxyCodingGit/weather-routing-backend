using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherRoutingBackend.DataLayer;
using WeatherRoutingBackend.DataLayer.Models;

namespace WeatherRoutingBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] // https://stackoverflow.com/questions/55781040/401-unauthorized-www-authenticate-bearer
    public class UserDefinedRouteController : BaseController<Route> // TODO: Make response.
    {
        [HttpGet]
        [Route("get/{username}")]
        public async Task<List<Route>> GetUserRoutes(string username)
        {
            var context = new DatabaseContext();
            // sql injection attack. Need to check username and password for malicious code.
            return context.Routes.FromSqlInterpolated($"EXECUTE dbo.GetUserRoutes {username}").ToList();
        }

        //[HttpGet]
        //[Route("{lat}/{lng}")]
        //public async Task<Route> SaveUserRoute(double lat, double lng)
        //{
        //    var url = $"https://api.darksky.net/forecast/{_weatherKey}/{lat},{lng}?units=si";
        //    return await GetResponse(url);

        //    return weatherResponse.Currently.PrecipProbability;
        //}
    }
}