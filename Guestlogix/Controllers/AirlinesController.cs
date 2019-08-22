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
    public class AirlinesController : ControllerBase{
        private readonly IDBRepository _repo;
        private readonly IMapper _mapper;
        public AirlinesController(IDBRepository repo , IMapper mapper)  
        {  
            _repo= repo;  
            _mapper = mapper;
        }

        [HttpGet("all")]
        public IActionResult GetResult(){
            var airlines = _repo.GetAirlines();
            return Ok(Mapper.Map<IEnumerable<AirlineModel>>(airlines));
        }
    }
}