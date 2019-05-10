using Guestlogix.Models;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Web;

namespace Guestlogix.data
{
    public class DataLoader
    {
        private const string AirlinesFile = "airlines.csv";
        private const string AirportsFile = "airports.csv";
        private const string FlightsFile = "routes.csv";

        private static readonly Lazy<IEnumerable<AirlineModel>> lazyAirlines = new Lazy<IEnumerable<AirlineModel>>(() => GetAllAirlines());
        private static readonly Lazy<IEnumerable<AirportModel>> lazyAirports = new Lazy<IEnumerable<AirportModel>>(() => GetAllAirports());
        private static readonly Lazy<IEnumerable<FlightModel>> lazyFlights = new Lazy<IEnumerable<FlightModel>>(() => GetAllFlights());

        public static IEnumerable<AirlineModel> Airlines { get { return lazyAirlines.Value; } }
        public static IEnumerable<AirportModel> Airports { get { return lazyAirports.Value; } }
        public static IEnumerable<FlightModel> Flights { get { return lazyFlights.Value; } }

        private static IEnumerable<AirlineModel> GetAllAirlines()
        {
            TextFieldParser parser = new TextFieldParser(GetActualPath(AirlinesFile));
            List<AirlineModel> airlines = new List<AirlineModel>();
            parser.HasFieldsEnclosedInQuotes = true;
            parser.SetDelimiters(",");
            parser.ReadLine();
            while (!parser.EndOfData)
            {
                string[] fields = parser.ReadFields();
                airlines.Add(new AirlineModel
                {
                    Name = fields[0].Trim(),
                    TwoDigitCode = fields[1].Trim(),
                    ThreeDigitCode = fields[2].Trim(),
                    Country = fields[3].Trim()
                });
            }
            parser.Close();
            return airlines;
        }

        private static IEnumerable<AirportModel> GetAllAirports()
        {
            TextFieldParser parser = new TextFieldParser(GetActualPath(AirportsFile));
            List<AirportModel> airports = new List<AirportModel>();
            parser.HasFieldsEnclosedInQuotes = true;
            parser.SetDelimiters(",");
            parser.ReadLine();
            while (!parser.EndOfData)
            {
                string[] fields = parser.ReadFields();
                airports.Add(new AirportModel
                {
                    Name = fields[0].Trim(),
                    City = fields[1].Trim(),
                    Country = fields[2].Trim(),
                    IATA3 = fields[3].Trim(),
                    Latitude = double.Parse(fields[4].Trim()),
                    Longitude = double.Parse(fields[5].Trim())
                });
            }
            parser.Close();
            return airports;
        }

        private static IEnumerable<FlightModel> GetAllFlights()
        {
            TextFieldParser parser = new TextFieldParser(GetActualPath(FlightsFile));
            List<FlightModel> flights = new List<FlightModel>();
            parser.HasFieldsEnclosedInQuotes = true;
            parser.SetDelimiters(",");
            parser.ReadLine();
            while (!parser.EndOfData)
            {
                string[] fields = parser.ReadFields();
                flights.Add(new FlightModel
                {
                    AirlineId = fields[0].Trim(),
                    Origin = fields[1].Trim(),
                    Destination = fields[2].Trim()
                });
            }
            parser.Close();
            return flights;
        }

        private static string GetActualPath(string filename)
        {
            return HttpContext.Current.Server.MapPath("~/bin/data/") + filename;
        }
    }
}