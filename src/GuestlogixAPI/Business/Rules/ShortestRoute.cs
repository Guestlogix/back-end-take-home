using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.DTO;
using Business.Exceptions;
using Database.Model;

namespace Business.Rules
{
    public static class ShortestRoute
    {
        public static List<RouteDTO> GetRoute(string origin, string destin, List<RouteDTO> allRoutes)
        {
            var result = new List<RouteDTO>();

            var allOrigins = allRoutes.Where(a => a.Origin == origin);
            var allDestins = allRoutes.Where(a => a.Destin == destin);
            if (!allOrigins.Any() && !allDestins.Any())
            {
                throw new ValidationException("Invalid Origin and Destination");
            }
            if(!allDestins.Any())
            {
                throw new ValidationException("Invalid Destination");
            }
            if (!allOrigins.Any())
            {
                throw new ValidationException("Invalid Origin");
            }



            result.AddRange(allOrigins);
            result.AddRange(allDestins);

            return result;
        }
    }
}
