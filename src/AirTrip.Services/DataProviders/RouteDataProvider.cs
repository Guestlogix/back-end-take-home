using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using AirTrip.Core;
using CsvHelper;
using CsvHelper.Configuration;
using JetBrains.Annotations;

namespace AirTrip.Services.DataProviders
{
    internal sealed class RouteDataProvider : DataProvider<Route>
    {
        private readonly string _location;

        [UsedImplicitly]
        public RouteDataProvider()
        {
        }

        internal RouteDataProvider(string location)
        {
            _location = location;
        }

        protected override string Location => string.IsNullOrEmpty(_location) 
                ? Path.Combine(Path.GetDirectoryName(GetType().Assembly.Location), @"Data\routes.csv") 
                : _location;

        protected override async Task<IReadOnlyCollection<Route>> LoadDataAsync(CsvReader reader, CancellationToken token)
        {
            reader.Configuration.RegisterClassMap<RouteMapper>();
            var result = reader.GetRecords<RouteDataHolder>();
            return await Task.FromResult(result.Select(Map).ToList());
        }

        private static Route Map(RouteDataHolder dataHolder)
        {
            return new Route(
                dataHolder.AirlineId,
                new Airport(dataHolder.Origin), 
                new Airport(dataHolder.Destination));
        }

        [UsedImplicitly]
        private sealed class RouteMapper : ClassMap<RouteDataHolder>
        {
            public RouteMapper()
            {
                Map(m => m.AirlineId).Name("Airline Id");
                Map(m => m.Origin).Name("Origin");
                Map(m => m.Destination).Name("Destination");
            }
        }

        [UsedImplicitly]
        private sealed class RouteDataHolder
        {
            public string AirlineId { get; [UsedImplicitly] set; }

            public string Origin { get; [UsedImplicitly] set; }

            public string Destination { get; [UsedImplicitly] set; }
        }
    }
}