using System.Collections.Generic;
using System.Linq;
using RouteSearch.Domain.Entities;
using RouteSearch.Domain.Repositories;
using RouteSearch.Infrastructure.Data;

namespace RouteSearch.Infrastructure.Repositories
{
    public class RouteRepository : IRouteRepository
    {
        private readonly DataContext _dataContext;

        public RouteRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public List<Route> GetDestinationConnections(Airport origin)
        {
            return _dataContext.Routes.Where(r => r.Origin == origin).ToList();
        }

        public bool HasConnections(Airport origin, Airport destination)
        {
            if (!_dataContext.Routes.Any(route => route.Origin == origin))
                return false;
            
            if (!_dataContext.Routes.Any(route => route.Destination == destination))
                return false;

            return true;
        }
    }
}