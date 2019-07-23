using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RouteCalculator.Contracts;
using RouteCalculator.Entities;

namespace RouteCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesCalculatorController : ControllerBase
    {
        private readonly IRouteCalculatorService _service;

        public RoutesCalculatorController(IRouteCalculatorService service)
        {
            _service = service;
        }

        // GET api/values
        [HttpGet("{origin}/{destination}")]
        [ProducesResponseType(typeof(Entities.Dtos.Route), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public ActionResult Get(string origin, string destination)
        {
            var result = _service.GetShortestRoute(origin, destination, out var errorCode);

            if (errorCode == Error.None)
            {
                return Ok(result);
            }

            if (errorCode == Error.NoRouteFound)
            {
                return NotFound(errorCode.Code);
            }

            return BadRequest(errorCode.Code);
        }
    }
}