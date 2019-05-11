using System;
using AirTrip.Core;
using AirTrip.Services.DataProviders;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace AirTrip.Services.DependencyInjection
{
    public static class DependencyInjectino
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
    }
}
