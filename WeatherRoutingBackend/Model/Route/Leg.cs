using System.Collections.Generic;

namespace WeatherRoutingBackend.Model.Route
{
    public class Leg
    {
        public Summary Summary { get; set; }
        public IEnumerable<Point> Points { get; set; }
    }
}
