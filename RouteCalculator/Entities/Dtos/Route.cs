using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouteCalculator.Entities.Dtos
{
    public class Route
    {
        public string AirlineCode { get; set; }
        public string OriginAirportCode { get; set; }
        public string DestinationAirportCode { get; set; }
    }
}