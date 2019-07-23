using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouteCalculator.Entities.Models
{
    public class Route
    {
        public string AirlineCode { get; set; }
        public Airport OriginAirport { get; set; }
        public Airport DestinationAirport { get; set; }
    }
}
