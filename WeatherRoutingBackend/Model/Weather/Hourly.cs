using System.Collections.Generic;

namespace WeatherRoutingBackend.Model.Weather
{
    public class Hourly
    {
        public string Summary { get; set; }
        public string Icon { get; set; }
        public IEnumerable<HourlyData> Data { get; set; }
    }
}
