using System;

namespace WeatherRoutingBackend.Model.Route
{
    public class Summary
    {
        public int LengthInMeters { get; set; }
        public int TravelTimeInSeconds { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }

    }
}
