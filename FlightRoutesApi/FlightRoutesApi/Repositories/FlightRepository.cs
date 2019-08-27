using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FlightRoutesApi.Models;
using FlightRoutesApi.Repositories;

namespace FlightRoutesApi.Repositories
{
    public class FlightRepository: IFlightRepository
    {
        public List<Airport> GetAirports()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), @"Repositories\Data\test\airports.csv");

            var airportList = File.ReadAllLines(path)
                .Skip(1)
                .Select(Airport.FromCsv)
                .ToList();

            return airportList;
        }

        public List<Route> GetAirlineRoutes()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), @"Repositories\Data\test\routes.csv");
            
            var routeList = File.ReadAllLines(path)
                .Skip(1)
                .Select(Route.FromCsv)
                .ToList();

            return routeList;
        }
    }
}
