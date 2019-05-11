﻿using System.Collections.Generic;
using System.Linq;
using AirTrip.Core;
using CsvHelper;
using CsvHelper.Configuration;
using JetBrains.Annotations;

namespace AirTrip.Services.DataProviders
{
    internal sealed class AirlineDataProvider : DataProvider
    {
        protected override IReadOnlyCollection<TResult> ParseData<TResult>(CsvReader reader)
        {
            reader.Configuration.RegisterClassMap<AirlineMapper>();
            var records = reader.GetRecords<AirlineDataHolder>();
            return (IReadOnlyCollection<TResult>) records.Select(Map).ToList();
        }

        private static Airline Map(AirlineDataHolder dataHolder)
        {
            return new Airline(
                dataHolder.Name,
                dataHolder.TwoDigitCode,
                dataHolder.ThreeDigitCode,
                dataHolder.Country);
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