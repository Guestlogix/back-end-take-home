using System.Collections.Generic;
using System.Linq;

namespace Domain.Modules.ValueObject
{
    public class Route
    {
        public Route()
        {
            Flights = new List<Flight>();
        }
        public IList<Flight> Flights { get; set; }
        public string ConnectingFlights => $"{(Flights.Any() ? string.Join(" -> ", Flights.OrderBy(x=> x.Order).Select(x => x.AirPort.Iata3)) : string.Empty)}";
    }
}