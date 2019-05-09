using Guestlogix.Models;
using Guestlogix.repositories.interfaces;
using System.Collections.Generic;

namespace Guestlogix.repositories.classes
{
    public class FlightRepository : IFlightRepository
    {
        public IEnumerable<FlightModel> GetAllFlights()
        {
            return null;
        }
    }
}