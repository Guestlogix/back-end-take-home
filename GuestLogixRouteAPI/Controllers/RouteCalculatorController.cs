using GuestLogixRouteAPI.BusinessLayer;
using GuestLogixRouteAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace GuestLogixRouteAPI.Controllers
{
    public class RouteCalculatorController : ApiController
    {
        [HttpGet]
        //PATH /api/RouteCalculator/GetRoute?origin=YYZ&destination=YVR
        public IHttpActionResult GetRoute(string origin, string destination)
        {
            try
            {
                var routes = new Dictionary<int, List<string>>();
                using (var context=new ApplicationDBContext())
                {
                    List<AirportRoute> listPredefinedRoutes;
                    DbSet<AirportRoute> airportRoutesDbSet;
                    try
                    {
                        context.Database.ExecuteSqlCommand("TRUNCATE TABLE AirportRoutes");
                    }
                    catch (Exception) { }
                    var airportRoute = ReadDataFromFile();

                    context.routeAirports.AddRange(airportRoute.AsEnumerable<AirportRoute>());
                    context.SaveChanges();

                    listPredefinedRoutes = context.routeAirports.ToList<AirportRoute>();
                    airportRoutesDbSet = context.routeAirports;

                    if (!(listPredefinedRoutes.Exists(x => (x.originAirport == origin)) && listPredefinedRoutes.Exists(x => (x.destAirport == destination))))
                    {
                        return BadRequest(String.Format("Invalid origin or destination pair found - {0},{1}", origin, destination));
                    }
                    RouteDecision routeDecision = new RouteDecision();
                    routes = routeDecision.GetAirportRoutes(origin, destination, airportRoutesDbSet);
                }
                var shortRoute = routes.OrderBy(x => x.Value.Count).First().Value;

                if (!shortRoute.Exists(x => x == destination))
                    return Ok<String>("No route found between " + origin + " and " + destination);

                return Ok<List<String>>(shortRoute);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        private List<AirportRoute> ReadDataFromFile()
        {
            try
            {
                StreamReader streamReader = new StreamReader(Properties.Settings.Default.TestDataPath);
                List<AirportRoute> airportRoutes = new List<AirportRoute>();
                string headerLine = streamReader.ReadLine();
                int i = 0;
                while (!streamReader.EndOfStream)
                {
                    string cityLine = streamReader.ReadLine();
                    string airlineCode = (cityLine.Split(','))[0];
                    string originCity = (cityLine.Split(','))[1];
                    string destCity = (cityLine.Split(','))[2];

                    AirportRoute route = new AirportRoute();
                    route.routeId = i + 1;
                    route.airlineCode = airlineCode;
                    route.originAirport = originCity;
                    route.destAirport = destCity;

                    airportRoutes.Add(route);
                    i++;
                }
                return airportRoutes;
            }
            catch (Exception)
            {
                throw new EndOfStreamException();
            }
        }

    }
}
