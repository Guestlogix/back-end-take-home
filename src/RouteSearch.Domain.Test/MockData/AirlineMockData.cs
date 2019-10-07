using System.Collections.Generic;
using RouteSearch.Domain.Entities;

namespace RouteSearch.Domain.Test.MockData
{
    public class AirlineMockData
    {
        public const string AC = "AC";
        public const string UA = "UA";

        public static Dictionary<string, Airline> Airlines = new Dictionary<string, Airline>()
        {
            {"AC", new Airline("AC", "Air Canada", "Canada", "ACA")},
            {"UA", new Airline("UA", "United Airlines", "United States", "UAL")}
        };
    }
}