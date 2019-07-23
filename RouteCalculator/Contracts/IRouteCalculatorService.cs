using System.Collections.Generic;
using RouteCalculator.Entities;
using RouteCalculator.Entities.Models;

namespace RouteCalculator.Contracts
{
    public interface IRouteCalculatorService
    {
        IEnumerable<Entities.Dtos.Route> GetShortestRoute(string originAirportCode, string destinationAirportCode,
            out Error errorCode);
    }
}