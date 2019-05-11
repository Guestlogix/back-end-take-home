using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;

namespace AirTrip.Services.DataProviders
{
    public abstract class DataProvider
    {
        public IReadOnlyCollection<TResult> LoadData<TResult>(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(path));
            }

            using (var reader = new StreamReader(path))
            {
                using (var csvReader = new CsvReader(reader))
                {
                    return ParseData<TResult>(csvReader);
                }
            }
        }

        protected abstract IReadOnlyCollection<TResult> ParseData<TResult>(CsvReader reader);
    }
}
