using Guestlogix.Models;
using System.Collections.Generic;

namespace Guestlogix.repositories.interfaces
{
    interface IAirportRepository
    {
        IEnumerable<AirportModel> GetAllAirports();
    }
}