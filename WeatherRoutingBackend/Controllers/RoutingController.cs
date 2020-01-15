using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WeatherRoutingBackend.Model.Route;

namespace WeatherRoutingBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class RoutingController : ControllerBase
    {
        private static readonly HttpClient Client = new HttpClient(); // needs to only be init once, so need to move out into own base service.

        [HttpGet]
        [Route("{travelMode}/{numberOfAlternates}/{startLat}/{startLng}/{endLat}/{endLng}")]
        public async Task<PointsTimeAndDistance[]> GetRouteLegsAndTime(string travelMode, int numberOfAlternates, double startLat, double startLng, double endLat, double endLng)
        {
            var key = "dzTX2x3ocGZPLzhVGol51CtFKBX7hD63";

            HttpResponseMessage response =
                await Client.GetAsync(
                    $"https://api.tomtom.com/routing/1/calculateRoute/{startLat},{startLng}:{endLat},{endLng}/json?travelMode={travelMode}&maxAlternatives={numberOfAlternates}&key={key}");


            var jsonString = await response.Content.ReadAsStringAsync(); // need to catch errors here as if get so. Then just 500 is added by code below.
            var adam = JsonConvert.DeserializeObject<RouteResponse>(jsonString);

            PointsTimeAndDistance[] routes = new PointsTimeAndDistance[adam.Routes.ToArray().Length];

            for (int i = 0; i < adam.Routes.ToArray().Length; i++)
            {
                var justPoints = adam.Routes.ToList()[i].Legs.ToList()[0].Points; // DONT KNOW HOW MAXALT WORKS. IS IT A ROUTE OR A LEG????

                routes[i] = new PointsTimeAndDistance
                {
                    Points = justPoints,
                    TravelTimeInSeconds = adam.Routes.ToList()[i].Summary.TravelTimeInSeconds,
                    Distance = adam.Routes.ToList()[i].Summary.LengthInMeters
                };
            }

            return routes;


        }
    }
}