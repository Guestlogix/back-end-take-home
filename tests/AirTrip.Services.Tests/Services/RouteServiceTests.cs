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
            result.ElementAt(0).Origin.Should().Be("YYZ");
            result.ElementAt(0).Destination.Should().Be("YOW");

            result.ElementAt(1).Origin.Should().Be("YOW");
            result.ElementAt(1).Destination.Should().Be("YUL");
        }

        private sealed class MockDataProvider : IDataProvider<Route>
        {
            public IReadOnlyCollection<Route> GetData()
            {
                return new[]
                {
                    new Route("AC", "YYZ", "YOW"),
                    new Route("AC", "YOW", "YUL")
                };
            }
        }

        private sealed class EmptyDataProvider : IDataProvider<Route>
        {
            public IReadOnlyCollection<Route> GetData()
            {
                return null;
            }
        }
    }
}