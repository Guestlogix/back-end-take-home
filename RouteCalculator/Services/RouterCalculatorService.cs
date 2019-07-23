using System;
using System.Collections.Generic;
using System.Linq;
using RouteCalculator.Contracts;
using RouteCalculator.Entities;
using RouteCalculator.Entities.Models;

namespace RouteCalculator.Services
{
    public class RouterCalculatorService : IRouteCalculatorService
    {
        private readonly ICacheService _cache;
        private readonly IRouteCalculatorRepository _repository;
        private readonly List<Airport> _airports;

        public RouterCalculatorService(ICacheService cache, IRouteCalculatorRepository repo)
        {
            _cache = cache;
            _repository = repo;
            _airports = _cache.GetEntity("allAirports", InitializeAirports().ToList);
            _cache.GetEntity("allRoutes", InitializeRoutes().ToList);
        }

        public IEnumerable<Entities.Dtos.Route> GetShortestRoute(string originAirportCode, string destinationAirportCode,
            out Error errorCode)
        {
            errorCode = Error.None;

            #region "Validation"
            if (_airports.All(airport => !airport.Code.Equals(originAirportCode, StringComparison.OrdinalIgnoreCase)))
            {
                errorCode = Error.InvalidOriginAirport;
                return null;
            }

            if (_airports.All(airport => !airport.Code.Equals(destinationAirportCode, StringComparison.OrdinalIgnoreCase)))
            {
                errorCode = Error.InvalidDestinationAirport;
                return null;
            }
            #endregion

            var originAirport = _airports.Single(airport =>
                airport.Code.Equals(originAirportCode, StringComparison.OrdinalIgnoreCase));
            var airportsToProcess = new Queue<Airport>();
            var airportsAlreadyProcessed = new List<Airport> { originAirport };

            // Start by processing all the "neighbours" of origin airport
            foreach (var route in originAirport.OutboundFlights)
            {
                route.DestinationAirport.ItineraryTracker = route;
                airportsToProcess.Enqueue(route.DestinationAirport);
            }

            while (airportsToProcess.Any())
            {
                var airportToEvaluate = airportsToProcess.Dequeue();

                if (!airportToEvaluate.Code.Equals(destinationAirportCode, StringComparison.OrdinalIgnoreCase))
                {
                    // Ensure the same airport is not processed again
                    airportsAlreadyProcessed.Add(airportToEvaluate);

                    // Continue to process "neighbours" of the neighbouring airports
                    foreach (var route in airportToEvaluate.OutboundFlights
                            .Where(r => !airportsAlreadyProcessed.Contains(r.DestinationAirport)
                                    && !airportsToProcess.Contains(r.DestinationAirport)))
                    {
                        route.DestinationAirport.ItineraryTracker = route;
                        airportsToProcess.Enqueue(route.DestinationAirport);
                    }
                }
                else
                {
                    // Destination airport has been located
                    var itinerary = new List<Entities.Dtos.Route>();
                    var traversal = airportToEvaluate;
                    while (traversal != originAirport)
                    {
                        itinerary.Add(new Entities.Dtos.Route
                        {
                            AirlineCode = traversal.ItineraryTracker.AirlineCode,
                            OriginAirportCode = traversal.ItineraryTracker.OriginAirport.Code,
                            DestinationAirportCode = traversal.ItineraryTracker.DestinationAirport.Code,
                        });
                        traversal = traversal.ItineraryTracker.OriginAirport;
                    }

                    // Flights were added beginning from destination. Sort them in correct order.
                    itinerary.Reverse();
                    return itinerary;
                }
            }

            // All airports and their "neighbouring" airports have been evaluated. Destination could not be reached.
            errorCode = Error.NoRouteFound;
            return null;
        }

        private IEnumerable<Airport> InitializeAirports()
        {
            return _repository.GetAirports().Select(airportDto => new Airport(airportDto));
        }

        private IEnumerable<Route> InitializeRoutes()
        {
            var routes = new List<Route>();

            foreach (var routeDto in _repository.GetRoutes())
            {
                if (_airports.All(airport =>
                        !airport.Code.Equals(routeDto.OriginAirportCode, StringComparison.OrdinalIgnoreCase)) ||
                    _airports.All(airport =>
                        !airport.Code.Equals(routeDto.DestinationAirportCode, StringComparison.OrdinalIgnoreCase)))
                    // ToDo: Should this be an error? A route that involves an unknown airport.
                    continue;

                var origin = _airports.Single(airport =>
                    airport.Code.Equals(routeDto.OriginAirportCode, StringComparison.OrdinalIgnoreCase));

                var destination = _airports.Single(airport => airport.Code.Equals(routeDto.DestinationAirportCode,
                    StringComparison.OrdinalIgnoreCase));

                routes.Add(new Route()
                {
                    AirlineCode = routeDto.AirlineCode,
                    OriginAirport = origin,
                    DestinationAirport = destination
                });

                origin.OutboundFlights.Add(routes.Last());
            }

            return routes;
        }
    }
}
