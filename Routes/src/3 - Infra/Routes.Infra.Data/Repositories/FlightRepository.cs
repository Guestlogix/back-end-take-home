using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Domain.Modules;
using Domain.Modules.Repositories;
using Domain.Modules.ValueObject;
using Route = Domain.Modules.ValueObject.Route;

namespace Routes.Infra.Data.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        private const int MaximumStops = 3;
        private IList<Route> Routes { get; set; }
        private Dictionary<AirPort, Domain.Modules.Route> Stops { get; set; }

        public string GetShortestRoute(IList<AirPort> airPorts, string origin, string destination)
        {           
            var originAirPort = airPorts.FirstOrDefault(x => x.Iata3 == origin);
            var destinationAirPort = airPorts.FirstOrDefault(x => x.Iata3 == destination);

            if (originAirPort == null)
                return "Invalid Origin";

            if (destinationAirPort == null)
                return "Invalid Destination";

            // Create instance to store all routes.
            Routes = new List<Route>();
            var destinations = originAirPort.DestinationRoutes.OrderBy(x => x.Distance.Kilometers).ToList();
            var directFlight = destinations.Where(x => x.Origin.Equals(originAirPort) && x.Destination.Equals(destinationAirPort)).ToList();

            if (directFlight.Any())
                directFlight.ForEach(x => Routes.Add(CreateRoute(new[] { x })));
            else
                GetShortestRouteByDestination(destinations, originAirPort, destinationAirPort);

            var route = Routes.OrderBy(x => x.Flights.Count).FirstOrDefault();

            return route != null ? route.ConnectingFlights : "No Route";
        }

        private void GetShortestRouteByDestination(IEnumerable<Domain.Modules.Route> destinations, AirPort origin, AirPort destination)
        {
            destinations.ToList().ForEach(x =>
            {
                if (x.Destination.Iata3 == destination.Iata3)
                {
                    var routes = Stops.Values.ToList();
                    routes.Add(x);
                    Routes.Add(CreateRoute(routes));
                    return;
                }

                if (Stops != null && Stops.Count > MaximumStops) return;

                if (Stops == null || !Stops.Any())
                    Stops = new Dictionary<AirPort, Domain.Modules.Route>();

                // Add Stop
                Stops.Add(x.Destination, x);

                // Load Destinations of destination but removed the same origin and exists destination
                var newDestinations = x.Destination
                                       .DestinationRoutes
                                       .Where(d => !d.Destination.Equals(origin) &&
                                                   !Stops.ContainsKey(d.Destination))
                                       .ToList();

                GetShortestRouteByDestination(newDestinations, origin, destination);

                // Remove Stop that system has used in the return of recursive function.
                Stops.Remove(x.Destination);

            });
        }

        /// <summary>
        ///     Create Route with flights to destination
        /// </summary>
        /// <param name="routes"></param>
        /// <returns></returns>
        private Route CreateRoute(IEnumerable<Domain.Modules.Route> routes)
        {
            var route = new Route();
            var i = 0;
            routes.ToList().ForEach(x =>
            {
                if (route.Flights.FirstOrDefault(z => z.AirPort.Equals(x.Origin)) == null)
                    route.Flights.Add(new Flight
                    {
                        AirPort = x.Origin,
                        AirLine = x.AirLine,
                        Distance = x.Distance,
                        Order = ++i
                    });

                if (route.Flights.FirstOrDefault(z => z.AirPort.Equals(x.Destination)) == null)
                    route.Flights.Add(new Flight
                    {
                        AirPort = x.Destination,
                        AirLine = x.AirLine,
                        Distance = x.Distance,
                        Order = ++i
                    });
            });
            return route;
        }
    }
}
