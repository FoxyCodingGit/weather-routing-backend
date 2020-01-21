using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WeatherRoutingBackend.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] // https://stackoverflow.com/questions/55781040/401-unauthorized-www-authenticate-bearer
    public abstract class BaseController<T> : ControllerBase
    {
        private readonly HttpClient _client = new HttpClient();

        protected async Task<T> GetResponse(string url)
        {
            HttpResponseMessage response = await _client.GetAsync(url);

            // need to catch errors here as if get so. Then just 500 is added by code below.
            var jsonString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(jsonString);
            
        }
    }
}