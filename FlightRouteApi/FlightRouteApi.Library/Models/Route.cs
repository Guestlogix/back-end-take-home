using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlightRouteApi.Library.Models;

namespace FlightRouteApi.Library.Models
{
    public class Route
    {
        public Airline Airline { get; set; }
        public Airport Origin { get; set; }
        public Airport Destination { get; set; }
    }
}