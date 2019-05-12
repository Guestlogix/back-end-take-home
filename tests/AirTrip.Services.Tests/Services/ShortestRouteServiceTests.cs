using System;
using AirTrip.Services.Services;
using Xunit;

namespace AirTrip.Services.Tests.Services
{
    public sealed class ShortestRouteServiceTests
    {
        [Fact]
        public void ShouldThrow_WhenNullDependencies()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => new ShortestRouteService(null));
        }
    }
}