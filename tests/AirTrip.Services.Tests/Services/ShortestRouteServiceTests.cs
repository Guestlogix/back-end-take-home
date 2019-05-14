using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AirTrip.Core.Exceptions;
using AirTrip.Core.Models;
using AirTrip.Services.Services;
using FluentAssertions;
using JetBrains.Annotations;
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

        [Theory]
        [MemberData(nameof(NullData))]
        public async Task ShouldThrowWhenNullInputs(Airport origin, Airport destination)
        {
            var mockRouteService = new MockRouteService(Array.Empty<Route>());
            var service = new ShortestRouteService(mockRouteService);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await service.GetShortestRouteAsync(origin, destination, CancellationToken.None));
        }

        public static IEnumerable<object[]> NullData => new List<object[]>
        {
            new object[] {null, new Airport("YYY")},
            new object[] {new Airport("YYY"), null}
        };

        [Fact]
        public async Task ShouldThrow_WhenOriginAirportIsNotSupported()
        {
            // arrange
            var toronto = new Airport("YYZ");
            var ottawa = new Airport("YOW");
            var denver = new Airport("DEN");

            var routes = new[]
            {
                new Route(toronto, ottawa)
            };

            var mockRouteService = new MockRouteService(routes);
            var service = new ShortestRouteService(mockRouteService);

            // act & assert
            await Assert.ThrowsAsync<RouteNotSupportedException>(async () =>
                await service.GetShortestRouteAsync(denver, ottawa, CancellationToken.None));
        }

        [Fact]
        public async Task ShouldReturnEmptyCollection_WhenNoRouteExits()
        {
            // arrange
            var toronto = new Airport("YYZ");
            var ottawa = new Airport("YOW");
            var montreal = new Airport("YUL");
            var hamilton = new Airport("YHM");
            var halifax = new Airport("YHZ");

            var routes = new[]
            {
                new Route(toronto, hamilton),
                new Route(toronto, ottawa),
                new Route(montreal, halifax)
            };

            var mockRouteService = new MockRouteService(routes);
            var service = new ShortestRouteService(mockRouteService);

            // act
            var result = await service.GetShortestRouteAsync(toronto, montreal, CancellationToken.None);

            // assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task ShouldReturnDirectRoute()
        {
            // arrange
            var routes = new[]
            {
                new Route(new Airport("YYZ"), new Airport("YOW"))
            };

            var mockRouteService = new MockRouteService(routes);
            var service = new ShortestRouteService(mockRouteService);

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
            var toronto = new Airport("YYZ");
            var montreal = new Airport("YUL");
            var ottawa = new Airport("YOW");
            var hamilton = new Airport("YHM");

            var routes = new[]
            {
                new Route(toronto, hamilton),
                new Route(hamilton, montreal),
                new Route(toronto, ottawa),
                new Route(ottawa, montreal)
            };

            var mockRouteService = new MockRouteService(routes);
            var service = new ShortestRouteService(mockRouteService);

            // act
            var result = await service.GetShortestRouteAsync(toronto, montreal, CancellationToken.None);

            // assert
            result.Should().BeEquivalentTo(toronto, hamilton, montreal);
        }

        [Fact]
        public async Task ShouldReturnMoreHopData()
        {
            // arrange

            var toronto = new Airport("YYZ");
            var montreal = new Airport("YUL");
            var ottawa = new Airport("YOW");
            var hamilton = new Airport("YHM");
            var calgary = new Airport("YYC");
            var moncton = new Airport("YQM");
            var halifax = new Airport("YHZ");

            var routes = new[]
            {
                new Route(toronto, hamilton),
                new Route(hamilton, calgary),
                new Route(calgary, moncton),
                new Route(moncton, halifax),
                new Route(toronto, ottawa),
                new Route(ottawa, montreal),
                new Route(montreal, halifax)
            };

            var mockRouteService = new MockRouteService(routes);
            var service = new ShortestRouteService(mockRouteService);

            // act
            var result = await service.GetShortestRouteAsync(toronto, halifax, CancellationToken.None);

            // assert
            result.Should().BeEquivalentTo(toronto, ottawa, montreal, halifax);
        }

        private sealed class MockRouteService : IRouteService
        {
            private readonly IReadOnlyCollection<Route> _routes;

            public MockRouteService([NotNull] IReadOnlyCollection<Route> routes)
            {
                _routes = routes ?? throw new ArgumentNullException(nameof(routes));
            }

            public Task<IReadOnlyCollection<Route>> GetAllRoutesAsync(CancellationToken token)
            {
                return Task.FromResult(_routes);
            }
        }
    }
}