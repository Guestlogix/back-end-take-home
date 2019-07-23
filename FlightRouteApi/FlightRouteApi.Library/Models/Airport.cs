using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightRouteApi.Library.Models
{
    public class Airport
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Iata3 { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}