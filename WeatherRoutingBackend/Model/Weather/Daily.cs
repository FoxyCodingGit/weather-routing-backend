using System.Collections.Generic;

namespace WeatherRoutingBackend.Model.Weather
{
    public class Daily
    {
        public string Summary { get; set; }
        public string Icon { get; set; }
        public IEnumerable<DailyData> Data { get; set; }
    }
}
