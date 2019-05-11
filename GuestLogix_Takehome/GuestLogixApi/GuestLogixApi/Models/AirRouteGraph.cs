namespace GuestLogixApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AirRouteGraph
    {
        //Airline information not actually used by the algorithm.
        //private static List<Airline> airlines = new List<Airline>();
        private static List<Airport> airports = new List<Airport>();
        private static List<Route> routes = new List<Route>();

        public AirRouteGraph()
        {
            ModelConstructor.BuildModels(out routes, out airports);
        }

        public List<Route> ShortestPath(string origin, string destination)
        {
            if (origin == destination)
            {
                //TODO: better exception
                throw new Exception("Please enter a destination that is different from your origin");
            }
            Airport start = airports.FirstOrDefault(x => x.IATA3 == origin);
            //TODO: Allow user to input city or airport name. Currently only accepts airport code.
            if (start == null)
            {
                //TODO: better exception
                throw new Exception("Origin not found");
            }
            if (!airports.Any(x => x.IATA3 == destination))
            {
                //TODO: better exception
                throw new Exception("Destination not found");
            }

            //Initialize collections for tracking progress through BFS
            Queue<Airport> airportsToSee = new Queue<Airport>();
            List<Airport> airportsVisited = new List<Airport>();

            //Set up with the first airport
            airportsVisited.Add(start);
            foreach (Route r in start.DepartingFlights)
            {
                airportsToSee.Enqueue(r.Destination);
                r.Destination.IncomingFlight = r;
            }

            //Continue searching until we run out of airports to visit or we find a route.
            while (airportsToSee.Any())
            {
                Airport current = airportsToSee.Dequeue();
                if (current.IATA3 == destination)
                {
                    //we're done!
                    List<Route> itinerary = new List<Route>();
                    Airport review = current;
                    while (review != start)
                    {
                        itinerary.Add(review.IncomingFlight);
                        review = review.IncomingFlight.Origin;
                    }
                    //We added the flights going backwards...
                    itinerary.Reverse();
                    return itinerary;
                }
                else
                {
                    airportsVisited.Add(current);
                    foreach (Route r in current.DepartingFlights)
                    {
                        if (!airportsVisited.Contains(r.Destination) && !airportsToSee.Contains(r.Destination))
                        {
                            //haven't visited here yet, nor has a shorter route to it been found
                            airportsToSee.Enqueue(r.Destination);
                            r.Destination.IncomingFlight = r;
                        }
                    }
                }
            }
            //If we exit the loop without returning, we've exhausted the search without finding a route
            throw new Exception("No route found");
        }
    }
}