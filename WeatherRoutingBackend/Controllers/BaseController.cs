using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WeatherRoutingBackend.Controllers
{
    public abstract class BaseController<T> : ControllerBase
    {
        private readonly HttpClient _client = new HttpClient();

        protected async Task<T> GetResponse(string url)
        {
            var response = await _client.GetAsync(url);

            // need to catch errors here as if get so. Then just 500 is added by code below.
            var jsonString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(jsonString);
            
        }
    }
}