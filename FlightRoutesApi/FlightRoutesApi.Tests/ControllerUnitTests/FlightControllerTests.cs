using FlightRoutesApi.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using FlightRoutesApi.Models;
using FlightRoutesApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FlightRoutesApi.Tests.ControllerUnitTests
{
    [TestClass]
    public class FlightControllerTests
    {
        private readonly Mock<IFlightService> _flightServiceMock;
        public FlightControllerTests()
        {
            _flightServiceMock = new Mock<IFlightService>();
        }
        

        [TestMethod]
        public void WhenValidInput_Get_ReturnsShortestPath()
        {
            // Arrange
            var controller = new FlightController(_flightServiceMock.Object);
            var expectedResponse = new List<string>
            {
                "YYZ", "JFK", "LAX", "YVR"
            };
            _flightServiceMock.Setup(s => s.GetShortestPath(It.IsAny<string>(), It.IsAny<string>())).Returns(expectedResponse);

            // Act
            var response = controller.Get(new RouteRequest {Destination = "test", Origin = "Test"}) as OkObjectResult;

            // Assert

            Assert.AreEqual(response.Value,expectedResponse);

            
        }

        [TestMethod]
        public void WhenInvalidInput_Get_ReturnsNotFound()
        {
            // Arrange
            var controller = new FlightController(_flightServiceMock.Object);
            _flightServiceMock.Setup(s => s.GetShortestPath(It.IsAny<string>(), It.IsAny<string>())).Returns((IEnumerable<string>)null);

            // Act
            var response = controller.Get(new RouteRequest { Destination = "test", Origin = "Test" });

            // Assert

            Assert.IsInstanceOfType(response, typeof(NotFoundObjectResult));


        }

        [TestMethod]
        public void WhenValidInputNoRouteExists_Get_ReturnsNotRoute()
        {
            // Arrange
            var controller = new FlightController(_flightServiceMock.Object);
            _flightServiceMock.Setup(s => s.GetShortestPath(It.IsAny<string>(), It.IsAny<string>())).Returns((IEnumerable<string>)null);

            // Act
            var response = controller.Get(new RouteRequest { Destination = "YYZ", Origin = "ORD" }) as NotFoundObjectResult;

            // Assert

            Assert.AreEqual(response.Value, "No route found between YYZ and ORD");


        }
    }
}
