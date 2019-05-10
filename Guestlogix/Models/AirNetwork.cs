using Guestlogix.resources;
using System;
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
                throw new Exception(Resource.ERR_ORIGIN_NOT_FOUND);
            }
            if (!airports.Any(a => a.IATA3 == destination))
            {
                throw new Exception(Resource.ERR_DESTINATION_NOT_FOUND);
            }
            return new RouteModel { Flights = new List<FlightModel>() };
        }
    }
}