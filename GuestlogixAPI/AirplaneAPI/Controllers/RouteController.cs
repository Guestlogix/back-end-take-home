using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Web.Http;
using AirplaneAPI.Helpers;
using Business.DTO;
using Business.Exceptions;
using Business.Services;

namespace AirplaneAPI.Controllers
{
    public class RouteController : ApiController
    {
        private RouteService _routeService;
        private AirportService _airportService;

        public RouteController()
        {
            var path = ConfigurationManager.AppSettings["pathRouteCSV"];
            var serverPath = System.Web.Hosting.HostingEnvironment.MapPath(path);
            _routeService = new RouteService(serverPath);

            var pathAirport = ConfigurationManager.AppSettings["pathAirportCSV"];
            var serverPathAirport = System.Web.Hosting.HostingEnvironment.MapPath(pathAirport);
            _airportService = new AirportService(serverPathAirport);
        }

        public RouteController(RouteService routeService, AirportService airportService)
        {
            _routeService = routeService;
            _airportService = airportService;
        }

        public String Get()
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
