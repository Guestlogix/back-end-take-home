using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AirTrip.Core;
using AirTrip.Services.Services;
using FluentAssertions;
using Xunit;

namespace AirTrip.Services.Tests.Services
{
    public sealed class ShortestRouteServiceTests
    {
        [Fact]
        public void ShouldThrow_WhenNullDependencies()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => new ShortestRouteService(null));
        }

        [Fact]
        public async Task ShouldReturnDirectRoute()
        {
            var service = new ShortestRouteService(new MockRouteService());

            var origin = new Airport("YYZ");
            var destination = new Airport("YOW");

            var result = await service.GetShortestRouteAsync(origin, destination, CancellationToken.None);

            var shortestRoute = result.Single();
            shortestRoute.Airports.Should().BeEquivalentTo(origin, destination);
        }

        private class MockRouteService : IRouteService
        {
            public Task<IReadOnlyCollection<Route>> GetAllRoutesAsync(CancellationToken cancellationToken)
            {
                var directRoute = new Route(new Airline("AC"), new Airport("YYZ"), new Airport("YOW"));

                var routes = new[] {directRoute};

                return Task.FromResult((IReadOnlyCollection<Route>) routes);
            }
        }
    }
}