using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AirTrip.Core;
using AirTrip.Services.DataProviders;
using JetBrains.Annotations;

namespace AirTrip.Services.Services
{
    internal sealed class RouteService : IRouteService
    {
        private IReadOnlyCollection<Route> _routeCache;
        private readonly IDataProvider<Route> _routeDataProvider;
        private readonly object _lockObject = new object();

        public RouteService([NotNull] IDataProvider<Route> routeDataProvider)
        {
            _routeDataProvider = routeDataProvider ?? throw new ArgumentNullException(nameof(routeDataProvider));
        }

        public async Task<IReadOnlyCollection<Route>> GetAllRoutesAsync(CancellationToken cancellationToken)
        {
            if (_routeCache == null)
            {
                var routes = await _routeDataProvider.GetDataAsync(cancellationToken);

                if (routes == null)
                {
                    return Array.Empty<Route>();
                }

                _routeCache = routes;
                return _routeCache;
            }

            return _routeCache;
        }
    }
}