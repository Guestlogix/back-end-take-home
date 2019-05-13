using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AirTrip.Core;
using AirTrip.Core.Models;

namespace AirTrip.Services.Services
{
    internal interface IAirportService
    {
        Task<IReadOnlyCollection<Airport>> GetAirportsAsync(CancellationToken token);
    }
}