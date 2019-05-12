using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using AirTrip.Core;
using AirTrip.Services.DataProviders;
using FluentAssertions;
using Xunit;

namespace AirTrip.Services.Tests.DataProviders
{
    public class AirportDataProviderTests
    {
        [Fact]
        public async Task ShouldReturnAirportData()
        {
            // arrange
            var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var location = Path.Combine(basePath, @"TestData\airports.csv");

            var service = new AirportDataProvider(location);

            // act
            var result = await service.GetDataAsync(CancellationToken.None);

            // assert
            result.Count.Should().Be(2);
            result.ElementAt(0).Should().Be(new Airport("YQT"));
            result.ElementAt(1).Should().Be(new Airport("YTZ"));
        }
    }
}