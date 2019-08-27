using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightRoutesApi.Models
{
    public class Route
    {
        public string AirlineId { get; set; }

        public string Origin { get; set; }

        public string Destination { get; set; }

        public static Route FromCsv(string csvLine)
        {
            var values = csvLine.Split(',');
            var route = new Route {AirlineId = values[0], Origin = values[1], Destination = values[2]};
            return route;
        }
    }
}
