using System;
using System.Collections.Generic;
using System.Linq;

namespace Database.Model
{
    public class Route
    {
        public string Code { get; set; }
        public string OriginAirport { get; set; }
        public string DestinAirport { get; set; }

        public static List<Route> ConvertList(List<String> source, char splitChar)
        {
            source.RemoveAt(0);
            var destin = source.Select(a => Convert(a, splitChar)).ToList();

            return destin;
        }

        public static Route Convert(string source, char splitChar)
        {
            var destin  = new Route();

            var split = source.Split(',');

            destin.Code = split[0];
            destin.OriginAirport = split[1];
            destin.DestinAirport = split[2];

            return destin;
        }
    }
}