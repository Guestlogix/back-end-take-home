using System;
using AirTrip.Core;
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

            services.AddSingleton<IDataProvider<Airline>, AirlineDataProvider>();
            services.AddSingleton<IDataProvider<Airport>, AirportDataProvider>();
            services.AddSingleton<IDataProvider<Route>, RouteDataProvider>();

            return services;
        }

        public static IServiceCollection AddAirlineServices([NotNull] this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddSingleton<IRouteService, RouteService>();
            services.AddSingleton<IAirlineService, AirlineService>();
            services.AddSingleton<IShortestRouteService, ShortestRouteService>();

            return services;
        }
    }
}
