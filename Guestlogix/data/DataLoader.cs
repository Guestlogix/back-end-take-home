using Guestlogix.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            return File.ReadAllLines(AirlinesFile).Select(a =>
            {
                string[] split = a.Split(',');
                return new AirlineModel
                {
                    Name = split[0].Trim(),
                    TwoDigitCode = split[1].Trim(),
                    ThreeDigitCode = split[2].Trim(),
                    Country = split[3].Trim()
                };
            });
        }

        private static IEnumerable<AirportModel> GetAllAirports()
        {
            return File.ReadAllLines(AirportsFile).Select(a =>
            {
                string[] split = a.Split(',');
                return new AirportModel
                {
                    Name = split[0].Trim(),
                    City = split[1].Trim(),
                    Country = split[2].Trim(),
                    IATA3 = split[3].Trim(),
                    Latitude = double.Parse(split[4].Trim()),
                    Longitude = double.Parse(split[5].Trim())
                };
            });
        }

        private static IEnumerable<FlightModel> GetAllFlights()
        {
            return File.ReadAllLines(FlightsFile).Select(f =>
            {
                string[] split = f.Split(',');
                return new FlightModel
                {
                    AirlineId = split[0].Trim(),
                    Origin = split[1].Trim(),
                    Destination = split[2].Trim()
                };
            });
        }
    }
}