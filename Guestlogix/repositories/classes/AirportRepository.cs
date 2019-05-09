using Guestlogix.Models;
using Guestlogix.repositories.interfaces;
using System.Collections.Generic;

namespace Guestlogix.repositories.classes
{
    public class AirportRepository : IAirportRepository
    {
        public IEnumerable<AirportModel> GetAllAirports()
        {
            return null;
        }
    }
}