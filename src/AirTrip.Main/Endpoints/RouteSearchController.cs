using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AirTrip.Core.Exceptions;
using AirTrip.Core.Models;
using AirTrip.Main.Endpoints.Examples;
using AirTrip.Main.Endpoints.Models;
using AirTrip.Services.Services;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.Examples;

namespace AirTrip.Main.Endpoints
{
    public class RouteSearchController : Controller
    {
        private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(10);
        private readonly IShortestRouteService _shortestRouteService;
        private readonly ILogger<RouteSearchController> _logger;

        public RouteSearchController(
            [NotNull] IShortestRouteService shortestRouteService,
            [NotNull] ILogger<RouteSearchController> logger)
        {
            _shortestRouteService = shortestRouteService ?? throw new ArgumentNullException(nameof(shortestRouteService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Returns shortest route between Origin and Destination Airport
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/routeSearch")]
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(SuccessfulResponseExample))]
        [ProducesResponseType(typeof(SuccessResponse), 200)]
        public async Task<IActionResult> GetShortestRouteAsync(
            [FromQuery] [NotNull] string origin,
            [FromQuery] [NotNull] string destination)
        {
            if (string.IsNullOrEmpty(origin))
            {
                return BadRequest(new ErrorResponse
                {
                    Error = $"{nameof(origin)} is null or empty"
                });
            }

            if (string.IsNullOrEmpty(destination))
            {
                return BadRequest(new ErrorResponse
                {
                    Error = $"{nameof(destination)} is null or empty"
                });
            }

            if (origin == destination)
            {
                return BadRequest(new ErrorResponse
                {
                    Error = "Origin and Destination Airport Cannot Be the Same"
                });
            }

            try
            {
                using (var cts = CancellationTokenSource.CreateLinkedTokenSource(HttpContext.RequestAborted))
                {
                    cts.CancelAfter(Timeout);

                    var routes = await _shortestRouteService
                        .GetShortestRouteAsync(new Airport(origin), new Airport(destination), cts.Token);

                    return !routes.Any() 
                        ? Ok(new ErrorResponse {Error = $"No Route found from {origin} to {destination}"}) 
                        : Ok(Map(routes));
                }
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError(ex, ex.Message);

                return Ok("The Task Was Cancelled");
            }
            catch (Exception ex)
            {
                if (ex is RouteNotSupportedException || ex is BadAirportException)
                {
                    return BadRequest(ex.Message);
                }

                return Ok(new ErrorResponse { Error = "It's not you. Its Us. Something went down. Try again after sometime" });
            }
        }

        private static SuccessResponse Map(IEnumerable<Airport> routes)
        {
            return new SuccessResponse
            {
                ShortestRoute = routes.Select(i => i.Code)
            };
        }
    }
}