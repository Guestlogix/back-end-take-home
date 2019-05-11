using System;
using Xunit;

namespace AirTrip.Core.Tests
{
    public sealed class AirportTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ShouldValidateAirportName(string airport)
        {
            // arrange, act, assert
            Assert.Throws<ArgumentException>(() => new Airport(airport, "someCity", "someCountry", "AAA"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ShouldValidateCityName(string city)
        {
            // arrange, act, assert
            Assert.Throws<ArgumentException>(() => new Airport("someAirport", city, "someCountry", "AAA"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ShouldValidateCountryName(string country)
        {
            // arrange, act, assert
            Assert.Throws<ArgumentException>(() => new Airport("someAirport", "someCity", country, "AAA"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("123")]
        [InlineData("A")]
        [InlineData("AA")]
        [InlineData("AAAA")]
        [InlineData("AA12")]
        [InlineData("AA2")]
        public void ShouldValidateAirportCode(string airportCode)
        {
            // arrange, act, assert
            Assert.ThrowsAny<Exception>(() => new Airport("someAirport", "someCity", "someCountry", airportCode));
        }
    }
}