using FlightRoutesApi.Repositories;
using FlightRoutesApi.Models;
using System;
using System.Collections.Generic;

namespace FlightRoutesApi.Services
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _flightRepository;

        public FlightService(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public IEnumerable<string> GetShortestPath(string origin,string destination)
        {
            try
            {
                var airportList = GetAirports();
                var routeList = GetRoutes();

                var vertices = GetAirportNodesList(airportList);

                var (edges, verticesToBeRemoved) = GetEdgesToAirportNodes(airportList, routeList);

                verticesToBeRemoved.ForEach(v => { vertices.Remove(v); });

                var graph = new Graph<string>(vertices, edges);

                var adjacentRoutes = GetAdjacentRoutes(graph, origin);

                var shortestPath = CalculateShortestPath(destination, origin, adjacentRoutes);

                return shortestPath;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
           
            
        }

        public List<string> GetAirportNodesList(List<Airport> airportList)
        {
            var vertices = new List<string>();

            airportList.ForEach(a =>
            {
                vertices.Add(a.IATA3);
            });

            return vertices;
        }

        public Tuple<List<Tuple<string, string>>,List<string>> GetEdgesToAirportNodes(List<Airport> airportList, List<Route>routeList)
        {
            var edges = new List<Tuple<string, string>>();
            var verticesToBeRemoved = new List<string>();

            airportList.ForEach(a =>
            {
                var requestedOriginRoutes = routeList.FindAll(r => r.Origin == a.IATA3);
                if (requestedOriginRoutes.Count == 0)
                {
                    verticesToBeRemoved.Add(a.IATA3);

                }
                else
                {
                    requestedOriginRoutes.ForEach(r =>
                    {
                        edges.Add(Tuple.Create(a.IATA3, r.Destination));
                    });
                }


            });

            return new Tuple<List<Tuple<string, string>>, List<string>>(edges,verticesToBeRemoved);
        }

        public Dictionary<string, string> GetAdjacentRoutes(Graph<string> graph, string origin)
        {
            var previous = new Dictionary<string, string>();

            var queue = new Queue<string>();
            queue.Enqueue(origin);

            while (queue.Count > 0)
            {
                var vertex = queue.Dequeue();
                foreach (var neighbor in graph.AdjacencyList[vertex])
                {
                    if (previous.ContainsKey(neighbor))
                        continue;

                    previous[neighbor] = vertex;
                    queue.Enqueue(neighbor);
                }
            }

            return previous;
        }

        private IEnumerable<string> CalculateShortestPath(string vertex,string origin,Dictionary<string, string> previous)
        {
            var path = new List<string> { };

            var current = vertex;
            while (!current.Equals(origin))
            {
                path.Add(current);
                current = previous[current];
            }

            ;

            path.Add(origin);
            path.Reverse();

            return path;
        }

        private List<Airport> GetAirports()
        {
            return _flightRepository.GetAirports();
        }

        private List<Route> GetRoutes()
        {
            return _flightRepository.GetAirlineRoutes();
        }

        
    }
}
