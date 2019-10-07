namespace RouteSearch.Domain.Entities
{
    public class Route
    {
        public Airport Origin { get; private set; }
        public Airport Destination { get; private set; }
        public Airline Airline { get; private set; }

        public Route(Airport origin, Airport destination, Airline airline)
        {
            Origin = origin;
            Destination = destination;
            Airline = airline;
        }
    }
}