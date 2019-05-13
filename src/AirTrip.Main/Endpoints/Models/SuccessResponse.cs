using System.Collections.Generic;

namespace AirTrip.Main.Endpoints.Models
{
    public class SuccessResponse
    {
        public IEnumerable<string> ShortestRoute { get; set; }
    }
}