namespace WeatherRoutingBackend.DataLayer.Models
{
    public class Route
    {
        public int RouteId { get; set; }
        public string Name { get; set; }
        public int ModeOfTransportId { get; set; }
        public int StartLatLngCoordId { get; set; }
        public int EndLatLngCoordId { get; set; }
    }
}
