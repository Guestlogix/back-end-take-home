using System.Collections.Generic;

namespace AirTrip.Main.Endpoints.Models
{
    public class SuccessResponse
    {
        /// <summary>
        /// Shortest route between origin and destination as an array of airports
        /// </summary>
        public IEnumerable<string> ShortestRoute { get; set; }
    }
}