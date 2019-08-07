using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FakeItEasy;
using GeoCoordinatePortable;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Process;
using Process.Models;

namespace Tests
{
    [TestClass]
    public class RouteProcessorTests
    {
        private RouteProcessor _processor;

        [TestInitialize]
        public void Init()
        {
            var context = A.Fake<IContext>();
            A.CallTo(() => context.Airports).Returns(GetAirports());
            A.CallTo(() => context.Routes).Returns(GetRoutes());

            _processor = new RouteProcessor(context);
        }

        [TestMethod]
        public void ProcessorShouldReturnOneStepPath()
        {
            var result = _processor.FindShortestRoute("YYZ", "JFK");
            Assert.AreEqual("YYZ -> JFK (distance 157.36 km) (flights 1)", result);
        }

        [TestMethod]
        public void ProcessorShouldReturnThreeStepPath()
        {
            var result = _processor.FindShortestRoute("YYZ", "YVR");
            Assert.AreEqual("YYZ -> JFK -> LAX -> YVR (distance 471.91 km) (flights 3)", result);
        }

        [TestMethod]
        public void ProcessorShouldReturnNullIfNoRouteFound()
        {
            var result = _processor.FindShortestRoute("YYZ", "ORD");
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ProcessorShouldThrowExceptionIfOriginInvalid()
        {
            _processor.FindShortestRoute("XXX", "ORD");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ProcessorShouldThrowExceptionIfDestinationInvalid()
        {
            _processor.FindShortestRoute("ORD", "XXX");
        }

        private ReadOnlyCollection<Airport> GetAirports()
        {
            return new ReadOnlyCollection<Airport>(new List<Airport>
            {
                new Airport
                {
                    IATA3 = "YYZ",
                    Location = new GeoCoordinate(1, 1)
                },
                new Airport
                {
                    IATA3 = "JFK",
                    Location = new GeoCoordinate(2, 2)
                },
                new Airport
                {
                    IATA3 = "LAX",
                    Location = new GeoCoordinate(3, 3)
                },
                new Airport
                {
                    IATA3 = "YVR",
                    Location = new GeoCoordinate(4, 4)
                },
                new Airport
                {
                    IATA3 = "ORD",
                    Location = new GeoCoordinate(5, 5)
                }
            });
        }

        private ReadOnlyCollection<Route> GetRoutes()
        {
            return new ReadOnlyCollection<Route>(new List<Route>
            {
                new Route
                {
                   Origin = "YYZ",
                   Destination = "JFK"
                },
                new Route
                {
                    Origin = "JFK",
                    Destination = "YYZ"
                },
                new Route
                {
                    Origin = "LAX",
                    Destination = "YVR"
                },
                new Route
                {
                    Origin = "YVR",
                    Destination = "LAX"
                },
                new Route
                {
                    Origin = "LAX",
                    Destination = "JFK"
                },
                new Route
                {
                    Origin = "JFK",
                    Destination = "LAX"
                }
            });
        }
    }
}
