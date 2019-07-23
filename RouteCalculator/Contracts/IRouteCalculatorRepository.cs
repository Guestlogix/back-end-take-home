using System.Collections.Generic;
using RouteCalculator.Entities.Dtos;

namespace RouteCalculator.Contracts
{
    public interface IRouteCalculatorRepository
    {
        IEnumerable<Route> GetRoutes();

        IEnumerable<Airport> GetAirports();
    }
}
