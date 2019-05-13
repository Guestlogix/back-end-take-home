using System;
using AirTrip.Core.Models;
using FluentAssertions;
using Xunit;

namespace AirTrip.Core.Tests.Models
{
    public sealed class AirlineTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("AAA")]
        [InlineData("A")]
        public void ShouldValidateAirlineTwoDigitCode(string code)
        {
            // arrange, act, assert
            Assert.ThrowsAny<Exception>(() => new Airline(code));
        }

        [Fact]
        public void ShouldExposeAirlineInformation()
        {
            // arrange
            const string airlineTwoDigitCode = "WS";

            // act
            var airline = new Airline(airlineTwoDigitCode);

            // assert
            airline.Code.Should().Be(airlineTwoDigitCode);
        }
    }
}
