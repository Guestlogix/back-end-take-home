using System.Collections.Generic;
using System.Text.RegularExpressions;
using RouteSearch.Domain.Entities;
using RouteSearch.Infrastructure.IO;

namespace RouteSearch.Infrastructure.Data
{
    public class DataLoading
    {
        private char SPLITTER = ',';
        private Regex csvSplit = new Regex("(?:^|,)(\"(?:[^\"])*\"|[^,]*)", RegexOptions.Compiled);        
        private const string AIRPORT = "airport";
        private const string AIRLINE = "airline";
        private const string ROUTE = "route";
        private readonly Dictionary<string, string> _filePaths;

        public DataLoading(Dictionary<string, string> filePaths)
        {
            _filePaths = filePaths;
        }

        public DataContext GetDataContext()
        {            
            Dictionary<string, Airport> airports = PopulateAirport();
            Dictionary<string, Airline> airlines = PopulateAirline();
            List<Route> routes = PopulateRoutes(airports, airlines);

            return new DataContext(airports, airlines, routes);
        }

        private Dictionary<string, Airport> PopulateAirport()
        {
            Dictionary<string, Airport> airports = GetAirports(_filePaths[AIRPORT]);            

            return airports;
        }

        private Dictionary<string, Airline> PopulateAirline()
        {
            Dictionary<string, Airline> airlines = GetAirlines(_filePaths[AIRLINE]);            

            return airlines;
        }

        private List<Route> PopulateRoutes(Dictionary<string, Airport> airports, Dictionary<string, Airline> airlines)
        {
            return GetRoutes(_filePaths[ROUTE], airports, airlines);            
        }       

        public Dictionary<string, Airport> GetAirports(string filePath)
        {
            IEnumerable<string> lines = FileReader.Read(filePath);

            var airports = new Dictionary<string, Airport>();

            foreach(string line in lines)
            {
                var columns = csvSplit.Split(line);
                var latitude = double.Parse(columns[9]);
                var longitude = double.Parse(columns[11]);
                var airport = new Airport(columns[7], columns[1], columns[3], columns[5], latitude, longitude);
                airports.Add(airport.Iata, airport);
            }

            return airports;
        }

        public Dictionary<string, Airline> GetAirlines(string filePath)
        {
            IEnumerable<string> lines = FileReader.Read(filePath);

            var airlines = new Dictionary<string, Airline>();

            foreach (var line in lines)
            {
                var columns = line.Split(SPLITTER);
                var airline = new Airline(columns[1], columns[0], columns[3], columns[2]);
                airlines.Add(airline.TwoDigitCode, airline);
            }

            return airlines;
        }

        public List<Route> GetRoutes(string filePath, Dictionary<string, Airport> airports, Dictionary<string, Airline> airlines)
        {
            IEnumerable<string> lines = FileReader.Read(filePath);

            var routes = new List<Route>();

            foreach (var line in lines)
            {
                var columns = line.Split(SPLITTER);
                var airline = airlines[columns[0]];
                var origin = airports[columns[1]];
                var destination = airports[columns[2]];
                var route = new Route(origin, destination, airline);
                routes.Add(route);
            }

            return routes;
        }
    }
}