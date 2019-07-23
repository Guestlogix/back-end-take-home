using System.Collections.Generic;

namespace RouteCalculator.Entities.Models
{
    public class Airport
    {
        public string Code { get; set; }
        public List<Route> OutboundFlights { get; set; }
        public Route ItineraryTracker { get; set; }

        public Airport()
        {
            OutboundFlights = new List<Route>();
        }

        public Airport(Dtos.Airport dto) : this()
        {
            Code = dto.Code;
        }
    }
}