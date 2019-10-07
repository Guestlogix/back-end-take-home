using System.Collections.Generic;
using RouteSearch.Domain.Entities;

namespace RouteSearch.Domain.Test.MockData
{
    public class RouteMockData
    {                
        public static List<Route> Routes = new List<Route>()
        {
            new Route(AirportMockData.Airports[AirportMockData.YYZ], AirportMockData.Airports[AirportMockData.JFK], AirlineMockData.Airlines[AirlineMockData.AC]),
            new Route(AirportMockData.Airports[AirportMockData.JFK], AirportMockData.Airports[AirportMockData.YYZ], AirlineMockData.Airlines[AirlineMockData.AC]),
            new Route(AirportMockData.Airports[AirportMockData.LAX], AirportMockData.Airports[AirportMockData.YVR], AirlineMockData.Airlines[AirlineMockData.AC]),
            new Route(AirportMockData.Airports[AirportMockData.YVR], AirportMockData.Airports[AirportMockData.LAX], AirlineMockData.Airlines[AirlineMockData.AC]),
            new Route(AirportMockData.Airports[AirportMockData.LAX], AirportMockData.Airports[AirportMockData.JFK], AirlineMockData.Airlines[AirlineMockData.UA]),
            new Route(AirportMockData.Airports[AirportMockData.JFK], AirportMockData.Airports[AirportMockData.LAX], AirlineMockData.Airlines[AirlineMockData.UA])
        };
    }
}