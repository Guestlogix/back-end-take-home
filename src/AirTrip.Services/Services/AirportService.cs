using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AirTrip.Core;
using AirTrip.Services.DataProviders;
using JetBrains.Annotations;

namespace AirTrip.Services.Services
{
    internal sealed class AirportService : IAirportService
    {
        private readonly IDataProvider<Airport> _airportDataProvider;
        private IReadOnlyCollection<Airport> _airportCache;

        public AirportService([NotNull] IDataProvider<Airport> airportDataProvider)
        {
            _airportDataProvider = airportDataProvider ?? throw new ArgumentNullException(nameof(airportDataProvider));
        }

        public async Task<IReadOnlyCollection<Airport>> GetAirportsAsync(CancellationToken token)
        {
            if (_airportCache == null)
            {
                var airports = await _airportDataProvider.GetDataAsync(token);

                if (airports == null)
                {
                    return Array.Empty<Airport>();
                }

                _airportCache = airports;
                return _airportCache;
            }

            return _airportCache;
        }

    }
}