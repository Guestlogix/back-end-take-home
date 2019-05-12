using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AirTrip.Core;
using AirTrip.Services.DataProviders;
using JetBrains.Annotations;

namespace AirTrip.Services.Services
{
    public sealed class AirlineService : IAirlineService
    {
        private IReadOnlyCollection<Airline> _airlines;
        private readonly IDataProvider<Airline> _airlineDataProvider;

        public AirlineService([NotNull] IDataProvider<Airline> airlineDataProvider)
        {
            _airlineDataProvider = airlineDataProvider ?? throw new ArgumentNullException(nameof(airlineDataProvider));
        }

        public async Task<IReadOnlyCollection<Airline>> GetAllAirlinesAsync(CancellationToken cancellationToken)
        {
            _airlines = _airlineDataProvider.GetData() ?? Array.Empty<Airline>();

            return await Task.FromResult(_airlines);
        }
    }
}
