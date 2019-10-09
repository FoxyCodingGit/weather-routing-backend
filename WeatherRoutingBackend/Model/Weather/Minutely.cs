using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherRoutingBackend.Model.Weather
{
    public class Minutely
    {
        public string Summary { get; set; }
        public string Icon { get; set; }
        public IEnumerable<MinutelyRain> Data { get; set; }
    }
}
