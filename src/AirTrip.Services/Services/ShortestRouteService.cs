using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AirTrip.Core.Exceptions;
using AirTrip.Core.Models;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace AirTrip.Services.Services
{
    public sealed class ShortestRouteService : IShortestRouteService
    {
        private readonly IRouteService _routeService;
        private ILogger<ShortestRouteService> _logger;

        public ShortestRouteService([NotNull] IRouteService routeService, [NotNull] ILogger<ShortestRouteService> logger)
        {
            _routeService = routeService ?? throw new ArgumentNullException(nameof(routeService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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

            var origins = routes.Where(i => i.Origin == origin);
            if (!origins.Any())
            {
                throw new RouteNotSupportedException($"Airport '{origin.Code}' is not supported");
            }

            var destinations = routes.Where(i => i.Destination == destination);
            if (!destinations.Any())
            {
                throw new RouteNotSupportedException($"Airport '{destination.Code}' is not supported");
            }

            var directRoute = new Route(new Airline("AC"), origin, destination);

            if (routes.Any(route => directRoute == route))
            {
                return new[] { origin, destination };
            }

            var originLookup = routes.ToLookup(i => i.Origin, route => route.Destination);
            
            var hashSet = new HashSet<Airport>{origin};
            var queue = new Queue<Airport>(new[] {origin});

            while (queue.Count > 0)
            {
                var currentAirport = queue.Dequeue();

                var hubs = originLookup[currentAirport];
                foreach (var hub in hubs)
                {
                    hashSet.Add(currentAirport);

                    if (!hashSet.Contains(hub))
                    {
                        queue.Enqueue(hub);
                    }
                }   
            }

            return hashSet;
        }
    }
}
