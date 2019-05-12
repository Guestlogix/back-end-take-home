using System;
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
    public class AirlineDataProviderTests
    {
        [Fact]
        public async Task ShouldReturnAirlineData()
        {
            // arrange
            var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var location = Path.Combine(basePath, @"TestData\airlines.csv");

            var service = new AirlineDataProvider(location);

            // act
            var result = await service.GetDataAsync(CancellationToken.None);

            // assert
            result.Count.Should().Be(2);
            result.ElementAt(0).Code.Should().Be("UA");
            result.ElementAt(1).Code.Should().Be("WS");
        }
    }
}
