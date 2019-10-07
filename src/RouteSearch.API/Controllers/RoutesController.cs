using Microsoft.AspNetCore.Mvc;
using RouteSearch.Application.DTO;
using RouteSearch.Application.RouteFinder.Interfaces;

namespace RouteSearch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private readonly IRouteFinder _routeFinder;

        public RoutesController(IRouteFinder routeFinder)
        {
            _routeFinder = routeFinder;
        }

        [HttpGet]
        public ActionResult<FlightRouteDTO> Get(string origin, string destination)
        {
            return _routeFinder.FindFlightRoute(origin, destination);
        }
    }
}