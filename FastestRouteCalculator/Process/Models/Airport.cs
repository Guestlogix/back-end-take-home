using GeoCoordinatePortable;

namespace Process.Models
{
    public class Airport
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string IATA3 { get; set; }
        public GeoCoordinate Location { get; set; }
    }
}
