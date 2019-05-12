using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AirTrip.Core;
using AirTrip.Services.DataProviders;
using JetBrains.Annotations;

namespace AirTrip.Services.Services
{
    internal sealed class AirlineService : IAirlineService
    {
        private IReadOnlyCollection<Airline> _airlines;
        private readonly IDataProvider<Airline> _airlineDataProvider;

        public AirlineService([NotNull] IDataProvider<Airline> airlineDataProvider)
        {
            _airlineDataProvider = airlineDataProvider ?? throw new ArgumentNullException(nameof(airlineDataProvider));
        }

        public async Task<IReadOnlyCollection<Airline>> GetAllAirlinesAsync(CancellationToken cancellationToken)
        {
            if (_airlines == null)
            {
                var airlines = await _airlineDataProvider.GetDataAsync(cancellationToken);

                if (airlines != null)
                {
                    _airlines = airlines;
                    return _airlines;
                }

                return Array.Empty<Airline>();
            }

            return Array.Empty<Airline>();
        }
    }
}