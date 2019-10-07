using System.Collections.Generic;
using RouteSearch.Domain.Entities;

namespace RouteSearch.Domain.Test.MockData
{
    public class AirportMockData
    {
        public const string JFK = "JFK";
        public const string YYZ = "YYZ";
        public const string LAX = "LAX";
        public const string YVR = "YVR";

        public const string ORD = "ORD";

        public static Dictionary<string, Airport> Airports = new Dictionary<string, Airport>()
        {
            {"JFK", new Airport("JFK", "John F Kennedy International Airport", "New York", "United States", 40.63980103, -73.77890015)},
            {"YYZ", new Airport("YYZ", "Lester B. Pearson International Airport", "Toronto", "Canada", 43.67720032, -79.63059998)},
            {"LAX", new Airport("LAX", "Los Angeles International Airport", "Los Angeles", "United States", 33.94250107, -118.4079971)},
            {"YVR", new Airport("YVR", "Vancouver International Airport", "Vancouver", "Canada", 49.19390106, -123.1839981)},
            {"ORD", new Airport("ORD", "Chicago O'Hare International Airport", "Chicago", "United States", 41.97859955, -87.90480042)}
        };
    }
}