using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.DTO;
using Database.Model;
using Database.Repositories;

namespace Business.Services
{
    public class RouteService
    {
        private RouteRepository _routeRepository;
        public RouteService()
        {
            _routeRepository = new RouteRepository();
        }

        public RouteService(RouteRepository _repository)
        {
            _routeRepository = _repository;
        }
        public List<RouteDTO> GetAll()
        {
            var routes = _routeRepository.GetAll();
            var routesDTO = routes.Select(RouteDTO.Convert).ToList();
            return routesDTO;
        }

        public List<RouteDTO> GetShortest(string origin, string destin)
        {
            var routes = _routeRepository.GetAll();

            //logic here


            var routesDTO = routes.Select(RouteDTO.Convert).ToList();
            return routesDTO;
        }
    }
}
