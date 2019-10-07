using System.Collections.Generic;
using System.Linq;
using RouteSearch.Domain.Entities;
using RouteSearch.Domain.Repositories;
using RouteSearch.Domain.Services.Interfaces;

namespace RouteSearch.Domain.Services.RouteFinder
{
    public class RouteFinderService : IRouteFinderService
    {
        private readonly IAirportRepository _airportRepository;
        private readonly IRouteRepository _routeRepository;

        private Airport _originNode;
        private Airport _destinationNode;

        public RouteFinderService(IAirportRepository airportRepository,
                                IRouteRepository routeRepository)
        {
            _airportRepository = airportRepository;
            _routeRepository = routeRepository;
        }

        public FlightRoute Find(string origin, string destination)
        {
            _originNode = _airportRepository.GetByIata(origin);
            _destinationNode = _airportRepository.GetByIata(destination);

            RouteFinderValidatorService.Validate(_originNode, _destinationNode);

            var queue = new Queue<Airport>();
            queue.Enqueue(_originNode);

            var previousAirports = new Dictionary<Airport, Airport>();

            while (queue.Any())
            {
                Airport airport = queue.Dequeue();
                List<Route> currentConnectedRoutes = _routeRepository.GetDestinationConnections(airport);
                airport.AddConnections(currentConnectedRoutes);

                if (!(airport.Connections is null) &&
                    airport.Connections.ContainsKey(_destinationNode))
                {
                    List<Airport> route = MountRoute(airport, previousAirports);
                    return new FlightRoute(route);
                }

                if (airport.Connections is null)
                    continue;

                foreach (KeyValuePair<Airport, Dictionary<string, Airline>> neighbor in airport.Connections)
                {
                    queue.Enqueue(neighbor.Key);
                    previousAirports[neighbor.Key] = airport;
                }
            }

            return new FlightRoute(null);
        }

        private List<Airport> MountRoute(Airport currentAirport,
                    Dictionary<Airport, Airport> previousAirports)
        {
            var route = new List<Airport>();

            route.Add(_destinationNode);

            while (previousAirports.ContainsKey(currentAirport) && currentAirport != _originNode)
            {
                route.Insert(0, currentAirport);
                currentAirport = previousAirports[currentAirport];
            }

            route.Insert(0, currentAirport);

            return route;
        }
    }
}