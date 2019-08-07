using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Process;

namespace FastestRouteCalculator.Controllers
{
    [Route("routes")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private readonly IContext _context;
        private readonly IRouteProcessor _processor;

        public RoutesController(IContext context, IRouteProcessor processor)
        {
            _context = context;
            _processor = processor;
        }
        
        [HttpGet("{origin}/{destination}")]
        public ActionResult<string> Get(string origin, string destination)
        {
            if (_context.Airports.All(x => !x.IATA3.Equals(origin, StringComparison.OrdinalIgnoreCase)))
            {
                return BadRequest($"Invalid Origin '{origin}'");
            }

            if (_context.Airports.All(x => !x.IATA3.Equals(destination, StringComparison.OrdinalIgnoreCase)))
            {
                return BadRequest($"Invalid Destination '{destination}'");
            }

            var result = _processor.FindShortestRoute(origin, destination);

            if (string.IsNullOrEmpty(result))
            {
                return BadRequest($"No route was found for Origin '{origin}' and Destination '{destination}'");
            }

            return result;
        }
    }
}
