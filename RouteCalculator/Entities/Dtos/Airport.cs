using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouteCalculator.Entities.Dtos
{
    public class Airport
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        // ToDo: Other properties
    }
}