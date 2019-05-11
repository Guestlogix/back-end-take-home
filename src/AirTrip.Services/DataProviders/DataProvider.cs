using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;

namespace AirTrip.Services.DataProviders
{
    public abstract class DataProvider<TResult> : IDataProvider<TResult>
    {
        public IReadOnlyCollection<TResult> GetData()
        {
            if (string.IsNullOrEmpty(Location))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(Location));
            }

            using (var reader = new StreamReader(Location))
            {
                using (var csvReader = new CsvReader(reader))
                {
                    return ParseData(csvReader);
                }
            }
        }

        protected abstract string Location { get; }

        protected abstract IReadOnlyCollection<TResult> ParseData(CsvReader reader);
        
    }
}
