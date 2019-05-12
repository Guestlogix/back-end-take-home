using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AirTrip.Core;
using CsvHelper;
using CsvHelper.Configuration;
using JetBrains.Annotations;

namespace AirTrip.Services.DataProviders
{
    public class AirportDataProvider : DataProvider<Airport>
    {
        private readonly string _location;

        [UsedImplicitly]
        public AirportDataProvider()
        {
        }

        internal AirportDataProvider(string location)
        {
            _location = location;
        }

        protected override string Location => string.IsNullOrEmpty(_location)
            ? Path.Combine(Path.GetDirectoryName(GetType().Assembly.Location), @"Data\airports.csv")
            : _location;

        protected override async Task<IReadOnlyCollection<Airport>> LoadDataAsync(CsvReader reader, CancellationToken token)
        {
            reader.Configuration.RegisterClassMap<AirportMapper>();
            var result = reader.GetRecords<AirportDataHolder>();
            return await Task.FromResult(result.Select(Map).ToList());
        }

        private static Airport Map(AirportDataHolder dataHolder)
        {
            return new Airport(dataHolder.Code);
        }

        [UsedImplicitly]
        private sealed class AirportMapper : ClassMap<AirportDataHolder>
        {
            public AirportMapper()
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