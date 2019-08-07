using System;
using System.Linq;

namespace Process
{
    public interface IRouteProcessor
    {
        string FindShortestRoute(string origin, string destination);
        void GenerateGraph();
    }

    public class RouteProcessor : IRouteProcessor
    {
        private readonly IContext _context;
        private readonly double[,] _graph;
        private bool _isInitiated = false;

        public RouteProcessor(IContext context)
        {
            _context = context;
            var count = _context.Airports.Count;
            _graph = new double[count, count];
        }

        public void GenerateGraph()
        {
            if (_isInitiated)
            {
                return;
            }

            var count = _context.Airports.Count;
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    var route = _context.Routes.FirstOrDefault(x =>
                        x.Origin.Equals(_context.Airports[i].IATA3, StringComparison.OrdinalIgnoreCase)
                        && x.Destination.Equals(_context.Airports[j].IATA3, StringComparison.OrdinalIgnoreCase));
                    
                    _graph[i, j] = _graph[j, i] = route != null
                        ? Math.Round(_context.Airports[i].Location.GetDistanceTo(_context.Airports[j].Location) / 1000, 2)
                        : 0;
                }
            }
        }

        public string FindShortestRoute(string origin, string destination)
        {
            if (!_isInitiated)
            {
                GenerateGraph();
            }

            var originAirport = _context.Airports.FirstOrDefault(x => x.IATA3.Equals(origin, StringComparison.OrdinalIgnoreCase)) ??
                                throw new InvalidOperationException("Invalid Origin");
            var originIndex = _context.Airports.IndexOf(originAirport);
            var destinationAirport = _context.Airports.FirstOrDefault(x => x.IATA3.Equals(destination, StringComparison.OrdinalIgnoreCase)) ??
                                throw new InvalidOperationException("Invalid Destination");
            var destinationIndex = _context.Airports.IndexOf(destinationAirport);

            var paths = _graph.ApplyAlgorithm(originIndex, destinationIndex);

            if (paths == null)
            {
                return null;
            }
        
            double pathLength = 0;
            for (int i = 0; i < paths.Count - 1; i++)
            {
                pathLength += _graph[paths[i], paths[i + 1]];
            }

            var formattedPath = string.Join(" -> ", paths.Select(x => _context.Airports[x].IATA3));
            return $"{formattedPath} (distance {pathLength} km) (flights {paths.Count - 1})";
        }
    }
}
