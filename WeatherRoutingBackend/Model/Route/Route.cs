using System.Collections.Generic;

namespace WeatherRoutingBackend.Model.Route
{
    public class Route
    {
        public Summary Summary { get; set; }
        public IEnumerable<Leg> Legs { get; set; }
        public IEnumerable<Section> Sections { get; set; }
    }
}
