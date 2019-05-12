using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using AirTrip.Core;
using AirTrip.Services.DataProviders;
using FluentAssertions;
using JetBrains.Annotations;
using Xunit;

namespace AirTrip.Services.Tests.DataProviders
{
    public class RouteDataProviderTests
    {
        [Fact]
        public async Task ShouldReturnRouteData()
        {
            // arrange
            var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var location = Path.Combine(basePath, @"TestData\routes.csv");

            var service = new RouteDataProvider(location);

            // act
            var result = await service.GetDataAsync(CancellationToken.None);

            // assert
            result.Count.Should().Be(2);
            AssertRoute(result.ElementAt(0), "AC", new Airport("ABJ"), new Airport("BRU"));
            AssertRoute(result.ElementAt(1), "AC", new Airport("ABJ"), new Airport("OUA"));
        }

        [AssertionMethod]
        private static void AssertRoute(Route route, string airline, Airport origin, Airport destination)
        {
            route.Airline.Should().Be(airline);
            route.Origin.Should().Be(origin);
            route.Destination.Should().Be(destination);
        }
    }
}