using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AirplaneAPI.Helpers;
using Business.DTO;
using Business.Exceptions;
using Business.Services;

namespace AirplaneAPI.Controllers
{
    public class RoutesController : ApiController
    {
        private RouteService _routeService;
        private AirportService _airportService;
        public RoutesController()
        {
            var path = ConfigurationManager.AppSettings["pathRouteCSV"];
            var serverPath = System.Web.Hosting.HostingEnvironment.MapPath(path);
            _routeService = new RouteService(serverPath);

            var pathAirport = ConfigurationManager.AppSettings["pathAirportCSV"];
            var serverPathAirport = System.Web.Hosting.HostingEnvironment.MapPath(pathAirport);
            _airportService = new AirportService(serverPathAirport);
        }

        public string Get()
        {
            try
            {
                var routes = _routeService.GetAll();

                var result = Util.RoutesToString(routes);
                return result;
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        public String Get(string origin, string destin)
        {
            try
            {
                _airportService.CheckAirports(origin, destin);
                var result = _routeService.GetShortest(origin, destin);

                return result;
            }
            catch (ValidationException e)
            {
                return e.Message;
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }
    }
}
