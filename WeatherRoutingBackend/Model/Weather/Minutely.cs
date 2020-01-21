using System.Collections.Generic;

namespace WeatherRoutingBackend.Model.Weather
{
    public class Minutely
    {
        public string Summary { get; set; }
        public string Icon { get; set; }
        public IEnumerable<MinutelyRain> Data { get; set; }
    }
}
