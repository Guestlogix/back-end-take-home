using System;
using FluentAssertions;
using Xunit;

namespace AirTrip.Core.Tests
{
    public sealed class AirlineTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ShouldValidateAirlineName(string name)
        {
            // arrange, act, assert
            Assert.Throws<ArgumentException>(() => new Airline(name, "AA", "AAA", "someCountry"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("AAA")]
        [InlineData("A")]
        public void ShouldValidateAirlineTwoDigitCode(string code)
        {
            // arrange, act, assert
            Assert.ThrowsAny<Exception>(() => new Airline("someAirline", code, "AAA", "someCountry"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("AA")]
        [InlineData("AAAA")]
        public void ShouldValidateAirlineThreeDigitCode(string code)
        {
            // arrange, act, assert
            Assert.ThrowsAny<Exception>(() => new Airline("someAirline", "AA", code, "someCountry"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ShouldValidateAirlineCountry(string countryName)
        {
            // arrange, act, assert
            Assert.Throws<ArgumentException>(() => new Airline("someAirline", "AA", "AAA", countryName));
        }


        [Fact]
        public void ShouldExposeAirlineInformation()
        {
            // arrange
            const string airlineName = "WestJet";
            const string airlineTwoDigitCode = "WS";
            const string airlineThreeDigitCode = "WJA";
            const string airlineCountry = "Canada";

            // act
            var airline = new Airline(airlineName, airlineTwoDigitCode, airlineThreeDigitCode, airlineCountry);

            // assert
            airline.Name.Should().Be(airlineName);
            airline.TwoDigitCode.Should().Be(airlineTwoDigitCode);
            airline.ThreeDigitCode.Should().Be(airlineThreeDigitCode);
            airline.Country.Should().Be(airlineCountry);
        }
    }
}
