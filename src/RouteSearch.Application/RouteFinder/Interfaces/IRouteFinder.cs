using RouteSearch.Application.DTO;

namespace RouteSearch.Application.RouteFinder.Interfaces
{
    public interface IRouteFinder
    {
         FlightRouteDTO FindFlightRoute(string origin, string destination);
    }
}