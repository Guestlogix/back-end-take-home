using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightRouteApi.Library.Models;

namespace FlightRouteApi.Library
{
    public static class FlightRouting
    {
        public static string GetShortestRoute(
            List<Airline> airlines, 
            List<Airport> airports, 
            List<Route> directRoutes,
            string origin,
            string destination)
        {

            var retVal = "";

            var originAirport= airports.FirstOrDefault(x => x.Iata3.ToLower() == origin.ToLower());
            var destinationAirport = airports.FirstOrDefault(x => x.Iata3.ToLower() == destination.ToLower());

            if (originAirport == null)
            {
                return "Invalid Origin";
            }

            if (destinationAirport == null)
            { 
                return "Invalid Destination";
            }


            var directFlights = directRoutes.Where(
                x => x.Origin.Equals(originAirport) && 
                x.Destination.Equals(destinationAirport)).ToList();

            if (directFlights.Any())
            {
                var firstRoute = new List<Route>();
                firstRoute.Add(directFlights.First());
                retVal = GetRouteString(firstRoute);
            }

            retVal = GetBFSRoute(originAirport, destinationAirport, directRoutes);

            if (String.IsNullOrEmpty(retVal)) { retVal = "No Route"; } 

            return retVal;
        }


        private static string GetBFSRoute(Airport originAirport, Airport destinationAirport, List<Route> directRoutes)
        {
            var retVal = "";
            var routesToVisit = new List<Route>();
            var routesVisited = new List<Route>();
            routesToVisit.Add(new Route { Destination = originAirport });
        
            while (routesToVisit.Any())
            {
                var currentRoute = routesToVisit[0];
                routesToVisit.RemoveAt(0);
                routesVisited.Add(currentRoute);
                if (currentRoute.Destination == destinationAirport)
                {
                    var selectedRoutes = new List<Route>();
                    while (currentRoute.Destination != originAirport)
                    {
                        selectedRoutes.Insert(0, currentRoute);
                        currentRoute = routesVisited.First(z => z.Destination == currentRoute.Origin);
                    }

                    retVal = GetRouteString(selectedRoutes);

                    return retVal;
                }

                var childs = directRoutes.Where(x => x.Origin == currentRoute.Destination).ToList();
                foreach (var child in childs)
                {
                    if (routesVisited.All(y => y.Destination != child.Destination))
                    {
                        var childRoute = new Route { Destination = child.Destination, Origin = currentRoute.Destination };
                        routesToVisit.Add(childRoute);
                    }
                }

            }

            return retVal;
        }

        private static string GetRouteString(List<Route> routes)
        {
            var retVal = "";

            for(var i = 0; i < routes.Count; i++)
            {
                if (i == 0) {
                    retVal = routes[i].Origin.Iata3;
                }
                else
                {
                    retVal += " -> " + routes[i].Origin.Iata3;
                }

                if (i == routes.Count - 1) {
                    retVal += " -> " + routes[i].Destination.Iata3;
                }
            }

            return retVal;
        }

    }
}
