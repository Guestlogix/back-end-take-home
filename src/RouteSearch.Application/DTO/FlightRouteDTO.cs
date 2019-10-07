using System.Collections.Generic;

namespace RouteSearch.Application.DTO
{
    public class FlightRouteDTO
    {
        public AirportDTO Origin { get; private set; }
        public AirportDTO Destination { get; private set; }

        public List<RouteDTO> Connections { get; private set; }

        public FlightRouteDTO()
        {
        }        
    }
}