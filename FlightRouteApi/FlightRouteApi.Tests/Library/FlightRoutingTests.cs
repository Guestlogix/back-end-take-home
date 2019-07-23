using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlightRouteApi.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRouteApi.Library.Tests
{
    [TestClass()]
    public class FlightRoutingTests
    {
        [TestMethod()]
        public void GetShortestRouteValidRoute1()
        {

            var loader = new LoadDataFromCsv("test");
            var airlines = loader.LoadAirlines();
            var airports = loader.LoadAirport();
            var routes = loader.LoadRoute(airlines, airports);
            var result = FlightRouting.GetShortestRoute(airlines, airports, routes, "YYZ", "LAX");

            Assert.AreEqual("YYZ -> JFK -> LAX", result);
        }

        [TestMethod()]
        public void GetShortestRouteValidRoute2()
        {

            var loader = new LoadDataFromCsv("test");
            var airlines = loader.LoadAirlines();
            var airports = loader.LoadAirport();
            var routes = loader.LoadRoute(airlines, airports);
            var result = FlightRouting.GetShortestRoute(airlines, airports, routes, "YYZ", "JFK");

            Assert.AreEqual("YYZ -> JFK", result);
        }

        [TestMethod()]
        public void GetShortestRouteInvalidDestination()
        {

            var loader = new LoadDataFromCsv("test");
            var airlines = loader.LoadAirlines();
            var airports = loader.LoadAirport();
            var routes = loader.LoadRoute(airlines, airports);
            var result = FlightRouting.GetShortestRoute(airlines, airports, routes, "YYZ", "XXX");

            Assert.AreEqual("Invalid Destination", result);
        }

        [TestMethod()]
        public void GetShortestRouteInvalidOrigin()
        {

            var loader = new LoadDataFromCsv("test");
            var airlines = loader.LoadAirlines();
            var airports = loader.LoadAirport();
            var routes = loader.LoadRoute(airlines, airports);
            var result = FlightRouting.GetShortestRoute(airlines, airports, routes, "XXX", "JFK");

            Assert.AreEqual("Invalid Origin", result);
        }

        [TestMethod()]
        public void GetShortestRouteNoRoute()
        {

            var loader = new LoadDataFromCsv("test");
            var airlines = loader.LoadAirlines();
            var airports = loader.LoadAirport();
            var routes = loader.LoadRoute(airlines, airports);
            var result = FlightRouting.GetShortestRoute(airlines, airports, routes, "YYZ", "ORD");

            Assert.AreEqual("No Route", result);
        }
    }
}