using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WeatherRoutingBackend.Model.Route;

namespace WeatherRoutingBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoutingController : BaseController<RouteResponse>
    {
        private readonly string _routingKey;

        public RoutingController(IConfiguration config)
        {
            _routingKey = config.GetValue<string>("AppSettings:RoutingKey");
        }


        [HttpGet]
        [Route("{travelMode}/{numberOfAlternates}/{startLat}/{startLng}/{endLat}/{endLng}")]
        public async Task<List<UsefulRouteResponse>> GetRoute(string travelMode, int numberOfAlternates, double startLat, double startLng, double endLat, double endLng)
        {
            var url = $"https://api.tomtom.com/routing/1/calculateRoute/{startLat},{startLng}:{endLat},{endLng}/json" +
                      $"?travelMode={travelMode}&maxAlternatives={numberOfAlternates}&key={_routingKey}";
            var routeResponse = await GetResponse(url);

            return Thinergner(routeResponse);
        }

        private List<UsefulRouteResponse> Thinergner(RouteResponse routeResponse)
        {
            var pointsTimeAndDistance = new List<UsefulRouteResponse>();

            foreach (var route in routeResponse.Routes)
            {
                pointsTimeAndDistance.Add(new UsefulRouteResponse
                {
                    Points = route.Legs[0].Points,
                    TravelTimeInSeconds = route.Summary.TravelTimeInSeconds,
                    Distance = route.Summary.LengthInMeters
                });
            }

            return pointsTimeAndDistance;
        }
    }
}
