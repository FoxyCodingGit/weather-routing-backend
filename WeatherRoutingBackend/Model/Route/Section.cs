namespace WeatherRoutingBackend.Model.Route
{
    public class Section
    {
        public int StartPointIndex { get; set; }
        public int EndPointIndex { get; set; }
        public string SectionType { get; set; }
        public string TravelMode { get; set; }
    }
}
