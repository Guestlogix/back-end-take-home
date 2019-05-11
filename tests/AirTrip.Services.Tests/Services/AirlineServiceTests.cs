using System;
using System.Collections.Generic;
using System.Text;
using AirTrip.Core;
using AirTrip.Services.DataProviders;
using AirTrip.Services.Services;
using AirTrip.Services.Tests.DataProviders;
using FluentAssertions;
using Xunit;

namespace AirTrip.Services.Tests.Services
{
    public class AirlineServiceTests
    {
        [Fact]
        public void ShouldReturnNoData()
        {
            // arrange
            var airlineDataProvider = new EmptyDataProvider();

            var service = new AirlineService(airlineDataProvider);

            // act
            var result = service.GetAllAirlines();

            // assert
            result.Should().BeEmpty();

        }

        private sealed class EmptyDataProvider : IDataProvider<Airline>
        {
            public IReadOnlyCollection<Airline> GetData()
            {
                return Array.Empty<Airline>();
            }
        }
    }

    
}
