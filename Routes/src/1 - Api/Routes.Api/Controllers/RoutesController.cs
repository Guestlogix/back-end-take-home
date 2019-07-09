using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Domain.Modules.Repositories;
using Routes.Api.Support;
using Routes.Infra.Data.Data;
using Routes.Infra.Data.Repositories;

namespace Routes.Api.Controllers
{
    /// <summary>
    /// Api Routes
    /// </summary>
    [RoutePrefix("routes")]
    public class RoutesController : ApiController
    {
        private IFlightRepository FlightRepository { get; set; }

        /// <summary>
        ///     Create a new instance of class <see cref="RoutesController" />
        /// </summary>
        public RoutesController(IFlightRepository flightRepository)
        {
            FlightRepository = flightRepository;
        }
        
        /// <summary>
        ///     Return the best route between origin and destination.
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-route-origin-destination")]
        [ResponseType(typeof(string))]
        public HttpResponseMessage GetRouteOriginDestination(string origin, string destination)
        {
            var responseMessage = new ResponseMessage(Request);
            if (string.IsNullOrWhiteSpace(origin) || string.IsNullOrWhiteSpace(destination))
                return responseMessage.CreateNoAsync(HttpStatusCode.NotFound, "Origin and/or Destination is empty.");

            // Get All AirPorts
            var airPorts = LoadDataToCsv.AirPortsRoutes();

            var result = FlightRepository.GetShortestRoute(airPorts, origin.ToUpper(), destination.ToUpper());
            return responseMessage.CreateNoAsync(HttpStatusCode.OK, result);
        }
    }
}