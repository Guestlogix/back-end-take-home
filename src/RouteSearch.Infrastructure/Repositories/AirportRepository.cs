using System.Collections.Generic;
using RouteSearch.Domain.Entities;
using RouteSearch.Domain.Repositories;
using RouteSearch.Infrastructure.Data;

namespace RouteSearch.Infrastructure.Repositories
{
    public class AirportRepository : IAirportRepository
    {
        private DataContext _dataContext;

        public AirportRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Airport GetByIata(string iata)
        {
            Airport airport = null;
            _dataContext.Airports.TryGetValue(iata, out airport);
            return airport;
        }
    }
}