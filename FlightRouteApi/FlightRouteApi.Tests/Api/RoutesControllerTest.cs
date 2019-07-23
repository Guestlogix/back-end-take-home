using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlightRouteApi;
using FlightRouteApi.Controllers;

namespace FlightRouteApi.Tests.Controllers
{
    [TestClass]
    public class RoutesControllerTest
    {
        [TestMethod]
        public void GetValidRoute1()
        {
            // Arrange
            RoutesController controller = new RoutesController();

            // Act
            string result = controller.Get("YYZ", "JFK");

            // Assert
            Assert.AreEqual("YYZ -> JFK", result);

        }

        [TestMethod]
        public void GetValidRoute2()
        {
            // Arrange
            RoutesController controller = new RoutesController();

            // Act
            string result = controller.Get("YYZ", "YVR");

            // Assert
            Assert.AreEqual("YYZ -> JFK -> LAX -> YVR", result);

        }

        [TestMethod]
        public void GetInvalidOrigin()
        {
            // Arrange
            RoutesController controller = new RoutesController();

            // Act
            string result = controller.Get("XXX", "ORD");

            // Assert
            Assert.AreEqual("Invalid Origin", result);
        }

        [TestMethod]
        public void GetInvalidDestination()
        {
            // Arrange
            RoutesController controller = new RoutesController();

            // Act
            string result = controller.Get("YYZ", "XXX");

            // Assert
            Assert.AreEqual("Invalid Destination", result);
        }

        [TestMethod]
        public void GetNoRoute()
        {
            // Arrange

            RoutesController controller = new RoutesController();

            // Act
            string result = controller.Get("YYZ", "ORD");

            // Assert
            Assert.AreEqual("No Route", result);
        }
    }
}
