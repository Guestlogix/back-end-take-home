using GuestLogixRouteAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestLogixRouteAPI.BusinessLayer
{
    interface IRouteDecision
    {
        Dictionary<int, List<String>> GetAirportRoutes(string originCity, string destinationCity, DbSet<AirportRoute> listOfAirportRoutes);
        List<String> DecideRoutes(string originCity, string destCity, DbSet<AirportRoute> airportRoutes, List<String> route);
    }
}
