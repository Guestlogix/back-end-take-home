using System;
using System.Collections.Generic;
using Moq;
using RouteCalculator.Contracts;
using RouteCalculator.Entities;
using RouteCalculator.Services;
using Xunit;
using Route = RouteCalculator.Entities.Models.Route;

namespace RouteCalculatorTest
{
    public class RouteCalculatorServiceTests
    {
        private class Mocks
        {
            public readonly Mock<IRouteCalculatorRepository> RepositoryMock = new Mock<IRouteCalculatorRepository>();
            public readonly Mock<ICacheService> CacheMock = new Mock<ICacheService>();
        }

        private static RouterCalculatorService GetService(Mocks mock)
        {
            return new RouterCalculatorService(mock.CacheMock.Object, mock.RepositoryMock.Object);
        }

        [Fact]
        public void GetShortestCode_ReturnsErrorCode_OnInvalidOriginAirportCode()
        {
            // Arrange
            var mocks = new Mocks();
            mocks.CacheMock.Setup(mock => mock.GetEntity(It.IsAny<string>(),
                    It.IsAny<Func<IEnumerable<RouteCalculator.Entities.Models.Airport>>>()))
                .Returns(new List<RouteCalculator.Entities.Models.Airport>
                {
                    new RouteCalculator.Entities.Models.Airport
                    {
                        Code = "YYZ",
                        OutboundFlights = new List<Route>()
                    }
                });
            var service = GetService(mocks);

            // Act
            var result = service.GetShortestRoute("XXX", "YYZ", out var errorCode);

            // Assert
            Assert.Null(result);
            Assert.Equal(Error.InvalidOriginAirport, errorCode);
        }
    }
}