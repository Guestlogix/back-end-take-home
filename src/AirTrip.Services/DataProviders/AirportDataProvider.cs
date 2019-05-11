using System.Collections.Generic;
using System.Linq;
using AirTrip.Core;
using CsvHelper;
using CsvHelper.Configuration;
using JetBrains.Annotations;

namespace AirTrip.Services.DataProviders
{
    public class AirportDataProvider : DataProvider
    {
        protected override IReadOnlyCollection<TResult> ParseData<TResult>(CsvReader reader)
        {
            reader.Configuration.RegisterClassMap<AirlineMapper>();
            var records = reader.GetRecords<AirportDataHolder>();
            return (IReadOnlyCollection<TResult>)records.Select(Map).ToList();
        }

        private static Airport Map(AirportDataHolder dataHolder)
        {
            return new Airport(
                dataHolder.Name,
                dataHolder.City,
                dataHolder.Country,
                dataHolder.Code);
        }

        [UsedImplicitly]
        private sealed class AirlineMapper : ClassMap<AirportDataHolder>
        {
            public AirlineMapper()
            {
                Map(m => m.Name).Name("Name");
                Map(m => m.City).Name("City");
                Map(m => m.Country).Name("Country");
                Map(m => m.Code).Name("IATA 3");
            }
        }

        [UsedImplicitly]
        private sealed class AirportDataHolder
        {
            public string Name { get; [UsedImplicitly] set; }

            public string City { get; [UsedImplicitly] set; }

            public string Country { get; [UsedImplicitly] set; }

            public string Code { get; [UsedImplicitly] set; }
        }
    }
}