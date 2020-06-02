using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WeatherRoutingBackend.Model.Response;
using WeatherRoutingBackend.Model.Route;

namespace WeatherRoutingBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoutingController : BaseController<RouteResponse>
    {
        private readonly string _routingKey;
        private readonly string _googleKey;

        public RoutingController(IConfiguration config)
        {
            _routingKey = config.GetValue<string>("AppSettings:RoutingKey");
            _googleKey = config.GetValue<string>("AppSettings:GoogleKey");
        }

        [HttpGet]
        [Route("{travelMode}/{numberOfAlternates}/{startLat}/{startLng}/{endLat}/{endLng}")]
        public async Task<List<UsefulRouteResponse>> GetRoute(string travelMode, int numberOfAlternates, double startLat, double startLng, double endLat, double endLng)
        {
            var url = $"https://api.tomtom.com/routing/1/calculateRoute/{startLat},{startLng}:{endLat},{endLng}/json" +
                      $"?travelMode={travelMode.ToLower()}&maxAlternatives={numberOfAlternates}&key={_routingKey}";
            var routeResponse = await GetResponse(url);

            return GenerateUsefulRouteResponse(routeResponse);
        }

        [HttpPost]
        [Route("elevation")]
        public async Task<ElevationResponse> GetLatitudeofPoints(Location[] locations)
        {
            var url = "https://maps.googleapis.com/maps/api/elevation/json?locations=";

            foreach(Location location in locations)
            {
                url += $"{location.Lat},{location.Lng}|";
            }
            url = url.Remove(url.Length - 1);

            url += $"&key={_googleKey}";

            HttpClient client = new HttpClient();

            var response = await client.GetAsync(url);

            // need to catch errors here as if get so. Then just 500 is added by code below.
            var jsonString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ElevationResponse>(jsonString);
        }


        public static List<UsefulRouteResponse> GenerateUsefulRouteResponse(RouteResponse routeResponse)
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
