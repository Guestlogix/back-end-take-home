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
        public RoutesController()
        {
            var path = ConfigurationManager.AppSettings["pathRouteCSV"];
            var serverPath = System.Web.Hosting.HostingEnvironment.MapPath(path);
            _routeService = new RouteService(serverPath);
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
                var routes = _routeService.GetShortest(origin, destin);

                var result = Util.RoutesToString(routes);
                return result;
            }
            catch (ValidationException e)
            {
                return e.Message;
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }
    }
}
