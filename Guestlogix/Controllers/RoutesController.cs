using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Guestlogix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesController : ControllerBase{
        private readonly IDBRepository _repo;
        private readonly IMapper _mapper;
        private RouteService _service;
        public RoutesController(IDBRepository repo , IMapper mapper)  
        {  
            _repo= repo;  
            _mapper = mapper;
            _service = new RouteService(_repo);
        }

        [HttpGet("all")]
        public IActionResult GetResult(){
            var routes = _repo.GetRoutes();
            return Ok(Mapper.Map<IEnumerable<RouteModel>>(routes));
        }

        [HttpGet("shortest")]
        public IActionResult GetResult(string origin, string destination){
            if(!_repo.HasAirport(origin))
                return BadRequest("Bad Request - Invalid Origin: " + origin + ". Please check that origin is spelled correctly.");

            if(!_repo.HasAirport(destination))
                return BadRequest("Bad Request - Invalid Destination: " + destination + ". Please check that destination is spelled correctly.");

            var shortestRoute = _service.GetShortestRoute(origin, destination);
            return Ok(shortestRoute);
        }
    }
}