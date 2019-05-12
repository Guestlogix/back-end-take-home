using System;
using System.Threading;
using System.Threading.Tasks;
using AirTrip.Core;
using AirTrip.Services.Services;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;

namespace AirTrip.Main.Endpoints
{
    public class RouteSearchController : Controller
    {
        private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(3);
        private readonly IShortestRouteService _shortestRouteService;

        public RouteSearchController([NotNull] IShortestRouteService shortestRouteService)
        {
            _shortestRouteService = shortestRouteService ?? throw new ArgumentNullException(nameof(shortestRouteService));
        }

        [HttpGet]
        [Route("/routeSearch")]
        public async Task<IActionResult> GetRoutes([FromQuery] string origin,[FromQuery] string destination)
        {
            try
            {
                using (var cts = CancellationTokenSource.CreateLinkedTokenSource(HttpContext.RequestAborted))
                {
                    cts.CancelAfter(Timeout);

                    var routes = await _shortestRouteService
                        .GetShortestRouteAsync(new Airport(origin), new Airport(destination), cts.Token);

                    return Ok(routes);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}