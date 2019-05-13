using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AirTrip.Core;
using AirTrip.Core.Models;
using CsvHelper;
using CsvHelper.Configuration;
using JetBrains.Annotations;

namespace AirTrip.Services.DataProviders
{
    internal sealed class AirlineDataProvider : DataProvider<Airline>
    {
        private readonly string _location;

        [UsedImplicitly]
        public AirlineDataProvider()
        {
        }

        internal AirlineDataProvider(string location)
        {
            _location = location;
        }

        protected override string Location => string.IsNullOrEmpty(_location)
            ? Path.Combine(Path.GetDirectoryName(GetType().Assembly.Location), @"Data\airlines.csv")
            : _location;

        protected override async Task<IReadOnlyCollection<Airline>> LoadDataAsync(
            CsvReader reader, CancellationToken token)
        {
            reader.Configuration.RegisterClassMap<AirlineMapper>();
            var result = reader.GetRecords<AirlineDataHolder>();
            return await Task.FromResult(result.Select(Map).ToList());
        }

        private static Airline Map(AirlineDataHolder dataHolder)
        {
            return new Airline(dataHolder.TwoDigitCode);
        }

        [UsedImplicitly]
        private sealed class AirlineMapper : ClassMap<AirlineDataHolder>
        {
            public AirlineMapper()
            {
                Map(m => m.Name).Name("Name");
                Map(m => m.TwoDigitCode).Name("2 Digit Code");
                Map(m => m.ThreeDigitCode).Name("3 Digit Code");
                Map(m => m.Country).Name("Country");
            }
        }

        [UsedImplicitly]
        private sealed class AirlineDataHolder
        {
            public string Name { get; [UsedImplicitly] set; }

            public string TwoDigitCode { get; [UsedImplicitly] set; }

            public string ThreeDigitCode { get; [UsedImplicitly] set; }

            public string Country { get; [UsedImplicitly] set; }
        }
    }
}