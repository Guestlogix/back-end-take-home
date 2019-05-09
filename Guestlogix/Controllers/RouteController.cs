using Guestlogix.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace Guestlogix.Controllers
{
    public class RouteController : ApiController
    {
        [Route("api/route/getshortestroute")]
        [HttpGet]
        public IHttpActionResult GetShortestRoute(string origin, string destination)
        {
            RouteModel rm = new RouteModel();
            rm.Flights = new List<FlightModel>();
            rm.Flights.Add(new FlightModel
            {
                AirlineId = "abc",
                Origin = "o",
                Destination = "d"
            });
            //return BadRequest("error");
            return Ok(rm);
        }
    }
}