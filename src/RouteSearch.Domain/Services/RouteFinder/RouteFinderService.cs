using System;
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
        private HashSet<string> _iataCodes;

        public RouteFinderService(IAirportRepository airportRepository,
                                IRouteRepository routeRepository)
        {
            _airportRepository = airportRepository;
            _routeRepository = routeRepository;

            _iataCodes = new HashSet<string>();
        }

        public FlightRoute Find(string origin, string destination)
        {
            var originNode = _airportRepository.GetByIata(origin);
            var destinationNode = _airportRepository.GetByIata(destination);

            RouteFinderValidatorService.Validate(originNode, destinationNode);

            _iataCodes.Add(origin);
            List<FlightRoute> flightRoutes = GetOriginDestinations(originNode);

            while(flightRoutes.Any() && !_iataCodes.Contains(destinationNode.Iata))
            {
                var flightRoutesAuxiliary = new List<FlightRoute>();
                foreach (var flightRoute in flightRoutes)
                {
                    List<FlightRoute> flights = GetConnectedRoutes(flightRoute);
                    flightRoutesAuxiliary.AddRange(flights);
                }
                flightRoutes = flightRoutesAuxiliary;
            }
            
            return GetFlightRouteResult(flightRoutes, destinationNode);
        }

        private FlightRoute GetFlightRouteResult(List<FlightRoute> flightRoutes, Airport destination)
        {
            if (!flightRoutes.Any())
                throw new ArgumentOutOfRangeException(string.Empty, "No Route");

            return flightRoutes.FirstOrDefault(route => route.Destination == destination);
        }

        private List<FlightRoute> GetConnectedRoutes(FlightRoute flightRoute)
        {
            var flightRoutes = new List<FlightRoute>();
            List<Route> connectedRoutes = _routeRepository.GetDestinationConnections(flightRoute.Destination);

            foreach (var connectedRoute in connectedRoutes)
            {
                if (!_iataCodes.Contains(connectedRoute.Destination.Iata))
                {
                    var newFlightRoute = new FlightRoute(flightRoute.Connections.ToList());
                    newFlightRoute.AddRoute(connectedRoute);
                    flightRoutes.Add(newFlightRoute);

                    _iataCodes.Add(connectedRoute.Destination.Iata);
                }
            }

            return flightRoutes;
        }

        private List<FlightRoute> GetOriginDestinations(Airport origin)
        {
            List<Route> currentConnectedRoutes = _routeRepository.GetDestinationConnections(origin);
            var flightRoutes = new List<FlightRoute>();

            foreach (var route in currentConnectedRoutes)
            {
                if (!_iataCodes.Contains(route.Destination.Iata))
                {
                    var flightRoute = new FlightRoute(route);
                    flightRoutes.Add(flightRoute);
                    _iataCodes.Add(route.Destination.Iata);
                }
            }            

            return flightRoutes;
        }        
    }
}