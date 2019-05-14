using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AirTrip.Core.Exceptions;
using AirTrip.Core.Models;
using JetBrains.Annotations;

namespace AirTrip.Services.Services
{
    public sealed class ShortestRouteService : IShortestRouteService
    {
        private readonly IRouteService _routeService;

        public ShortestRouteService([NotNull] IRouteService routeService)
        {
            _routeService = routeService ?? throw new ArgumentNullException(nameof(routeService));
        }

        public async Task<IReadOnlyCollection<Airport>> GetShortestRouteAsync(
            [NotNull] Airport origin,
            [NotNull] Airport destination,
            CancellationToken token)
        {
            if (origin == null)
            {
                throw new ArgumentNullException(nameof(origin));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            var routes = await _routeService.GetAllRoutesAsync(token);

            var airportLookup = routes.ToLookup(i => i.Origin, route => route.Destination);

            if (!airportLookup.Contains(origin))
            {
                throw new RouteNotSupportedException($"Airport '{origin.Code}' is not supported");
            }

            return airportLookup[origin].Contains(destination) ? 
                new[] { origin, destination } 
                : FindShortestRoute(origin, destination, airportLookup);
        }

        private static IReadOnlyCollection<Airport> FindShortestRoute(
            Airport origin, Airport destination, ILookup<Airport, Airport> lookUp)
        {
            var tracer = TraceRoute(origin, destination, lookUp);

            return !tracer.ContainsKey(destination) 
                ? Array.Empty<Airport>() 
                : ConstructShortestRoute(tracer, destination);
        }

        private static IReadOnlyCollection<Airport> ConstructShortestRoute(
            IReadOnlyDictionary<Airport, Airport> tracker,
            Airport destination)
        {
            var shortestRoute = new Stack<Airport>();
            var terminal = destination;

            while (terminal != null)
            {
                shortestRoute.Push(terminal);
                terminal = tracker[terminal];
            }

            return shortestRoute;
        }

        private static IReadOnlyDictionary<Airport, Airport> TraceRoute(Airport origin, Airport destination, ILookup<Airport, Airport> airportLookup)
        {
            var routeTracer = new Dictionary<Airport, Airport> {{origin, null}};
            var queue = new Queue<Airport>(new[] {origin});

            while (queue.Any())
            {
                var currentAirport = queue.Dequeue();

                if (currentAirport == destination)
                {
                    break;
                }

                var connections = airportLookup[currentAirport];
                foreach (var connection in connections)
                {
                    queue.Enqueue(connection);
                    if (!routeTracer.ContainsKey(connection))
                    {
                        routeTracer.Add(connection, currentAirport);
                    }
                }
            }

            return routeTracer;
        }
    }
}
