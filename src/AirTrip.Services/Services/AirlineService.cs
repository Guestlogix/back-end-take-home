using System.Collections.Generic;
using AirTrip.Core;
using AirTrip.Services.DataProviders;

namespace AirTrip.Services.Services
{
    public sealed class AirlineService
    {
        private IReadOnlyCollection<Airline> _airlines;
        private readonly IDataProvider<Airline> _airlineDataProvider;

        private readonly object _lockInstance = new object();

        public AirlineService(IDataProvider<Airline> airlineDataProvider)
        {
            _airlineDataProvider = airlineDataProvider;
        }

        public IReadOnlyCollection<Airline> GetAllAirlines()
        {
            lock (_lockInstance)
            {
                var airlines = _airlineDataProvider.GetData();

                if (airlines != null)
                {
                    _airlines = airlines;
                }
            }

            return _airlines;
        }
    }
}
