using System.Collections.Generic;

namespace WeatherRoutingBackend.Model.Route
{
    public class RouteResponse
    {
        //public string FormatVersion { get; set; }
        public IEnumerable<Route> Routes { get; set; }
    }
}
