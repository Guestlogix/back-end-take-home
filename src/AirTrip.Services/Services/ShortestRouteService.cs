using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AirTrip.Core;
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

        public async Task<IReadOnlyCollection<ShortestRoute>> GetShortestRouteAsync(
            [NotNull] Airport origin,
            [NotNull] Airport destination,
            CancellationToken cancellationToken)
        {
            if (origin == null)
            {
                throw new ArgumentNullException(nameof(origin));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            var routes = await _routeService.GetAllRoutesAsync(cancellationToken);

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
                return new[]
                {
                    new ShortestRoute(new[] {origin, destination})
                };
            }

            return new[]
            {
                new ShortestRoute(new[] {origin, destination})
            };
        }
    }

    public class RouteNotSupportedException : Exception
    {
        public RouteNotSupportedException(string message) : base(message)
        {
        }
    }

    public sealed class ShortestRoute
    {
        public IReadOnlyCollection<Airport> Airports { get; }

        public ShortestRoute([NotNull] IReadOnlyCollection<Airport> airports)
        {
            this.Airports = airports ?? throw new ArgumentNullException(nameof(airports));
        }
    }
}
