using System.Collections.Generic;

namespace Domain.Modules.Repositories
{
    public interface IFlightRepository
    {
        /// <summary>
        /// Get the shortest route between origin and destination airport.
        /// </summary>
        /// <param name="airPorts"></param>
        /// <param name="origin"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        string GetShortestRoute(IList<AirPort> airPorts, string origin, string destination);
    }
}
