using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Business.DTO;
using Business.Services;

namespace AirplaneAPI.Controllers
{
    public class RoutesController : ApiController
    {
        private RouteService _routeService;
        public RoutesController()
        {
            _routeService = new RouteService();
        }
        public List<RouteDTO> Get()
        {
            var routes = _routeService.GetAll();
            return routes;
        }
        [Route("api/routes/{origin}/{destin}")]
        public List<RouteDTO> Get(string origin, string destin)
        {
            var routes = _routeService.GetShortest(origin, destin);
            return routes;
        }
    }
}
