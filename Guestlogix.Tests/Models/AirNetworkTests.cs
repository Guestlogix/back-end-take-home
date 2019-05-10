using Guestlogix.exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Guestlogix.Models.Tests
{
    [TestClass]
    public class AirNetworkTests
    {
        AirNetwork airNetwork;

        [TestInitialize]
        public void TestInitialize()
        {
            List<AirportModel> airports = new List<AirportModel>();
            airports.Add(new AirportModel { IATA3 = "A1" });
            airports.Add(new AirportModel { IATA3 = "A2" });
            airports.Add(new AirportModel { IATA3 = "A3" });
            airports.Add(new AirportModel { IATA3 = "A4" });
            List<FlightModel> flights = new List<FlightModel>();
            flights.Add(new FlightModel { Origin = "A1", Destination = "A2" });
            flights.Add(new FlightModel { Origin = "A1", Destination = "A3" });
            flights.Add(new FlightModel { Origin = "A2", Destination = "A3" });
            flights.Add(new FlightModel { Origin = "A2", Destination = "A4" });
            airNetwork = new AirNetwork(null, airports, flights);
        }

        [TestMethod]
        [ExpectedException(typeof(OriginNotFoundException))]
        public void GetShortestRoute_OriginNotFound_KnownDestination()
        {
            airNetwork.GetShortestRoute("NotFound", "A1");
        }

        [TestMethod]
        [ExpectedException(typeof(OriginNotFoundException))]
        public void GetShortestRoute_OriginNotFound_DestinationNotFound()
        {
            airNetwork.GetShortestRoute("NotFound", "NotFound");
        }

        [TestMethod]
        [ExpectedException(typeof(DestinationNotFoundException))]
        public void GetShortestRoute_KnownOrigin_DestinationNotFound()
        {
            airNetwork.GetShortestRoute("A1", "NotFound");
        }

        [TestMethod]
        [ExpectedException(typeof(NoRouteFoundException))]
        public void GetShortestRoute_NoRouteFound()
        {
            airNetwork.GetShortestRoute("A3", "A4");
        }

        [TestMethod]
        public void GetShortestRoute_OneRoute()
        {
            RouteModel route = airNetwork.GetShortestRoute("A1", "A4");
            Assert.AreEqual(route.Flights.Count, 2);
            Assert.AreEqual(route.Flights[0].Origin, "A1");
            Assert.AreEqual(route.Flights[0].Destination, "A2");
            Assert.AreEqual(route.Flights[1].Origin, "A2");
            Assert.AreEqual(route.Flights[1].Destination, "A4");
        }

        [TestMethod]
        public void GetShortestRoute_TwoDifferentRoutes()
        {
            RouteModel route = airNetwork.GetShortestRoute("A1", "A3");
            Assert.AreEqual(route.Flights.Count, 1);
            Assert.AreNotEqual(route.Flights.Count, 2);
            Assert.AreEqual(route.Flights[0].Origin, "A1");
            Assert.AreEqual(route.Flights[0].Destination, "A3");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            airNetwork = null;
        }
    }
}