using System.Collections.Generic;
using System.Linq;
using Business.DTO;
using Business.Exceptions;
using Business.Rules;
using Database.Repositories;

namespace Business.Services
{
    public class AirportService
    {
        private AirportRepository _airportRepository;

        public AirportService(string path)
        {
            _airportRepository = new AirportRepository(path);
        }

        public void CheckAirports(string origin, string destin)
        {
            var airports = _airportRepository.GetAll();
            var codes = airports.Select(a => a.Iata);
            if (!codes.Contains(origin))
            {
                throw new ValidationException("Invalid Origin");
            }
            if (!codes.Contains(origin))
            {
                throw new ValidationException("Invalid Destination");
            }
        }
    }
}
