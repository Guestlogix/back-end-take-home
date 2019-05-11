using AirTrip.Main.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace AirTrip.Main.Tests.Endpoints
{
    public class HealthControllerTests
    {
        [Fact]
        public void ShouldReturnHealthResponse()
        {
            var controller = new HealthController();

            var response = controller.CheckHealth();

            var result = Assert.IsType<OkObjectResult>(response);
            Assert.Equal("Hello World", result.Value);
        }
    }
}
