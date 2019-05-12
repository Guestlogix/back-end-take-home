using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AirTrip.Core;
using AirTrip.Services.DataProviders;
using AirTrip.Services.Services;
using FluentAssertions;
using Xunit;

namespace AirTrip.Services.Tests.Services
{
    public sealed class AirlineServiceTests
    {
        [Fact]
        public async Task ShouldReturnNoData_WhenProviderReturnsNoData()
        {
            // arrange
            var service = new AirlineService(new EmptyDataProvider());

            // act
            var result = await service.GetAllAirlinesAsync(CancellationToken.None);

            // assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task ShouldReturnData_WhenProviderReturnsData()
        {
            // arrange
            var service = new AirlineService(new MockDataProvider());

            // act
            var result = await service.GetAllAirlinesAsync(CancellationToken.None);

            // assert
            result.Should().NotBeEmpty();
            result.Count.Should().Be(2);
            result.ElementAt(0).TwoDigitCode.Should().Be("AC");
            result.ElementAt(1).TwoDigitCode.Should().Be("PD");
        }

        private sealed class MockDataProvider : IDataProvider<Airline>
        {
            public Task<IReadOnlyCollection<Airline>> GetDataAsync(CancellationToken token)
            {
                var airlines = new[]
                {
                    new Airline("Air Canada", "AC", "ACC", "Canada"),
                    new Airline("Porter", "PD", "PDD", "Canada"),
                };

                return Task.FromResult( (IReadOnlyCollection<Airline>) airlines);
            }
        }

        private sealed class EmptyDataProvider : IDataProvider<Airline>
        {
            public Task<IReadOnlyCollection<Airline>> GetDataAsync(CancellationToken token)
            {
                return Task.FromResult((IReadOnlyCollection<Airline>) null);
            }
        }
    }
}