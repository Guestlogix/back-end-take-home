using System.Collections.Generic;

namespace RouteSearch.Domain.Entities
{
    public class Connection
    {
        public Airport Origin { get; set; }
        public Airport Destination { get; set; }
        public List<Airline> Airlines { get; private set; }

        public Connection(Airport origin, Airport destination, List<Airline> airlines)
        {
            Origin = origin;
            Destination = destination;
            Airlines = airlines;
        }
    }
}