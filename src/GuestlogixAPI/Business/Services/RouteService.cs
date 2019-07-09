using System.Collections.Generic;
using System.Linq;
using Business.DTO;
using Business.Rules;
using Database.Repositories;

namespace Business.Services
{
    public class RouteService
    {
        private IRouteRepository _routeRepository;

        public RouteService(string path)
        {
            _routeRepository = new RouteRepository(path);
        }

        public RouteService(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }

        public List<RouteDTO> GetAll()
        {
            var routes = _routeRepository.GetAll();
            var routesDTO = routes.Select(RouteDTO.Convert).ToList();
            return routesDTO;
        }

        public string GetShortest(string origin, string destin)
        {
            var routes = _routeRepository.GetAll();
            var routesDTO = routes.Select(RouteDTO.Convert).ToList();

            var result = ShortestRoute.GetRoute(origin, destin, routesDTO);

            return result;
        }
    }
}
