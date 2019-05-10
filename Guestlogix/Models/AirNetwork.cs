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

            //represent graph as adjacency list
            Dictionary<string, List<FlightModel>> adjList = new Dictionary<string, List<FlightModel>>();
            foreach (var flight in flights)
            {
                if (!adjList.ContainsKey(flight.Origin))
                {
                    adjList[flight.Origin] = new List<FlightModel>();
                }
                adjList[flight.Origin].Add(flight);
            }

            //BFS to find shortest path
            Queue<string> queue = new Queue<string>();
            Dictionary<string, bool> visited = new Dictionary<string, bool>();
            Dictionary<string, FlightModel> prev = new Dictionary<string, FlightModel>();
            queue.Enqueue(origin);
            visited[origin] = true;
            while (queue.Count > 0)
            {
                string curAirport = queue.Dequeue();
                if (adjList.ContainsKey(curAirport))
                {
                    foreach (var flight in adjList[curAirport])
                    {
                        if (!visited.ContainsKey(flight.Destination) || !visited[flight.Destination])
                        {
                            queue.Enqueue(flight.Destination);
                            visited[flight.Destination] = true;
                            prev[flight.Destination] = flight;
                            if (flight.Destination == destination)
                            {
                                break;
                            }
                        }
                    }
                }
            }

            //construct shortest path
            RouteModel rm = new RouteModel();
            rm.Flights = new List<FlightModel>();
            for (var a = destination; prev.ContainsKey(a); a = prev[a].Origin)
            {
                rm.Flights.Insert(0, prev[a]);
            }
            if (rm.Flights.Count == 0)
            {
                throw new NoRouteFoundException();
            }
            return rm;
        }
    }
}