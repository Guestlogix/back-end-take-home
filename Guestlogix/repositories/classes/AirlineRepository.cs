using Guestlogix.data;
using Guestlogix.Models;
using Guestlogix.repositories.interfaces;
using System.Collections.Generic;

namespace Guestlogix.repositories.classes
{
    public class AirlineRepository : IAirlineRepository
    {
        public IEnumerable<AirlineModel> GetAllAirlines()
        {
            return DataLoader.Airlines;
        }
    }
}