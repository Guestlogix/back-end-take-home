using System;
using System.Collections.Generic;
using System.Linq;

namespace RouteSearch.Domain.Entities
{
    public class FlightRoute
    {
        public string Origin { get; private set; }
        public string Destination { get; private set; }

        private List<Connection> _connections;
        public IReadOnlyList<Connection> Connections
        {
            get
            {
                return _connections;
            }
        }

        public FlightRoute(string origin, string destination, List<Connection> connections)
        {
            Origin = origin;
            Destination = destination;
            _connections = connections;
        }

        public FlightRoute(List<Airport> routeConnections)
        {
            if (routeConnections is null || !routeConnections.Any())
                throw new ArgumentOutOfRangeException("No Route");

            Origin = routeConnections.First().Iata;
            Destination = routeConnections.Last().Iata;
            _connections = new List<Connection>();
            SetRouteFlight(routeConnections);
        }

        private void SetRouteFlight(List<Airport> routeConnections)
        {
            List<Connection> connections = new List<Connection>();
            List<Airline> lastAirlines = null;

            for (int i = 1; i < routeConnections.Count; i++)
            {
                var currentNode = routeConnections[i];
                var previousNode = routeConnections[i - 1];
                var nextIndex = i - 1;

                lastAirlines = previousNode.Connections[currentNode].Values.ToList();

                var connection = new Connection(previousNode, currentNode, lastAirlines);

                _connections.Add(connection);
            }
        }
    }
}