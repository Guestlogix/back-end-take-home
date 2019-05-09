using Guestlogix.Models;
using System.Collections.Generic;

namespace Guestlogix.repositories.interfaces
{
    interface IFlightRepository
    {
        IEnumerable<FlightModel> GetAllFlights();
    }
}