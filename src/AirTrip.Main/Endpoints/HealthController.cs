using System.Collections.Generic;
using AirTrip.Core;
using Microsoft.AspNetCore.Mvc;

namespace AirTrip.Main.Endpoints
{
    public class HealthController : Controller
    {
        [HttpGet]
        [Route("/health")]
        public IActionResult CheckHealth()
        {
            return Ok("Hello World");
        }
    }
}
