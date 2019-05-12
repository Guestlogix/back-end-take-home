using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AirTrip.Core;
using AirTrip.Services.DataProviders;
using JetBrains.Annotations;

namespace AirTrip.Services.Services
{
    public sealed class RouteService : IRouteService
    {
        private IReadOnlyCollection<Route> _routes;
        private readonly IDataProvider<Route> _routeDataProvider;

        public RouteService([NotNull] IDataProvider<Route> routeDataProvider)
        {
            _routeDataProvider = routeDataProvider ?? throw new ArgumentNullException(nameof(routeDataProvider));
        }

        public async Task<IReadOnlyCollection<Route>> GetAllRoutesAsync(CancellationToken cancellationToken)
        {
            _routes = _routeDataProvider.GetData() ?? Array.Empty<Route>();

            return await Task.FromResult(_routes);
        }
    }
}