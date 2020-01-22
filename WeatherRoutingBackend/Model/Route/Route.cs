using System.Collections.Generic;

namespace WeatherRoutingBackend.Model.Route
{
    public class Route
    {
        public Summary Summary { get; set; }
        public List<Leg> Legs { get; set; }
    }
}
