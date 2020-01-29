namespace WeatherRoutingBackend.DataLayer.Models
{
    public class ReadableRoute
    {
        public int ReadableRouteId { get; set; }
        public string RouteName { get; set; }
        public string ModeOfTransport { get; set; }
        public decimal StartLat { get; set; }
        public decimal StartLng { get; set; }
        public decimal EndLat { get; set; }
        public decimal EndLng { get; set; }
    }
}
