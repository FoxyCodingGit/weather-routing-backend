using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherRoutingBackend.DataLayer;
using WeatherRoutingBackend.DataLayer.Models;

namespace WeatherRoutingBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] // https://stackoverflow.com/questions/55781040/401-unauthorized-www-authenticate-bearer
    public class UserDefinedRouteController : ControllerBase // TODO: Make response.
    {
        [HttpGet]
        [Route("get/{username}")]
        public async Task<List<Route>> GetUserRoutes(string username)
        {
            var context = new DatabaseContext();
            // sql injection attack. Need to check username and password for malicious code.
            return context.Routes.FromSqlInterpolated($"EXECUTE dbo.GetUserRoutes {username}").ToList();
        }

        [HttpGet]
        [Route("create/{username}/{routeName}/{modeOfTransport}/{startLat}/{startLng}/{endLat}/{endLng}")]
        public async Task<List<Route>> CreateUserRoute(string username, string routeName, string modeOfTransport, double startLat, double startLng, double endLat, double endLng)
        {
            var context = new DatabaseContext();
            // sql injection attack. Need to check username and password for malicious code.
            return context.Routes.FromSqlInterpolated($"EXECUTE dbo.CreateUserRoute {username} {routeName} {modeOfTransport} {startLat} {startLng} {endLat} {endLng}").ToList(); // TODO: unes call for list as only ever one. Figure out how to not need this bit of code.
        }

        [HttpGet]
        [Route("delete/{username}/{routeId}")]
        public async Task<List<Route>> DeleteUserRoute(string username, int routeId)
        {
            var context = new DatabaseContext();
            // sql injection attack. Need to check username and password for malicious code.
            return context.Routes.FromSqlInterpolated($"EXECUTE dbo.DeleteRoute {username} {routeId}").ToList();
        }

    }
}