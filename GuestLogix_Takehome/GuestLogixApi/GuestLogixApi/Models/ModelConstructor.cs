using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GuestLogixApi.Models
{
    public class ModelConstructor
    {
        public const string airlineFilePath = @"Models\data\airlines.csv";
        public const string airportFilePath = @"Models\data\airports.csv";
        public const string routeFilePath = @"Models\data\routes.csv";

        public static void BuildModels(out List<Route> routes, out List<Airport> airports)
        {
            airports = BuildAirports();
            routes = BuildRoutes(airports);
        }

        private static List<Route> BuildRoutes(List<Airport> airports)
        {
            string[] routes = File.ReadAllLines(routeFilePath);
            List<Route> result = new List<Route>();

            for (int i = 1; i < routes.Length; i++)
            {
                var split = routes[i].Split(',');
                Airport origin = airports.FirstOrDefault(x => x.IATA3 == split[1]);
                Airport destination = airports.FirstOrDefault(x => x.IATA3 == split[2]);
                //skip routes that include airport codes that don't exist in the csv files.
                if (origin != null && destination != null)
                {
                    Route route = new Route
                    {
                        Origin = origin,
                        Destination = destination
                    };
                    result.Add(route);
                    origin.DepartingFlights.Add(route);
                }
            }
            return result;
        }

        private static List<Airport> BuildAirports()
        {
            string[] airports = File.ReadAllLines(airportFilePath);
            List<Airport> result = new List<Airport>();

            for (int i = 1; i < airports.Length; i++)
            {
                var split = airports[i].Split(',');
                Airport airport = new Airport
                {
                    Name = split[0],
                    City = split[1],
                    Country = split[2],
                    IATA3 = split[3],
                    IncomingFlight = null,
                    DepartingFlights = new List<Route>()
                };
                result.Add(airport);
            }
            return result;
        }

        private static List<Airline> BuildAirlines()
        {
            return null;

        }
    }
}
