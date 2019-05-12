using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuestLogixApi.Exceptions
{
    public class RouteNotFoundException : Exception
    {
        public RouteNotFoundException(string message) : base(message)
        {
        }
    }
}
