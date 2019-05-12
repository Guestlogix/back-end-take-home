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
            var destinations = routes.Where(i => i.Destination == destination);

            return new []
            {
                new ShortestRoute(new List<Leg>
                {
                    
                }), 
            };
        }
    }

    public sealed class Leg
    {
        public Airline Airline { get; }
        public Airport Origin { get; }
        public Airport Destination { get; }

        public Leg([NotNull] Airline airline, [NotNull] Airport origin, [NotNull] Airport destination)
        {
            Airline = airline ?? throw new ArgumentNullException(nameof(airline));
            Origin = origin ?? throw new ArgumentNullException(nameof(origin));
            Destination = destination ?? throw new ArgumentNullException(nameof(destination));
        }
    }

    public sealed class ShortestRoute
    {
        public IReadOnlyCollection<Leg> Legs { get; }

        public ShortestRoute([NotNull] IReadOnlyCollection<Leg> legs)
        {
            Legs = legs ?? throw new ArgumentNullException(nameof(legs));
        }
    }
}
