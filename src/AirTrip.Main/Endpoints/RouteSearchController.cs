using System;
using System.Threading;
using System.Threading.Tasks;
using AirTrip.Services.Services;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;

namespace AirTrip.Main.Endpoints
{
    public class RouteSearchController : Controller
    {
        private readonly IRouteService _routeService;
        private static readonly TimeSpan Timeout = TimeSpan.FromMilliseconds(3);

        public RouteSearchController([NotNull] IRouteService routeService)
        {
            _routeService = routeService ?? throw new ArgumentNullException(nameof(routeService));
        }

        [Route("/routeSearch")]
        public async Task<IActionResult> GetRoutes()
        {
            try
            {
                using (var cts = CancellationTokenSource.CreateLinkedTokenSource(HttpContext.RequestAborted))
                {
                    cts.CancelAfter(Timeout);

                    var routes = await _routeService.GetAllRoutesAsync(cts.Token);

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