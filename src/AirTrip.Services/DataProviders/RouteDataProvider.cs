using System.Collections.Generic;
using System.Linq;
using AirTrip.Core;
using CsvHelper;
using CsvHelper.Configuration;
using JetBrains.Annotations;

namespace AirTrip.Services.DataProviders
{
    internal sealed class RouteDataProvider : DataProvider
    {
        protected override IReadOnlyCollection<TResult> ParseData<TResult>(CsvReader reader)
        {
            reader.Configuration.RegisterClassMap<AirlineMapper>();
            var records = reader.GetRecords<RouteDataHolder>();
            return (IReadOnlyCollection<TResult>)records.Select(Map).ToList();
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