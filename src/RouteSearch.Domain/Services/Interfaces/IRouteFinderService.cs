using RouteSearch.Domain.Entities;

namespace RouteSearch.Domain.Services.Interfaces
{
    public interface IRouteFinderService
    {
         FlightRoute Find(string origin, string destination);
    }
}