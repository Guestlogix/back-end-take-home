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
    public class AirportDataProviderTests
    {
        [Fact]
        public void ShouldReturnAirportData()
        {
            // arrange
            var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var location = Path.Combine(basePath, @"TestData\airports.csv");

            var service = new AirportDataProvider(location);

            // act
            var result = service.GetData();

            // assert
            result.Count.Should().Be(2);
            AssertAirport(result.ElementAt(0), "Thunder Bay Airport", "Thunder Bay", "Canada", "YQT");
            AssertAirport(result.ElementAt(1), "Billy Bishop Toronto City Centre Airport", "Toronto", "Canada", "YTZ");
        }

        [AssertionMethod]
        private static void AssertAirport(Airport airport, string name, string city, string country, string code)
        {
            airport.Name.Should().Be(name);
            airport.City.Should().Be(city);
            airport.Country.Should().Be(country);
            airport.Code.Should().Be(code);
        }
    }
}