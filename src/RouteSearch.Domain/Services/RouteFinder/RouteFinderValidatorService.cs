using System.Collections.Generic;
using RouteSearch.Domain.Entities;

namespace RouteSearch.Domain.Services.RouteFinder
{
    public class RouteFinderValidatorService
    {
        public static void Validate(Airport origin, Airport destination)
        {
            if (origin is null)
                throw new KeyNotFoundException("Invalid Origin");

            if (destination is null)
                throw new KeyNotFoundException("Invalid Destination");
        }
    }
}