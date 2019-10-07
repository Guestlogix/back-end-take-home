using System.Collections.Generic;

namespace RouteSearch.Application.DTO
{
    public class FlightRouteDTO
    {
        public string Origin { get; private set; }
        public string Destination { get; private set; }

        private List<ConnectionDTO> _connections;
        public IReadOnlyCollection<ConnectionDTO> Connections
        {
            get => _connections;
        }

        public FlightRouteDTO(string origin, string destination, List<ConnectionDTO> connections)
        {
            Origin = origin;
            Destination = destination;
            _connections = connections;
        }        
    }
}