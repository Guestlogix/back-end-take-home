using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Domain.Modules;
using Domain.Modules.ValueObject;
using Routes.Infra.Data.Components;
using Routes.Infra.Data.Data.Enum;
using Route = Domain.Modules.Route;

namespace Routes.Infra.Data.Data
{
    public static class LoadDataToCsv
    {
        private static readonly string _path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
        private const string AirPortFile = "airports.csv";
        private const string AirLineFile = "airlines.csv";
        private const string RouteFile = "routes.csv";
        private static ModeEnumerator _mode;

        public static IList<AirPort> AirPortsRoutes(ModeEnumerator mode = ModeEnumerator.Full)
        {
            _mode = mode;

            var airLines = LoadAirLines();
            var airPorts = LoadAirPorts();
            var routes = LoadRoutes();

            if (!airLines.Any() || !airPorts.Any() || !routes.Any()) return null;

            var routesList = new List<Route>();
            routes.ToList().ForEach(x =>
            {
                var airLine = airLines.Single(a => a.TwoDigitCode == x.AirLine);
                var airPortOrigin = airPorts.Single(a => a.Iata3 == x.Origin);
                var airPortDestination = airPorts.Single(a => a.Iata3 == x.Destination);
                var distanceCalculateByKm = CalculateDistanceBetweenLatitudeLongitude.CalculateByKilometers(airPortOrigin.Latitude,
                                                                                                                airPortOrigin.Longitude,
                                                                                                                airPortDestination.Latitude,
                                                                                                                airPortDestination.Longitude);

                var distance = Distance.DistanceFactory.NewDistanceByKilometers(distanceCalculateByKm);

                routesList.Add(Route.RouteFactory.NewRoute(airLine, airPortOrigin, airPortDestination, distance));
            });

            airPorts.ToList().ForEach(x =>
            {
                x.AddRangeDestinationRoute(routesList.Where(r => r.Origin.Iata3 == x.Iata3).ToList());
            });

            return airPorts;
        }

        private static IList<AirPort> LoadAirPorts()
        {
            if (string.IsNullOrWhiteSpace(_path)) return null;
            var pathFull = Path.Combine(_path.Substring(6), $@"Data\{(_mode == ModeEnumerator.Full ? "full" : "test")}\{AirPortFile}");
            using (var reader = new StreamReader(pathFull, Encoding.Default))
                return LoaderCsv.Load<AirPort>(reader);
        }

        private static IList<AirLine> LoadAirLines()
        {
            if (string.IsNullOrWhiteSpace(_path)) return null;
            var pathFull = Path.Combine(_path.Substring(6), $@"Data\{(_mode == ModeEnumerator.Full ? "full" : "test")}\{AirLineFile}");
            using (var reader = new StreamReader(pathFull, Encoding.Default))
                return LoaderCsv.Load<AirLine>(reader);
        }

        private static IList<Domain.Modules.Dto.Route> LoadRoutes()
        {
            if (string.IsNullOrWhiteSpace(_path)) return null;
            var pathFull = Path.Combine(_path.Substring(6), $@"Data\{(_mode == ModeEnumerator.Full ? "full" : "test")}\{RouteFile}");
            using (var reader = new StreamReader(pathFull, Encoding.Default))
                return LoaderCsv.Load<Domain.Modules.Dto.Route>(reader);
        }
    }
}
