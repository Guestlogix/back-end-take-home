using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightRoutesApi.Models;

namespace FlightRoutesApi.Repositories
{
    public interface IFlightRepository
    {
        List<Airport> GetAirports();

        List<Route> GetAirlineRoutes();
    }
}
