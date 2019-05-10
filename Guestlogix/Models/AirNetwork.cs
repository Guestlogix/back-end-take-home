using Guestlogix.exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Guestlogix.Models
{
    public class AirNetwork
    {
        private IEnumerable<AirlineModel> airlines;
        private IEnumerable<AirportModel> airports;
        private IEnumerable<FlightModel> flights;

        public AirNetwork(IEnumerable<AirlineModel> airlines, IEnumerable<AirportModel> airports, IEnumerable<FlightModel> flights)
        {
            this.airlines = airlines;
            this.airports = airports;
            this.flights = flights;
        }

        public RouteModel GetShortestRoute(string origin, string destination)
        {
            if (!airports.Any(a => a.IATA3 == origin))
            {
                throw new OriginNotFoundException();
            }
            if (!airports.Any(a => a.IATA3 == destination))
            {
                throw new DestinationNotFoundException();
            }
            return new RouteModel { Flights = new List<FlightModel>() };
        }
    }
}