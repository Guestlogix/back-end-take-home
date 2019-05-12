using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AirTrip.Core;

namespace AirTrip.Services.Services
{
    public interface IShortestRouteService
    {
        Task<IReadOnlyCollection<ShortestRoute>> GetShortestRouteAsync(
            Airport origin,
            Airport destination,
            CancellationToken cancellationToken);
    }
}