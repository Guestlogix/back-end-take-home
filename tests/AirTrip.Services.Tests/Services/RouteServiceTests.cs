using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AirTrip.Core;
using AirTrip.Services.DataProviders;
using AirTrip.Services.Services;
using FluentAssertions;
using Xunit;

namespace AirTrip.Services.Tests.Services
{
    public sealed class RouteServiceTests
    {
        [Fact]
        public async Task ShouldReturnNoData_WhenProviderReturnsNoData()
        {
            // arrange
            var service = new RouteService(new EmptyDataProvider());

            // act
            var result = await service.GetAllRoutesAsync(CancellationToken.None);

            // assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task ShouldReturnData_WhenProviderReturnsData()
        {
            // arrange
            var service = new RouteService(new MockDataProvider());

            // act
            var result = await service.GetAllRoutesAsync(CancellationToken.None);

            // assert
            result.Should().NotBeEmpty();
            result.Count.Should().Be(2);
            result.ElementAt(0).Origin.Should().Be(new Airport("YYZ"));
            result.ElementAt(0).Destination.Should().Be(new Airport("YOW"));

            result.ElementAt(1).Origin.Should().Be(new Airport("YOW"));
            result.ElementAt(1).Destination.Should().Be(new Airport("YUL"));
        }

        private sealed class MockDataProvider : IDataProvider<Route>
        {
            public Task<IReadOnlyCollection<Route>> GetDataAsync(CancellationToken token)
            {
                var routes = new[]
                {
                    new Route("AC", new Airport("YYZ"), new Airport("YOW")),
                    new Route("AC", new Airport("YOW"), new Airport("YUL"))
                };

                return Task.FromResult((IReadOnlyCollection<Route>) routes);
            }
        }

        private sealed class EmptyDataProvider : IDataProvider<Route>
        {
            public Task<IReadOnlyCollection<Route>> GetDataAsync(CancellationToken token)
            {
                return Task.FromResult((IReadOnlyCollection<Route>) null);
            }
        }
    }
}