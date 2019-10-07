using System;
using System.Collections.Generic;
using System.Linq;

namespace RouteSearch.Domain.Entities
{
    public class FlightRoute
    {
        public Airport Origin { get; private set; }
        public Airport Destination { get; private set; }

        private List<Route> _connections;

        public IReadOnlyList<Route> Connections
        {
            get => _connections;
        }        

        public FlightRoute(Route route)
        {
            Origin = route.Origin;
            Destination = route.Destination;
            _connections = new List<Route>();
            _connections.Add(route);
        }

        public FlightRoute(List<Route> routes)
        {
            _connections = routes;
            Origin = _connections.First().Origin;
            Destination = _connections.Last().Destination;
        }

        internal void AddRoute(Route route)
        {
            _connections.Add(route);
            Destination = route.Destination;
        }
    }
}