using System.Collections.Generic;
using WeatherRoutingBackend.Model.Route;

namespace WeatherRoutingBackend.Model.Response
{
    public class ElevationResponse
    {
        public List<ElevationResult> Results { get; set; }
    }
}
