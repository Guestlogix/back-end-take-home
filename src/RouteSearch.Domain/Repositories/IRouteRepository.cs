using System.Collections.Generic;
using RouteSearch.Domain.Entities;

namespace RouteSearch.Domain.Repositories
{
    public interface IRouteRepository
    {
        List<Route> GetDestinationConnections(Airport origin);
    }
}