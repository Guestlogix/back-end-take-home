using GuestLogixApi.Exceptions;
using GuestLogixApi.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace UnitTests
{
    [TestFixture]
    public class AirRouteGraphTests
    {
        private AirRouteGraph graph; 
        
        [SetUp]
        public void AirRouteGraphTestsSetup()
        {
            Airport YUL = CreateAirport("YUL");
            Airport YYZ = CreateAirport("YYZ");
            Airport BDA = CreateAirport("BDA");
            Airport YYC = CreateAirport("YYC");
            Airport ZZZ = CreateAirport("ZZZ");
            List<Airport> airports = new List<Airport> { YUL, YYZ, BDA, YYC, ZZZ };

            Route YULYYZ = CreateRoute(YUL, YYZ);
            Route YYZBDA = CreateRoute(YYZ, BDA);
            Route BDAYYZ = CreateRoute(BDA, YYZ);
            Route YYZYUL = CreateRoute(YYZ, YUL);
            Route YYZYYC = CreateRoute(YYZ, YYC);
            Route YYCYYZ = CreateRoute(YYC, YYZ);
            Route YYCYUL = CreateRoute(YYC, YUL);
            Route YULYYC = CreateRoute(YUL, YYC);
            List<Route> routes = new List<Route> { YULYYZ, YYZBDA, BDAYYZ, YYZYUL, YYZYYC, YYCYYZ, YYCYUL, YULYYC };

            graph = new AirRouteGraph(routes, airports);

        }

        [Test]
        public void PassingTest()
        {
            //Arrange
            string origin = "YYZ";
            string destination = "YUL";

            //Act
            List<Route> itinerary = graph.ShortestPath(origin, destination);

            //Assert
            Assert.AreEqual(1, itinerary.Count);
        }

        [Test]
        public void MissingOrigin()
        {
            Assert.Throws(typeof(OriginNotFoundException), () => graph.ShortestPath("BadOrigin","BDA"));
        }

        [Test]
        public void MissingDestination()
        {
            Assert.Throws(typeof(DestinationNotFoundException), () => graph.ShortestPath("YYZ", "BadDestination"));
        }

        [Test]
        public void NoRoute()
        {
            Assert.Throws(typeof(RouteNotFoundException), () => graph.ShortestPath("ZZZ", "YYZ"));
        }

        [Test]
        public void OriginDestinationSame()
        {
            Assert.Throws(typeof(OriginDestinationAreSameException), () => graph.ShortestPath("YYZ", "YYZ"));
        }

        [Test]
        [TestCase("YYZ", "YYC", 1)]
        [TestCase("YUL", "BDA", 2)]
        public void Flights(string start, string goal, int expectedFlights)
        {
            //Arrange is all done by TestCase attributes

            //Act
            List<Route> itinerary = graph.ShortestPath(start, goal);

            //Assert
            Assert.AreEqual(expectedFlights, itinerary.Count);
        }

        private Route CreateRoute(Airport origin, Airport destination)
        {
            Route r = new Route
            {
                Origin = origin,
                Destination = destination
            };
            origin.DepartingFlights.Add(r);
            return r;
        }

        private Airport CreateAirport(string code)
        {
            return new Airport
            {
                IATA3 = code,
                DepartingFlights = new List<Route>(),
                IncomingFlight = null
            };
        }
    }
}
