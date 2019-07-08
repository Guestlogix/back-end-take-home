using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Business.DTO;

namespace AirplaneAPI.Helpers
{
    public static class Util
    {
        public static string RoutesToString(List<RouteDTO> routes)
        {
            if (!routes.Any())
                return "No Route";
            var result = routes.First().Origin;
            foreach (var route in routes)
            {
                result += " -> " + route.Destin;
            }

            return result;
        }
    }
}