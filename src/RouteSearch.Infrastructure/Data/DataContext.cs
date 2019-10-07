using System.Collections.Generic;
using RouteSearch.Domain.Entities;

namespace RouteSearch.Infrastructure.Data
{
    public class DataContext
    {
        private static Dictionary<string, Airport> _airports;
        private static Dictionary<string, Airline> _airlines;
        private static List<Route> _routes;

        public IReadOnlyList<Route> Routes { get => _routes; }

        public IReadOnlyDictionary<string, Airport> Airports { get => _airports; }

        public IReadOnlyDictionary<string, Airline> Airlines { get => _airlines; }

        public DataContext(Dictionary<string, Airport> airports,
                           Dictionary<string, Airline> airlines,
                           List<Route> routes)
        {
            _airports = airports;
            _airlines = airlines;
            _routes = routes;
        }
    }
}