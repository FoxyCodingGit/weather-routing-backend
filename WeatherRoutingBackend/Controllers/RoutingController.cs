using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WeatherRoutingBackend.Model.Route;

namespace WeatherRoutingBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoutingController : ControllerBase
    {
        static readonly HttpClient client = new HttpClient(); // needs to only be init once, so need to move out into own base service.

        [HttpGet]
        [Route("{startLat}/{startLng}/{endLat}/{endLng}")]
        public async Task<IEnumerable<Point>> GetRouteLegs(double startLat, double startLng, double endLat, double endLng)
        {
            var key = "dzTX2x3ocGZPLzhVGol51CtFKBX7hD63";

            HttpResponseMessage response =
                await client.GetAsync(
                    $"https://api.tomtom.com/routing/1/calculateRoute/{startLat},{startLng}:{endLat},{endLng}/json?key={key}");


            var jsonString = await response.Content.ReadAsStringAsync(); // need to catch errors here as if get so. Then just 500 is added by code below.
            var adam =  JsonConvert.DeserializeObject<RouteResponse>(jsonString);

            var justPoints = adam.Routes.ToList()[0].Legs.ToList()[0].Points;

            return justPoints;
        }
    }
}