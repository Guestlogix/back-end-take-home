using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using CsvHelper;
using GeoCoordinatePortable;
using Process.Models;

namespace Process
{
    public interface IContext
    {
        ReadOnlyCollection<Airline> Airlines { get; }
        ReadOnlyCollection<Airport> Airports { get; }
        ReadOnlyCollection<Route> Routes { get; }
    }

    public class Context : IContext
    {
        public Context()
        {
            InitAirlines();
            InitAirports();
            InitRoutes();
        }

        public ReadOnlyCollection<Airline> Airlines { get; private set; }
        public ReadOnlyCollection<Airport> Airports { get; private set; }
        public ReadOnlyCollection<Route> Routes { get; private set; } 

        private void InitAirlines()
        {
            using (var reader = new StreamReader("Resources\\airlines.csv"))
            using (var csv = new CsvReader(reader))
            {
                var records = new List<Airline>();
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var record = new Airline
                    {
                        Name = csv.GetField("Name"),
                        DigitCode2 = csv.GetField("2 Digit Code"),
                        DigitCode3 = csv.GetField("3 Digit Code"),
                        Country = csv.GetField("Country")
                    };
                    records.Add(record);
                }

                Airlines = new ReadOnlyCollection<Airline>(records.ToList());
            }
        }

        private void InitAirports()
        {
            using (var reader = new StreamReader("Resources\\airports.csv"))
            using (var csv = new CsvReader(reader))
            {
                var records = new List<Airport>();
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var record = new Airport
                    {
                        Name = csv.GetField("Name"),
                        City = csv.GetField("City"),
                        Country = csv.GetField("Country"),
                        IATA3 = csv.GetField("IATA 3"),
                        Location = new GeoCoordinate(csv.GetField<double>("Latitute"), csv.GetField<double>("Longitude"))
                    };
                    records.Add(record);
                }

                Airports = new ReadOnlyCollection<Airport>(records.ToList());
            }
        }

        private void InitRoutes()
        {
            using (var reader = new StreamReader("Resources\\routes.csv"))
            using (var csv = new CsvReader(reader))
            {
                var records = new List<Route>();
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var record = new Route
                    {
                        AirlineId = csv.GetField("Airline Id"),
                        Destination = csv.GetField("Destination"),
                        Origin = csv.GetField("Origin")
                    };
                    records.Add(record);
                }

                Routes = new ReadOnlyCollection<Route>(records.ToList());
            }
        }
    }
}
