using RouteSearch.Domain.Entities;

namespace RouteSearch.Domain.Repositories
{
    public interface IAirportRepository
    {
         Airport GetByIata(string iata);
    }
}