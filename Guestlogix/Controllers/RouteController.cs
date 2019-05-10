using Guestlogix.exceptions;
using Guestlogix.Models;
using Guestlogix.repositories.classes;
using Guestlogix.repositories.interfaces;
using System;
using System.Net;
using System.Web.Http;

namespace Guestlogix.Controllers
{
    public class RouteController : ApiController
    {
        private static IAirlineRepository airlineRepo = new AirlineRepository();
        private static IAirportRepository airportRepo = new AirportRepository();
        private static IFlightRepository flightRepo = new FlightRepository();

        [Route("api/route/getshortestroute")]
        [HttpGet]
        public IHttpActionResult GetShortestRoute(string origin, string destination)
        {
            try
            {
                AirNetwork airNetwork = new AirNetwork(airlineRepo.GetAllAirlines(), airportRepo.GetAllAirports(), flightRepo.GetAllFlights());
                return Ok(airNetwork.GetShortestRoute(origin, destination));
            }
            catch (CustomException ce)
            {
                return Content(HttpStatusCode.NotFound, ce.Message);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}