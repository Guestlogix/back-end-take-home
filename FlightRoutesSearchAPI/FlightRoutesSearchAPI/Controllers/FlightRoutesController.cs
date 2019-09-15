using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlightRoutesSearchAPI.Models;
using System.IO;

namespace FlightRoutesSearchAPI.Controllers
{
    public class FlightRoutesController : ApiController
    {
        // As a user I can make a GET request to an endpoint with an origin and destination query parameter, and receive back the shortest route between the two, as an array of connecting flights.
        //      A shortest route is defined as the route with the fewest connections.
        //      If there are mulitple routes with the same number of connections, you may choose any of them.
        // As a user I am provided meaningful feedback should no route exist between the airports.
        // As a user I am provided meaningful feedback if an error occurred with my request.
        public string GetConnections(string origin, string destination, string full)
        {
            string responseMsg = "";
            bool found = false;

            bool useFullData = false;
            bool.TryParse(full, out useFullData);

            List<string> discovered = new List<string>();
            List<string> shortestPath = new List<string>();
            Queue<Route> queueRoutes = new Queue<Route>();

            List<Airport> airports = new List<Airport>();
            List<Route> routes = new List<Route>();
            if (useFullData)
            {
                routes = loadCSVData();
                airports = Airport.loadAirportCSVData();
            }
            else
            {
                routes = loadTestCSVData();
                airports = Airport.loadAirportTestCSVData();
            }

            List<Route> originList = routes.Where(r => r.Origin == origin).ToList<Route>();

            // Validation: Invalid Origin
            if (airports.Where(a => a.IATA3 == origin).Count() == 0)
            {
                return String.Format("{0} is not a valid origin. Please verify the Origin Airport Code and try again.", origin);
            }

            // Validation: Invalid Destination
            if (airports.Where(a => a.IATA3 == destination).Count() == 0)
            {
                return String.Format("{0} is not a valid destination. Please verify the Destination Airport Code and try again.", destination);
            }

            // BFS Algorithm Implementation starts
            foreach (Route route in originList)
            {
                queueRoutes.Enqueue(route);
            }

            // queue may start with one or more connections ELP > CUU or ELP > HIA
            while (queueRoutes.Count > 0 && found == false)
            {
                Route currOrigin = queueRoutes.Dequeue();
                discovered.Add(currOrigin.Origin);
                if (shortestPath.Contains(currOrigin.Origin) == false)
                    shortestPath.Add(currOrigin.Origin);

                // get current element connections
                List<Route> currConnections = routes.Where(r => r.Origin == currOrigin.Destination).ToList<Route>();
                Console.WriteLine(currOrigin.Origin + " to " + currOrigin.Destination);
                if (currConnections.Count > 0)
                {
                    foreach (Route connection in currConnections)
                    {
                        // Skip already visited airports
                        if (discovered.Contains(connection.Destination))
                            continue;

                        // Was destination found? if not, enqueue connection to continue
                        if (connection.Destination == destination)
                        {
                            Console.WriteLine(connection.Origin + " to " + connection.Destination);

                            if (shortestPath.Contains(connection.Origin) == false)
                                shortestPath.Add(connection.Origin);
                            shortestPath.Add(destination);

                            found = true;
                        }
                        else
                            queueRoutes.Enqueue(connection);
                    }
                }
                else
                {
                    responseMsg = "No more connection flights.";
                }
            }

            if (found == false)
            {
                responseMsg = String.Format("There are no flights from {0} to {1}. Choose a different origin, or destination and try again.", origin, destination);
            }
            else
            {
                string shortestRouteMsg = "";
                foreach (string airport in shortestPath)
                {
                    shortestRouteMsg += airport + " > ";
                }
                shortestRouteMsg = shortestRouteMsg.Trim().Remove(shortestRouteMsg.Length - 2, 1);

                responseMsg = String.Format("Found shortest path from {0} to {1}. Route: {2}", origin, destination, shortestRouteMsg);
            }

            return responseMsg;
        }

        [NonAction]
        private List<Route> loadCSVData()
        {
            List<Route> routes = new List<Route>();

            string csvPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/data/full/routes.csv");
            string[] lines = File.ReadAllLines(csvPath);
            foreach (string line in lines)
            {
                string[] parameters = line.Split(',');
                if (parameters.Count() == 3)
                {
                    Route newRoute = new Route(parameters[0].Trim(), parameters[1].Trim(), parameters[2].Trim());
                    routes.Add(newRoute);
                }
            }
            return routes;
        }

        [NonAction]
        private List<Route> loadTestCSVData()
        {
            List<Route> routes = new List<Route>();

            string csvPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/data/test/routes.csv");
            string[] lines = File.ReadAllLines(csvPath);
            foreach (string line in lines)
            {
                string[] parameters = line.Split(',');
                if (parameters.Count() == 3)
                {
                    Route newRoute = new Route(parameters[0].Trim(), parameters[1].Trim(), parameters[2].Trim());
                    routes.Add(newRoute);
                }
            }
            return routes;
        }

    }
}
