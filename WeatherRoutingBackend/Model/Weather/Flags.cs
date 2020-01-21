using System.Collections.Generic;

namespace WeatherRoutingBackend.Model.Weather
{
    public class Flags
    {
        public IEnumerable<string> Sources { get; set; }
        public double NearestStation { get; set; } // was with hypthen. May mess everything up.
        public string Units { get; set; }
}
}
