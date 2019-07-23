using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RouteCalculator.Contracts;
using RouteCalculator.Controllers;
using RouteCalculator.Entities;
using RouteCalculator.Entities.Dtos;
using RouteCalculator.Services;
using Xunit;
using Xunit.Sdk;

namespace RouteCalculatorTest
{
    public class ControllerTests
    {
        private class Mocks
        {
            public readonly Mock<IRouteCalculatorService> ServiceMock = new Mock<IRouteCalculatorService>();
        }

        private static RoutesCalculatorController GetController(Mocks mock)
        {
            return new RoutesCalculatorController(mock.ServiceMock.Object);
        }

        [Fact]
        public void Get_ReturnsSuccess()
        {
            // Arrange
            var mocks = new Mocks();
            var errorCode = Error.None;
            mocks.ServiceMock.Setup(mock => mock.GetShortestRoute(It.IsAny<string>(),
                It.IsAny<string>(), out errorCode))
                .Returns(new List<Route>{ new Route
                {
                    AirlineCode = "AC",
                    OriginAirportCode = "YYZ",
                    DestinationAirportCode = "JFK"
                }});
            var testSubject = GetController(mocks);

            // Act
            var result = testSubject.Get(It.IsAny<string>(), It.IsAny<string>());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<Route>>(((OkObjectResult) result).Value);
        }

        [Fact]
        public void Get_ReturnsExpectedErrorCode()
        {
            // Arrange
            var mocks = new Mocks();
            var errorCode = Error.InvalidOriginAirport;
            mocks.ServiceMock.Setup(mock => mock.GetShortestRoute(It.IsAny<string>(),
                    It.IsAny<string>(), out errorCode))
                .Returns((IEnumerable<Route>) null);
            var testSubject = GetController(mocks);

            // Act
            var result = testSubject.Get(It.IsAny<string>(), It.IsAny<string>());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(Error.InvalidOriginAirport.Code, ((BadRequestObjectResult) result).Value);
        }

    }
}
