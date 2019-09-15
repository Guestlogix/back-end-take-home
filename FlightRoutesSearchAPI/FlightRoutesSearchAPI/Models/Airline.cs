using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightRoutesSearchAPI.Models
{
    public class Airline
    {
        public string Name { get; set; }
        public string Code2Digits { get; set; }
        public string Code3Digits { get; set; }
        public string Country { get; set; }
    }
}