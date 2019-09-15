using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FlightRoutesSearchAPI.Models
{
    public class Airport
    {
        public Airport(string name, string city, string country, string iata3, decimal latitude, decimal longitude)
        {
            Name = name;
            City = city;
            Country = country;
            IATA3 = iata3;
            Latitude = latitude;
            Longitude = longitude;
        }

        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string IATA3 { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public static List<Airport> loadAirportCSVData()
        {
            List<Airport> airports = new List<Airport>();

            string csvPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/data/full/airports.csv");
            string[] lines = File.ReadAllLines(csvPath);
            foreach (string line in lines)
            {
                string[] parameters = line.Split(',');
                if (parameters.Count() == 6)
                {
                    decimal latitude = 0.0M;
                    decimal longitude = 0.0M;
                    decimal.TryParse(parameters[4].Trim(), out latitude);
                    decimal.TryParse(parameters[5].Trim(), out longitude);

                    Airport newAirport = new Airport(parameters[0].Trim(), parameters[1].Trim(),
                        parameters[2].Trim(), parameters[3].Trim(),
                        latitude, longitude);

                    airports.Add(newAirport);
                }
            }
            return airports;
        }

        public static List<Airport> loadAirportTestCSVData()
        {
            List<Airport> airports = new List<Airport>();

            string csvPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/data/test/airports.csv");
            string[] lines = File.ReadAllLines(csvPath);
            foreach (string line in lines)
            {
                string[] parameters = line.Split(',');
                if (parameters.Count() == 6)
                {
                    decimal latitude = 0.0M;
                    decimal longitude = 0.0M;
                    decimal.TryParse(parameters[4].Trim(), out latitude);
                    decimal.TryParse(parameters[5].Trim(), out longitude);

                    Airport newAirport = new Airport(parameters[0].Trim(), parameters[1].Trim(),
                        parameters[2].Trim(), parameters[3].Trim(),
                        latitude, longitude);

                    airports.Add(newAirport);
                }
            }
            return airports;
        }
    }
}