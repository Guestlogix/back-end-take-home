using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlightRouteApi.Library;
using System.Web.Configuration;

namespace FlightRouteApi.Controllers
{
    public class RoutesController : ApiController
    {
        // GET api/Routes/origin=yyz&destination=jfk
        /// <summary>Get stuff</summary>
        /// <param name="origin"></param>
        /// <param name="destination"></param>
        /// <returns>string</returns>
        public string Get(string origin, string destination)
        {
            var toReturn = "";

            var loader = new LoadDataFromCsv(WebConfigurationManager.AppSettings["DataFolderName"]);
            var airlines = loader.LoadAirlines();
            var airports = loader.LoadAirport();
            var routes = loader.LoadRoute(airlines, airports);
            toReturn = FlightRouting.GetShortestRoute(airlines, airports, routes, origin, destination);

            return toReturn;
        }
    }
}
