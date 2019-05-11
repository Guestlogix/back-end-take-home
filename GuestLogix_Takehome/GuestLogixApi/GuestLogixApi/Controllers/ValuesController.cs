using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GuestLogixApi.Models;

namespace GuestLogixApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        [HttpGet]
        public string Get()
        {
            return "Please add the origin and destination to the URL above with the format: api/values/{origin}/{destination}\nNote that origin and desination must be entered as the three digit airport codes.";
        }


        // GET api/values
        [HttpGet]
        [Route("{origin}/{destination}")]
        public IEnumerable<string> Get(string origin, string destination)
        {
            AirRouteGraph graph = new AirRouteGraph();
            List<string> result = new List<string>();
            try
            {
                List<Route> itinerary = graph.ShortestPath(origin, destination);
                foreach (Route r in itinerary)
                {
                    result.Add(r.Origin.IATA3 + " - " + r.Destination.IATA3);
                }
                return result;
            }
            catch (Exception e)
            {
                return new List<string>{ e.Message };
            }
        }
    }
}
