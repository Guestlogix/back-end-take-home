using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuestLogixRouteAPI.Models
{
    public class AirportRoute
    {
        [Key]
        public int routeId { get; set; }
        public string airlineCode { get; set; }
        public string originAirport { get; set; }
        public string destAirport { get; set; }
    }
}