using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AirTrip.Core.Models;
using AirTrip.Services.Services;
using FluentAssertions;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace AirTrip.Services.Tests.Services
{
    public sealed class ShortestRouteServiceTests
    {
        [Fact]
        public void ShouldThrow_WhenNullDependencies()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => new ShortestRouteService(null, NullLogger<ShortestRouteService>.Instance));
        }

        [Fact]
        public async Task ShouldReturnDirectRoute()
        {
            // arrange
            var routes = new[]
            {
                new Route(new Airline("AC"), new Airport("YYZ"), new Airport("YOW"))
            };

            var mockRouteService = new MockRouteService(routes);
            var logger = NullLogger<ShortestRouteService>.Instance;
            var service = new ShortestRouteService(mockRouteService, logger);

            var origin = new Airport("YYZ");
            var destination = new Airport("YOW");

            // act
            var result = await service.GetShortestRouteAsync(origin, destination, CancellationToken.None);

            // assert
            result.Should().BeEquivalentTo(origin, destination);
        }

        [Fact]
        public async Task ShouldReturnOneHopRoute()
        {
            // arrange
            var airline = new Airline("AC");

            var toronto = new Airport("YYZ");
            var montreal = new Airport("YUL");
            var ottawa = new Airport("YOW");
            var hamilton = new Airport("YHM");

            var routes = new[]
            {
                new Route(airline, toronto, hamilton),
                new Route(airline, hamilton, montreal),
                new Route(airline, toronto, ottawa),
                new Route(airline, ottawa, montreal)
            };

            var mockRouteService = new MockRouteService(routes);
            var logger = NullLogger<ShortestRouteService>.Instance;
            var service = new ShortestRouteService(mockRouteService, logger);

            // act
            var result = await service.GetShortestRouteAsync(toronto, montreal, CancellationToken.None);

            // assert
            result.Should().BeEquivalentTo(toronto, hamilton, montreal);
        }

        [Fact]
        public async Task ShouldReturnMoreHopData()
        {
            // arrange
            var airline = new Airline("AC");

            var toronto = new Airport("YYZ");
            var montreal = new Airport("YUL");
            var ottawa = new Airport("YOW");
            var hamilton = new Airport("YHM");
            var calgary = new Airport("YYC");
            var moncton = new Airport("YQM");
            var halifax = new Airport("YHZ");

            var routes = new[]
            {
                new Route(airline, toronto, hamilton),
                new Route(airline, hamilton, calgary),
                new Route(airline, calgary, moncton),
                new Route(airline, calgary, halifax),
                new Route(airline, toronto, ottawa),
                new Route(airline, ottawa, montreal),
                new Route(airline, montreal, halifax)
            };

            var mockRouteService = new MockRouteService(routes);
            var logger = NullLogger<ShortestRouteService>.Instance;
            var service = new ShortestRouteService(mockRouteService, logger);

            // act
            var result = await service.GetShortestRouteAsync(toronto, halifax, CancellationToken.None);

            // assert
            result.Should().BeEquivalentTo(toronto, ottawa, montreal, halifax);
        }

        private class MockRouteService : IRouteService
        {
            private readonly IReadOnlyCollection<Route> _routes;

            public MockRouteService([NotNull] IReadOnlyCollection<Route> routes)
            {
                _routes = routes ?? throw new ArgumentNullException(nameof(routes));
            }

            public Task<IReadOnlyCollection<Route>> GetAllRoutesAsync(CancellationToken cancellationToken)
            {
                return Task.FromResult(_routes);
            }
        }
    }
}