using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AirTrip.Core.Models;
using AirTrip.Main.Endpoints;
using AirTrip.Main.Endpoints.Models;
using AirTrip.Services.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace AirTrip.Main.Tests.Endpoints
{
    public class RouteSearchControllerTests
    {
        [Theory]
        [InlineData(null, "AAA")]
        [InlineData("AAA", null)]
        [InlineData("", "AAA")]
        [InlineData("AAA", "")]
        public async Task ShouldReturnBadRequest_WhenInvalidInputs(string origin, string destination)
        {
            // arrange
            var shortestRouteService = new MockRouteService();
            var logger = NullLogger<RouteSearchController>.Instance;
            var controller = new RouteSearchController(shortestRouteService, logger);

            // act
            var result = await controller.GetShortestRouteAsync(origin, destination);

            // assert
            var okResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<ErrorResponse>(okResult.Value);
        }

        [Fact]
        public async Task ShouldReturnBadRequest_WhenSameOriginAndDestination()
        {
            // arrange
            var shortestRouteService = new MockRouteService();
            var logger = NullLogger<RouteSearchController>.Instance;
            var controller = new RouteSearchController(shortestRouteService, logger);

            // act
            var result = await controller.GetShortestRouteAsync("AAA", "AAA");

            // assert
            var okResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<ErrorResponse>(okResult.Value);
        }

        [Fact]
        public async Task ShouldReturnShortestRoute()
        {
            // arrange
            var shortestRouteService = new MockRouteService();
            var logger = NullLogger<RouteSearchController>.Instance;
            var controller = new RouteSearchController(shortestRouteService, logger)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        RequestAborted = CancellationToken.None
                    }
                }
            };

            // act
            var result = await controller.GetShortestRouteAsync("AAA", "BBB");

            // assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<SuccessResponse>(okResult.Value);
            response.ShortestRoute.Should().BeEquivalentTo("AAA", "BBB");
        }

        [Fact]
        public async Task ShouldReturnError_WhenNoRoute()
        {
            // arrange
            var shortestRouteService = new EmptyRouteService();
            var logger = NullLogger<RouteSearchController>.Instance;
            var controller = new RouteSearchController(shortestRouteService, logger)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        RequestAborted = CancellationToken.None
                    }
                }
            };

            const string origin = "AAA";
            const string destination = "CCC";

            // act
            var result = await controller.GetShortestRouteAsync(origin, destination);

            // assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ErrorResponse>(okResult.Value);
            response.Error.Should().Be($"No Route found from {origin} to {destination}");
        }

        [Fact]
        public async Task ShouldReturnError_WhenStuffGoesDown()
        {
            // arrange
            var shortestRouteService = new ErrorRouteService();
            var logger = NullLogger<RouteSearchController>.Instance;
            var controller = new RouteSearchController(shortestRouteService, logger)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        RequestAborted = CancellationToken.None
                    }
                }
            };

            const string origin = "AAA";
            const string destination = "CCC";

            // act
            var result = await controller.GetShortestRouteAsync(origin, destination);

            // assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<ErrorResponse>(okResult.Value);
        }

        private sealed class ErrorRouteService : IShortestRouteService
        {
            public Task<IReadOnlyCollection<Airport>> GetShortestRouteAsync(
                Airport origin,
                Airport destination,
                
                CancellationToken token)
            {
                throw new Exception();
            }
        }

        private sealed class EmptyRouteService : IShortestRouteService
        {
            public Task<IReadOnlyCollection<Airport>> GetShortestRouteAsync(
                Airport origin,
                Airport destination,
                CancellationToken token)
            {
                return Task.FromResult((IReadOnlyCollection<Airport>) Array.Empty<Airport>());
            }
        }

        private sealed class MockRouteService : IShortestRouteService
        {
            public Task<IReadOnlyCollection<Airport>> GetShortestRouteAsync(
                Airport origin,
                Airport destination,
                CancellationToken token)
            {
                var shortestRoute = new[] {origin, destination};
                return Task.FromResult((IReadOnlyCollection<Airport>) shortestRoute);
            }
        }
    }
}