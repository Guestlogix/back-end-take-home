using System;
using System.Collections.Generic;
using System.Linq;

namespace Database.Model
{
    public class Airport
    {
        public string Name { get; set; }
        public string Iata { get; set; }

        public Airport()
        {
            
        }

        public Airport(string IATA)
        {
            Iata = IATA;
        }

        public static List<Airport> ConvertToList(List<string> source, char splitChar)
        {
            source.RemoveAt(0);
            var destin = source.Select(a => Convert(a, splitChar)).ToList();

            return destin;

        }

        public static Airport Convert(string source, char splitChar)
        {
            var destin = new Airport();

            var split = source.Split(',');

            destin.Name = split[0];
            destin.Iata = split[3];

            return destin;
        }
    }
}