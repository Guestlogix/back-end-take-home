using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightRouteApi.Library.Models
{
    public class Airline
    {
        public string Name { get; set; }
        public string TwoDigitCode { get; set; }
        public string ThreeDigitCode { get; set; }
        public string Country { get; set; }
    }
}