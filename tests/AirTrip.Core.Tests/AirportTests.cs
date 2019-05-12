using System;
using Xunit;

namespace AirTrip.Core.Tests
{
    public sealed class AirportTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("123")]
        [InlineData("A")]
        [InlineData("AA")]
        [InlineData("AAAA")]
        [InlineData("AA12")]
        [InlineData("AA2")]
        public void ShouldValidateAirportCode(string code)
        {
            // arrange, act, assert
            Assert.ThrowsAny<Exception>(() => new Airport(code));
        }
    }
}