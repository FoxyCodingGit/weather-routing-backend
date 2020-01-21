using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WeatherRoutingBackend.Model.Weather;

namespace WeatherRoutingBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WeatherController : BaseController<WeatherResponse>
    {
        private readonly string _weatherKey;

        public WeatherController(IConfiguration config)
        {
            _weatherKey = config.GetValue<string>("AppSettings:WeatherKey");
        }

        [HttpGet]
        [Route("{lat}/{lng}")]
        public async Task<WeatherResponse> GetWeatherForPoint(double lat, double lng)
        {
            var url = $"https://api.darksky.net/forecast/{_weatherKey}/{lat},{lng}?units=si";
            return await GetResponse(url);
        }

        [HttpGet]
        [Route("rainprob/{lat}/{lng}")]
        public async Task<double> GetRainPercentageForPoint(double lat, double lng)
        {
            var url = $"https://api.darksky.net/forecast/{_weatherKey}/{lat},{lng}?units=si";
            var weatherResponse = await GetResponse(url);
            return weatherResponse.Currently.PrecipProbability;
        }

        [HttpGet]
        [Route("rain/minutely/{lat}/{lng}")]
        public async Task<IEnumerable<MinutelyRain>> GetMinutelyDataForPoint(double lat, double lng)
        {
            var url = $"https://api.darksky.net/forecast/{_weatherKey}/{lat},{lng}?units=si";
            var weatherResponse = await GetResponse(url);
            return weatherResponse.Minutely.Data;
        }

        [HttpGet]
        [Route("currently/{lat}/{lng}")]
        public async Task<Currently> GetCurrentWeatherForPoint(double lat, double lng)
        {
            var url = $"https://api.darksky.net/forecast/{_weatherKey}/{lat},{lng}?units=si";
            var weatherResponse = await GetResponse(url);
            return weatherResponse.Currently;
        }
    }
}