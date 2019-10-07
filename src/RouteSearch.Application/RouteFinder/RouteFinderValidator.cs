using System;

namespace RouteSearch.Application.RouteFinder
{
    public class RouteFinderValidator
    {
        public static void Validate(string origin, string destination)
        {
            if (string.IsNullOrWhiteSpace(origin))
                throw new ArgumentNullException("Origin cannot be null or empty");
            
            if (string.IsNullOrWhiteSpace(destination))
                throw new ArgumentNullException("Destination cannot be null or empty");
        }
    }
}