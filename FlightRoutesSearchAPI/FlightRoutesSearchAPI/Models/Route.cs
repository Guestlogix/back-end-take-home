using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightRoutesSearchAPI.Models
{
    public class Route
    {
        public Route(string airlineID, string origin, string destination)
        {
            AirlineID = airlineID;
            Origin = origin;
            Destination = destination;
        }
        public string AirlineID { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
    }
}