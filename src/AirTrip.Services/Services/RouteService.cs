using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AirTrip.Core.Models;
using AirTrip.Services.DataProviders;
using JetBrains.Annotations;
using NeoSmart.AsyncLock;

namespace AirTrip.Services.Services
{
    internal sealed class RouteService : IRouteService
    {
        private IReadOnlyCollection<Route> _routeCache;
        private readonly IDataProvider<Route> _routeDataProvider;
        private readonly AsyncLock _lockObject = new AsyncLock();

        public RouteService([NotNull] IDataProvider<Route> routeDataProvider)
        {
            _routeDataProvider = routeDataProvider ?? throw new ArgumentNullException(nameof(routeDataProvider));
        }

        public async Task<IReadOnlyCollection<Route>> GetAllRoutesAsync(CancellationToken token)
        {
            if (_routeCache != null)
            {
                return _routeCache;
            }

            using (await _lockObject.LockAsync())
            {
                var routes = await _routeDataProvider.GetDataAsync(token);

                if (routes == null)
                {
                    return Array.Empty<Route>();
                }

                _routeCache = routes;
                return _routeCache;
            }

        }
    }
}