using System.Collections.Generic;
using System.IO;
using System.Linq;
using AirTrip.Core;
using CsvHelper;
using CsvHelper.Configuration;
using JetBrains.Annotations;

namespace AirTrip.Services.DataProviders
{
    internal sealed class RouteDataProvider : DataProvider<Route>
    {
        private readonly string _location;

        internal RouteDataProvider(string location)
        {
            _location = location;
        }

        protected override string Location => string.IsNullOrEmpty(_location)
            ? Path.Combine(typeof(AirlineDataProvider).Assembly.Location, @"Data\routes.csv")
            : _location;

        protected override IReadOnlyCollection<Route> ParseData(CsvReader reader)
        {
            reader.Configuration.RegisterClassMap<AirlineMapper>();
            var records = reader.GetRecords<RouteDataHolder>();
            return records.Select(Map).ToList();
        }

        private static Route Map(RouteDataHolder dataHolder)
        {
            return new Route(
                dataHolder.AirlineId,
                dataHolder.Origin,
                dataHolder.Destination);
        }

        [UsedImplicitly]
        private sealed class AirlineMapper : ClassMap<RouteDataHolder>
        {
            public AirlineMapper()
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