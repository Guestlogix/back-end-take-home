using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightRouteApi.Library.Models;

namespace FlightRouteApi.Library
{
    public class LoadDataFromCsv
    {
        private string _pathBase;

        public LoadDataFromCsv(string dataFolderName = "test") {
            _pathBase = AppDomain.CurrentDomain.BaseDirectory.Substring(
            0,
            AppDomain.CurrentDomain.BaseDirectory.IndexOf("FlightRouteApi") + 14)
            + "\\FlightRouteApi.Library\\Data\\" + dataFolderName + "\\";
        }

        public List<Airline> LoadAirlines() {
            var retVal = new List<Airline>();
            var filePath = _pathBase + "airlines.csv";
            retVal = File.ReadLines(filePath)
                .Skip(1)
                .Select(line => line.Split(','))
                .Select(tokens => new Airline { Name = tokens[0], TwoDigitCode = tokens[1], ThreeDigitCode = tokens[2], Country = tokens[3] })
                .ToList();

            return retVal;
        }

        public List<Airport> LoadAirport()
        {
            var retVal = new List<Airport>();
            var filePath = _pathBase + "airports.csv";
            var lines = Csv.CsvReader.ReadFromText(File.ReadAllText(filePath));
            foreach (var line in lines) {
                if (line.Values.Count() == 6)
                {
                    retVal.Add(new Airport
                    {
                        Name = line.Values[0],
                        City = line.Values[1],
                        Country = line.Values[2],
                        Iata3 = line.Values[3],
                        Latitude = Convert.ToDouble(line.Values[4]),
                        Longitude = Convert.ToDouble(line.Values[5])
                    });
                }
            }

            return retVal;
        }

        public List<Route> LoadRoute(List<Airline> airlines, List<Airport> airports)
        {
            var retVal = new List<Route>();
            var filePath = _pathBase + "routes.csv";

            retVal = Csv.CsvReader.ReadFromText(File.ReadAllText(filePath))
                .Select(line => new Route
                {
                    Airline = airlines.Where(x => x.TwoDigitCode == line.Values[0]).FirstOrDefault(),
                    Origin = airports.Where(y => y.Iata3 == line.Values[1]).FirstOrDefault(),
                    Destination = airports.Where(z => z.Iata3 == line.Values[2]).FirstOrDefault()
                })
                .ToList();

            return retVal;
        }
    }
}
