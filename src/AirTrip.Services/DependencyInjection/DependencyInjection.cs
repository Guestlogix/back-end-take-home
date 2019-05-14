using System;
using AirTrip.Core.Models;
using AirTrip.Services.DataProviders;
using AirTrip.Services.Services;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace AirTrip.Services.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataProviders([NotNull] this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddSingleton<IDataProvider<Route>, RouteDataProvider>();

            return services;
        }

        public static IServiceCollection AddCustomServices([NotNull] this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddSingleton<IRouteService, RouteService>();
            services.AddSingleton<IShortestRouteService, ShortestRouteService>();

            return services;
        }
    }
}
