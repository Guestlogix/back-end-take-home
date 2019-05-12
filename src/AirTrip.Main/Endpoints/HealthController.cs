using Microsoft.AspNetCore.Mvc;

namespace AirTrip.Main.Endpoints
{
    public class HealthController : Controller
    {
        /// <summary>
        /// Endpoint to check the health of the application
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/health")]
        public IActionResult CheckHealth()
        {
            return Ok("Hello World");
        }
    }
}
