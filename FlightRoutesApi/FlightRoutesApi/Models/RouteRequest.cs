using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightRoutesApi.Models
{
    public class RouteRequest
    {
        public string Origin { get; set; }

        public string Destination { get; set; }
    }
}
