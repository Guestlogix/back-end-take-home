using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightRoutesApi.Models
{
    public class Airport
    {
        public string Name { get; set; }

        public string City { get; set; }

        public string ThreeDigitCode { get; set; }

        public string Country { get; set; }

        public string IATA3 { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public static Airport FromCsv(string csvLine)
        {
            var values = csvLine.Split(',');
            var airport = new Airport
            {
                Name = values[0], City = values[1],
                Country = values[2],
                IATA3 = values[3],
                Latitude = Convert.ToDouble(values[4]),
                Longitude = Convert.ToDouble(values[5])
            };
            return airport;
        }
    }

}
