using AirplaneAPI.Controllers;
using Business.Services;
using Database.Model;
using Database.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class RouteControllerTest
    {
        [TestMethod]
        public void When_HasInvalidOriginAirport_ReturnsError()
        {
            // Arrange
            Mock<IRouteRepository> routeRepository = new Mock<IRouteRepository>();
            routeRepository.Setup(a => a.GetAll()).Returns(MockRouteData());
            RouteService routeService = new RouteService(routeRepository.Object);

            Mock<IAirportRepository> airportRepository = new Mock<IAirportRepository>();
            airportRepository.Setup(a => a.GetAll()).Returns(MockAirportData());
            AirportService airportService = new AirportService(airportRepository.Object);

            RouteController controller = new RouteController(routeService, airportService);
            string origin = "XXX";
            string destin = "ORD";

            // Act
            string result = controller.Get(origin, destin);

            // Assert
            Assert.AreEqual("Invalid Origin", result);
        }

        [TestMethod]
        public void When_HasInvalidDestinationAirport_ReturnsError()
        {
            // Arrange
            Mock<IRouteRepository> routeRepository = new Mock<IRouteRepository>();
            routeRepository.Setup(a => a.GetAll()).Returns(MockRouteData());
            RouteService routeService = new RouteService(routeRepository.Object);

            Mock<IAirportRepository> airportRepository = new Mock<IAirportRepository>();
            airportRepository.Setup(a => a.GetAll()).Returns(MockAirportData());
            AirportService airportService = new AirportService(airportRepository.Object);

            RouteController controller = new RouteController(routeService, airportService);
            string origin = "ORD";
            string destin = "XXX";

            // Act
            string result = controller.Get(origin, destin);

            // Assert
            Assert.AreEqual("Invalid Destination", result);
        }

        [TestMethod]
        public void When_ValidAirportAndNoRoute_ReturnsError()
        {
            // Arrange
            Mock<IRouteRepository> routeRepository = new Mock<IRouteRepository>();
            routeRepository.Setup(a => a.GetAll()).Returns(MockRouteData());
            RouteService routeService = new RouteService(routeRepository.Object);

            Mock<IAirportRepository> airportRepository = new Mock<IAirportRepository>();
            airportRepository.Setup(a => a.GetAll()).Returns(MockAirportData());
            AirportService airportService = new AirportService(airportRepository.Object);

            RouteController controller = new RouteController(routeService, airportService);
            string origin = "YYZ";
            string destin = "ORD";

            // Act
            string result = controller.Get(origin, destin);

            // Assert
            Assert.AreEqual("No Route", result);
        }

        [TestMethod]
        public void When_ValidRoute_ReturnsPath()
        {
            // Arrange
            Mock<IRouteRepository> routeRepository = new Mock<IRouteRepository>();
            routeRepository.Setup(a => a.GetAll()).Returns(MockRouteData());
            RouteService routeService = new RouteService(routeRepository.Object);

            Mock<IAirportRepository> airportRepository = new Mock<IAirportRepository>();
            airportRepository.Setup(a => a.GetAll()).Returns(MockAirportData());
            AirportService airportService = new AirportService(airportRepository.Object);

            RouteController controller = new RouteController(routeService, airportService);
            string origin = "YYZ";
            string destin = "YVR";

            // Act
            string result = controller.Get(origin, destin);

            // Assert
            Assert.AreEqual("YYZ -> JFK -> LAX -> YVR", result);
        }

        #region Mock Data

        private List<Route> MockRouteData()
        {
            List<string> lines = new List<string>
            {
                "Airline Id,Origin,Destination",
                "AC,YYZ,JFK", "AC,JFK,YYZ", "AC,LAX,YVR",
                "AC,YVR,LAX", "UA,LAX,JFK", "UA,JFK,LAX"
            };

            List<Route> routes = Route.ConvertList(lines, ',');

            return routes;
        }

        private List<Airport> MockAirportData()
        {
            List<Airport> airports = new List<Airport>
            {
                new Airport("JFK"),
                new Airport("YYZ"),
                new Airport("LAX"),
                new Airport("YVR"),
                new Airport("ORD")
            };

            return airports;
        }
        #endregion

    }
}
