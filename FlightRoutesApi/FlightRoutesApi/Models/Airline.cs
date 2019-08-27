using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightRoutesApi.Models
{
    public class Airline
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string TwoDigitCode { get; set; }

        public string ThreeDigitCode { get; set; }

        public string Country { get; set; }
    }
}
