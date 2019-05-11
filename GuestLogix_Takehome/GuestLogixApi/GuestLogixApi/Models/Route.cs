using System;
using System.Collections.Generic;
using System.Text;

namespace GuestLogixApi.Models
{
    public class Route
    {
        public string AirlineId { get; set; }
        public Airport Origin { get; set; }
        public Airport Destination { get; set; }
    }
}
