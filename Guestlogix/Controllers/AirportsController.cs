using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Guestlogix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportsController : ControllerBase{
        private readonly IDBRepository _repo;
        private readonly IMapper _mapper;
        public AirportsController(IDBRepository repo , IMapper mapper)  
        {  
            _repo= repo;  
            _mapper = mapper;
        }

        [HttpGet("all")]
        public IActionResult GetResult(){
            var airports = _repo.GetAirports();
            return Ok(Mapper.Map<IEnumerable<AirportModel>>(airports));
        }
    }
}