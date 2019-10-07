using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using RouteSearch.Domain.Entities;
using RouteSearch.Domain.Repositories;
using RouteSearch.Domain.Services.RouteFinder;
using RouteSearch.Domain.Test.MockData;
using Xunit;

namespace RouteSearch.Domain.Test.Services
{
    public class RouteFinderServiceTest
    {
        Mock<IAirportRepository> _airportRepositoryMock;
        Mock<IRouteRepository> _routeRepositoryMock;

        public RouteFinderServiceTest()
        {
            _airportRepositoryMock = new Mock<IAirportRepository>();
            _routeRepositoryMock = new Mock<IRouteRepository>();
        }

        [Fact]
        public void ShouldReturnNoRouteWhenItIsInexistent()
        {
            var origin = AirportMockData.Airports.First(x => x.Key == AirportMockData.ORD).Value;
            var destination = AirportMockData.Airports.First(x => x.Key == AirportMockData.JFK).Value;            
            _airportRepositoryMock.Setup(x => x.GetByIata(AirportMockData.ORD)).Returns(origin);
            _airportRepositoryMock.Setup(x => x.GetByIata(AirportMockData.JFK)).Returns(destination);
            _routeRepositoryMock.Setup(x => x.GetDestinationConnections(origin)).Returns(RouteMockData.Routes.Where(x => x.Origin == origin).ToList());
            var routeFinderService = new RouteFinderService(_airportRepositoryMock.Object, _routeRepositoryMock.Object);

            Action act = () => routeFinderService.Find(AirportMockData.ORD, AirportMockData.JFK);
            
            act.Should().Throw<ArgumentOutOfRangeException>("No Route");
        }

        [Fact]
        public void ShouldReturnInvalidDestinationWhenReceivesNotRegisteredDestination()
        {
            var destinationCode = "XPTO";
            var origin = AirportMockData.Airports.First(x => x.Key == AirportMockData.ORD).Value;
            _airportRepositoryMock.Setup(x => x.GetByIata(AirportMockData.ORD)).Returns(origin);
            _airportRepositoryMock.Setup(x => x.GetByIata(destinationCode)).Returns((Airport)null);
            _routeRepositoryMock.Setup(x => x.GetDestinationConnections(origin)).Returns(RouteMockData.Routes.Where(x => x.Origin == origin).ToList());
            var routeFinderService = new RouteFinderService(_airportRepositoryMock.Object, _routeRepositoryMock.Object);

            Action act = () => routeFinderService.Find(AirportMockData.ORD, AirportMockData.JFK);
            
            act.Should().Throw<KeyNotFoundException>("Invalid Destination");        
        }

        [Fact]
        public void ShouldReturnInvalidOriginWhenReceivesNotRegisteredOrigin()
        {
            var originCode = "XPTO";
            var origin = AirportMockData.Airports.First(x => x.Key == AirportMockData.ORD).Value;
            _airportRepositoryMock.Setup(x => x.GetByIata(originCode)).Returns((Airport)null);
            _routeRepositoryMock.Setup(x => x.GetDestinationConnections(origin)).Returns(RouteMockData.Routes.Where(x => x.Origin == origin).ToList());
            var routeFinderService = new RouteFinderService(_airportRepositoryMock.Object, _routeRepositoryMock.Object);

            Action act = () => routeFinderService.Find(AirportMockData.ORD, AirportMockData.JFK);
            
            act.Should().Throw<KeyNotFoundException>("Invalid Origin");        
        }        

        [Fact]
        public void ShouldReturnStraightRoute()
        {
            var origin = AirportMockData.Airports.First(x => x.Key == AirportMockData.YYZ).Value;
            var destination = AirportMockData.Airports.First(x => x.Key == AirportMockData.JFK).Value;            
            _airportRepositoryMock.Setup(x => x.GetByIata(AirportMockData.ORD)).Returns(origin);
            _airportRepositoryMock.Setup(x => x.GetByIata(AirportMockData.JFK)).Returns(destination);
            _routeRepositoryMock.Setup(x => x.GetDestinationConnections(origin)).Returns(RouteMockData.Routes.Where(x => x.Origin == origin).ToList());
            var routeFinderService = new RouteFinderService(_airportRepositoryMock.Object, _routeRepositoryMock.Object);

            var flightRoute = routeFinderService.Find(AirportMockData.ORD, AirportMockData.JFK);
            
            flightRoute.Origin.Should().Be(origin.Iata);
            flightRoute.Destination.Should().Be(destination.Iata);
            flightRoute.Connections.Should().HaveCount(1);
        }

        [Fact]
        public void ShouldReturnConnectionRoute()
        {
            var origin = AirportMockData.Airports.First(x => x.Key == AirportMockData.YYZ).Value;
            var destination = AirportMockData.Airports.First(x => x.Key == AirportMockData.LAX).Value;            
            var airportConnection = AirportMockData.Airports.First(x => x.Key == AirportMockData.JFK).Value;
            _airportRepositoryMock.Setup(x => x.GetByIata(AirportMockData.ORD)).Returns(origin);
            _airportRepositoryMock.Setup(x => x.GetByIata(AirportMockData.JFK)).Returns(destination);
            _routeRepositoryMock.Setup(x => x.GetDestinationConnections(origin)).Returns(RouteMockData.Routes.Where(x => x.Origin == origin).ToList());
            _routeRepositoryMock.Setup(x => x.GetDestinationConnections(airportConnection)).Returns(RouteMockData.Routes.Where(x => x.Origin == airportConnection).ToList());
            var routeFinderService = new RouteFinderService(_airportRepositoryMock.Object, _routeRepositoryMock.Object);

            var flightRoute = routeFinderService.Find(AirportMockData.ORD, AirportMockData.JFK);
            
            flightRoute.Origin.Should().Be(origin.Iata);
            flightRoute.Destination.Should().Be(destination.Iata);
            flightRoute.Connections.Should().HaveCount(2);
        }
    }
}