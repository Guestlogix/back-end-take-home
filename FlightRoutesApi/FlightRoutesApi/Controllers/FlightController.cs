using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using FlightRoutesApi.Models;
using FlightRoutesApi.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FlightRoutesApi.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    public class FlightController : Controller
    {
        private readonly IFlightService _flightService;

        public FlightController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        public FlightController()
        {
            
        }


        // GET: api/<controller>
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public ActionResult Get([FromUri]RouteRequest request)
        {
            var shortestFlightPath = _flightService.GetShortestPath(request.Origin,request.Destination);

            if (shortestFlightPath == null)
            {
                return NotFound($"Exception occurred while finding path between {request.Origin} and {request.Destination}");
            }

            if (!shortestFlightPath.Any())
            {
                return NotFound($"No route found between {request.Origin} and {request.Destination}");
            }

            return Ok(shortestFlightPath);
        }

       

        // POST api/<controller>
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public void Post([Microsoft.AspNetCore.Mvc.FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [Microsoft.AspNetCore.Mvc.HttpPut("{id}")]
        public void Put(int id, [Microsoft.AspNetCore.Mvc.FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [Microsoft.AspNetCore.Mvc.HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
