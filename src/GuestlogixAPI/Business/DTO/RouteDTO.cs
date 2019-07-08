using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Database.Model;

namespace Business.DTO
{
    public class RouteDTO
    {
        public string Code { get; set; }
        public string Origin { get; set; }
        public string Destin { get; set; }

        public static RouteDTO Convert(Route source)
        {
            var destin = new RouteDTO();

            destin.Code = source.Code;
            destin.Origin = source.OriginAirport;
            destin.Destin = source.DestinAirport;

            return destin;
        }

        public override string ToString()
        {
            return String.Format("{0} -> {1}", Origin, Destin);
        }
    }
}
