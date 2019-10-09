using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherRoutingBackend.Model.Weather
{
    public class MinutelyRain
    {
        public int Time { get; set; }
        public double PrecipIntensity { get; set; }
        public double PrecipProbability { get; set; }
    }
}
