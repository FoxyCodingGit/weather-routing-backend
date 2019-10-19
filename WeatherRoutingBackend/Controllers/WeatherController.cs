using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WeatherRoutingBackend.Model.Route;
using WeatherRoutingBackend.Model.Weather;

namespace WeatherRoutingBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        private static readonly HttpClient
            Client = new HttpClient(); // needs to only be init once, so need to move out into own base service.

        [HttpGet]
        [Route("{lat}/{lng}")]
        public async Task<WeatherResponse> GetWeatherForPoint(double lat, double lng)
        {
            const string key = "c2cae150bc54b0e5884a2e74a974152a";

            HttpResponseMessage response =
                await Client.GetAsync(
                    $"https://api.darksky.net/forecast/{key}/{lat},{lng}");


            var jsonString =
                await response.Content
                    .ReadAsStringAsync(); // need to catch errors here as if get so. Then just 500 is added by code below.

            return JsonConvert.DeserializeObject<WeatherResponse>(jsonString);
        }

        [HttpGet]
        [Route("rainprob/{lat}/{lng}")]
        public async Task<double> GetRainPercentageForPoint(double lat, double lng)
        {
            const string key = "c2cae150bc54b0e5884a2e74a974152a";

            HttpResponseMessage response =
                await Client.GetAsync(
                    $"https://api.darksky.net/forecast/{key}/{lat},{lng}"); // add unit in request.


            var jsonString =
                await response.Content
                    .ReadAsStringAsync(); // need to catch errors here as if get so. Then just 500 is added by code below.

            return JsonConvert.DeserializeObject<WeatherResponse>(jsonString).Currently.PrecipProbability;
        }

        [HttpGet]
        [Route("rainprob/minutely/{lat}/{lng}/{minuteWillReach}")]
        public async Task<double> GetRainPercentageForPointWithinHourAway(double lat, double lng, int minuteWillReach)
        {
            const string key = "c2cae150bc54b0e5884a2e74a974152a";

            HttpResponseMessage response =
                await Client.GetAsync(
                    $"https://api.darksky.net/forecast/{key}/{lat},{lng}"); // add unit in request.

            var jsonString =
                await response.Content
                    .ReadAsStringAsync(); // need to catch errors here as if get so. Then just 500 is added by code below.

            return JsonConvert.DeserializeObject<WeatherResponse>(jsonString).Minutely.Data.ToArray()[minuteWillReach].PrecipProbability;
        }

        [HttpGet]
        [Route("rainprob/minutely/{lat}/{lng}")]
        public async Task<IEnumerable<MinutelyRain>> GetMinutelyDataForPoint(double lat, double lng)
        {
            const string key = "c2cae150bc54b0e5884a2e74a974152a";

            HttpResponseMessage response =
                await Client.GetAsync(
                    $"https://api.darksky.net/forecast/{key}/{lat},{lng}"); // add unit in request.

            var jsonString =
                await response.Content
                    .ReadAsStringAsync(); // need to catch errors here as if get so. Then just 500 is added by code below.

            return JsonConvert.DeserializeObject<WeatherResponse>(jsonString).Minutely.Data;
        }
    }
}