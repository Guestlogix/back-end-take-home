using AutoMapper;
using RouteSearch.Application.DTO;
using RouteSearch.Application.RouteFinder.Interfaces;
using RouteSearch.Domain.Entities;
using RouteSearch.Domain.Services.Interfaces;

namespace RouteSearch.Application.RouteFinder
{
    public class RouteFinder : IRouteFinder
    {
        private readonly IRouteFinderService _routeFinderService;
        private readonly IMapper _mapper;

        public RouteFinder(IRouteFinderService routeFinderService, IMapper mapper)
        {
            _routeFinderService = routeFinderService;
            _mapper = mapper;
        }

        public FlightRouteDTO FindFlightRoute(string origin, string destination)
        {
            RouteFinderValidator.Validate(origin, destination);
            FlightRoute flightRoute = _routeFinderService.Find(origin.ToUpper(), destination.ToUpper());
            return _mapper.Map<FlightRoute, FlightRouteDTO>(flightRoute);
        }
    }
}