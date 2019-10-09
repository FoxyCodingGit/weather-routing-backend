using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherRoutingBackend.Model.Weather
{
    public class Daily
    {
        public string Summary { get; set; }
        public string Icon { get; set; }
        public IEnumerable<DailyData> Data { get; set; }
    }
}
