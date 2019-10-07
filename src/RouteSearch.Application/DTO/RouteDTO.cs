namespace RouteSearch.Application.DTO
{
    public class RouteDTO
    {
        public AirportDTO Origin { get; private set; }

        public AirportDTO Destination { get; private set; }
        
        public AirlineDTO Airline { get; private set; }

        public RouteDTO()
        {

        }        
    }
}