using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;

namespace AirTrip.Services.DataProviders
{
    public abstract class DataProvider<TResult> : IDataProvider<TResult>
    {
        public Task<IReadOnlyCollection<TResult>> GetDataAsync(CancellationToken token)
        {
            if (string.IsNullOrEmpty(Location))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(Location));
            }

            using (var reader = new StreamReader(Location))
            {
                using (var csvReader = new CsvReader(reader))
                {
                    return LoadDataAsync(csvReader, token);
                }
            }
        }

        protected abstract string Location { get; }

        protected abstract Task<IReadOnlyCollection<TResult>> LoadDataAsync(CsvReader reader, CancellationToken token);
        
    }
}
