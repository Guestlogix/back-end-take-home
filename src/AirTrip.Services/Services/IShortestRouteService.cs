using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AirTrip.Core.Models;

namespace AirTrip.Services.Services
{
    public interface IShortestRouteService
    {
        Task<IReadOnlyCollection<Airport>> GetShortestRouteAsync(
            Airport origin,
            Airport destination,
            CancellationToken token);
    }
}