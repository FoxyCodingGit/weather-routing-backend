using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WeatherRoutingBackend.Model.Route;

namespace WeatherRoutingBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] // https://stackoverflow.com/questions/55781040/401-unauthorized-www-authenticate-bearer
    public class RoutingController : BaseController<RouteResponse>
    {
        private readonly string _routingKey;

        public RoutingController(IConfiguration config)
        {
            _routingKey = config.GetValue<string>("AppSettings:RoutingKey");
        }


        [HttpGet]
        [Route("{travelMode}/{numberOfAlternates}/{startLat}/{startLng}/{endLat}/{endLng}")]
        public async Task<PointsTimeAndDistance[]> GetRouteLegsAndTime(string travelMode, int numberOfAlternates, double startLat, double startLng, double endLat, double endLng)
        {

            var url = $"https://api.tomtom.com/routing/1/calculateRoute/{startLat},{startLng}:{endLat},{endLng}/json" +
                      $"?travelMode={travelMode}&maxAlternatives={numberOfAlternates}&key={_routingKey}";

            var routeResponse = await GetResponse(url);

            PointsTimeAndDistance[] routes = new PointsTimeAndDistance[routeResponse.Routes.ToArray().Length];

            for (int i = 0; i < routeResponse.Routes.ToArray().Length; i++)
            {
                var justPoints = routeResponse.Routes.ToList()[i].Legs.ToList()[0].Points; // DONT KNOW HOW MAXALT WORKS. IS IT A ROUTE OR A LEG????

                routes[i] = new PointsTimeAndDistance
                {
                    Points = justPoints,
                    TravelTimeInSeconds = routeResponse.Routes.ToList()[i].Summary.TravelTimeInSeconds,
                    Distance = routeResponse.Routes.ToList()[i].Summary.LengthInMeters
                };
            }

            return routes;
        }
    }
}
