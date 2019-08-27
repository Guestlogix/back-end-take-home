using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightRoutesApi.Models;

namespace FlightRoutesApi.Services
{
    public interface IFlightService
    {
        IEnumerable<string> GetShortestPath(string source, string destination);
    }
}
