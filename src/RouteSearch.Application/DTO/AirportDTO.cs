namespace RouteSearch.Application.DTO
{
    public class AirportDTO
    {
        public string Iata { get; private set; }
        public string AirportName { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }

        public AirportDTO()
        {
            
        }
    }
}