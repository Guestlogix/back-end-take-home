using System.IO;
using System.Linq;
using System.Reflection;
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
        public void ShouldReturnRouteData()
        {
            // arrange
            var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var location = Path.Combine(basePath, @"TestData\routes.csv");

            var service = new RouteDataProvider(location);

            // act
            var result = service.GetData();

            // assert
            result.Count.Should().Be(2);
            AssertRoute(result.ElementAt(0), "AC", "ABJ", "BRU");
            AssertRoute(result.ElementAt(1), "AC", "ABJ", "OUA");
        }

        [AssertionMethod]
        private static void AssertRoute(Route route, string airline, string origin, string destination)
        {
            route.Airline.Should().Be(airline);
            route.Origin.Should().Be(origin);
            route.Destination.Should().Be(destination);
        }
    }
}