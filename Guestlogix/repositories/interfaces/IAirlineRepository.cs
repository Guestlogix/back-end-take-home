using Guestlogix.Models;
using System.Collections.Generic;

namespace Guestlogix.repositories.interfaces
{
    interface IAirlineRepository
    {
        IEnumerable<AirlineModel> GetAllAirlines();
    }
}