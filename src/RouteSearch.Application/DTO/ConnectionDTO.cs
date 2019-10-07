using System.Collections.Generic;

namespace RouteSearch.Application.DTO
{
    public class ConnectionDTO
    {
        public AirportDTO Origin { get; private set; }

        public AirportDTO Destination { get; private set; }
        
        private List<AirlineDTO> _airlines;
        public IReadOnlyList<AirlineDTO> Airlines { get => _airlines; }

        public ConnectionDTO(AirportDTO origin, AirportDTO destination, List<AirlineDTO> airlines)
        {
            Origin = origin;
            Destination = destination;
            _airlines = airlines;
        }        
    }
}