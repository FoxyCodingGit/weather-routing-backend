using System.Collections.Generic;

namespace WeatherRoutingBackend.Model.Route
{
    public class UsefulRouteResponse
    {
        public IEnumerable<Point> Points { get; set; }
        public int TravelTimeInSeconds { get; set; }
        public int Distance { get; set; }
    }
}
