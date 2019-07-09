using System.Collections.Generic;
using Domain.Modules;
using Domain.Modules.Repositories;
using Routes.Infra.Data.Data;
using Routes.Infra.Data.Data.Enum;
using Routes.Infra.Data.Repositories;
using Xunit;

namespace Routes.Test.Repositories
{
    public class FlightRepositoryTest
    {
        private IFlightRepository FlightRepository { get; set; }
        private IList<AirPort> AirPorts { get; set; }
        public FlightRepositoryTest()
        {
            FlightRepository = new FlightRepository();

            // Get All AirPorts
            AirPorts = LoadDataToCsv.AirPortsRoutes(ModeEnumerator.Test);
        }

        [Theory(DisplayName = "Invalid Origin")]
        [Trait("Category", "Flight Repository")]
        [InlineData("XXX", "ORD")]
        [InlineData("ZZZ", "YYZ")]
        [InlineData("", "ORD")]
        [InlineData("0515", "YYZ")]
        [InlineData("YYYX", "GRU")]
        public void FlightRepository_GetShortestRoute_InvalidOrigin(string origin, string destination)
        {
            //Act
            var result = FlightRepository.GetShortestRoute(AirPorts, origin, destination);

            //Assert
            Assert.Equal("Invalid Origin", result);
        }

        [Theory(DisplayName = "Invalid Destination")]
        [Trait("Category", "Flight Repository")]
        [InlineData("ORD", "XXX")]
        [InlineData("YYZ", "")]
        [InlineData("ORD", "0515")]
        [InlineData("YYZ", "DDD")]
        public void FlightRepository_GetShortestRoute_InvalidDestination(string origin, string destination)
        {
            //Act
            var result = FlightRepository.GetShortestRoute(AirPorts, origin, destination);

            //Assert
            Assert.Equal("Invalid Destination", result);
        }

        [Theory(DisplayName = "No Route")]
        [Trait("Category", "Flight Repository")]
        [InlineData("YYZ", "ORD")]
        [InlineData("ORD", "YYZ")]
        public void FlightRepository_GetShortestRoute_NoRoute(string origin, string destination)
        {
            //Act
            var result = FlightRepository.GetShortestRoute(AirPorts, origin, destination);

            //Assert
            Assert.Equal("No Route", result);
        }

        [Theory(DisplayName = "Found Shortest Route")]
        [Trait("Category", "Flight Repository")]
        [InlineData("YYZ", "JFK", "YYZ -> JFK")]
        [InlineData("YYZ", "YVR", "YYZ -> JFK -> LAX -> YVR")]
        public void FlightRepository_GetShortestRoute_FoundShortestRoute(string origin, string destination, string expected)
        {
            //Act
            var result = FlightRepository.GetShortestRoute(AirPorts, origin, destination);

            //Assert
            Assert.Equal(expected, result);
        }
    }
}
