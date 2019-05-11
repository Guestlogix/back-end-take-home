using System;
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
    public class AirlineDataProviderTests
    {
        [Fact]
        public void ShouldThrowIfNoPath()
        {
            // arrange, act, assert
            Assert.Throws<ArgumentException>(() => new AirlineDataProvider().LoadData<Airline>(""));
        }

        [Fact]
        public void ShouldReturnAirlineData()
        {
            // arrange
            var service = new AirlineDataProvider();

            var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var location = Path.Combine(basePath, @"TestData\airlines.csv");

            // act
            var result = service.LoadData<Airline>(location);

            // assert
            result.Count.Should().Be(2);
            AssertAirline(result.ElementAt(0), "United Airlines", "UA", "UAL", "United States");
            AssertAirline(result.ElementAt(1), "WestJet", "WS", "WJA", "Canada");
        }

        [AssertionMethod]
        private static void AssertAirline(Airline airline, string name, string twoDigitCode, string threeDigitCode, string country)
        {
            airline.Name.Should().Be(name);
            airline.TwoDigitCode.Should().Be(twoDigitCode);
            airline.ThreeDigitCode.Should().Be(threeDigitCode);
            airline.Country.Should().Be(country);
        }
    }
}
