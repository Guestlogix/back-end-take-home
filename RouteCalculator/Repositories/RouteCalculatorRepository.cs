using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Options;
using RouteCalculator.Contracts;
using RouteCalculator.Entities.Dtos;
using RouteCalculator.Settings;

namespace RouteCalculator.Repositories
{
    public class RouteCalculatorRepository : IRouteCalculatorRepository
    {
        private readonly string _path;

        public RouteCalculatorRepository(IOptions<RepositorySettings> settings)
        {
            _path = settings.Value.RelativeFolderPath;
        }

        public IEnumerable<Route> GetRoutes()
        {
            var results = new List<Route>();
            var csvFile = Path.Combine(_path, "routes.csv");

            using (var fileStream = new StreamReader(csvFile))
            {
                var skipHeaderRecord = true;
                string csvRecord;

                while ((csvRecord = fileStream.ReadLine()) != null)
                {
                    if (skipHeaderRecord)
                    {
                        skipHeaderRecord = false;
                        continue;
                    }

                    var fields = csvRecord.Split(',');

                    // ToDo: This may be an error?
                    if (fields.Length != 3) continue;

                    results.Add(new Route
                    {
                        AirlineCode = fields[0],
                        OriginAirportCode = fields[1],
                        DestinationAirportCode = fields[2]
                    });
                }
            }

            return results;
        }

        public IEnumerable<Airport> GetAirports()
        {
            var results = new List<Airport>();
            var csvFile = Path.Combine(_path, "airports.csv");

            using (var fileStream = new StreamReader(csvFile))
            {
                var skipHeaderRecord = true;
                string csvRecord;

                while ((csvRecord = fileStream.ReadLine()) != null)
                {
                    if (skipHeaderRecord)
                    {
                        skipHeaderRecord = false;
                        continue;
                    }

                    var fields = csvRecord.Split(',');

                    // ToDo: This may be an error?
                    if (fields.Length != 6) continue;

                    results.Add(new Airport()
                    {
                        Name = fields[0],
                        City = fields[1],
                        Country = fields[2],
                        Code = fields[3]
                    });
                }
            }

            return results;
        }
    }
}
