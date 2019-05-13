using System;

namespace AirTrip.Core.Exceptions
{
    public class RouteNotSupportedException : Exception
    {
        public RouteNotSupportedException(string message) : base(message)
        {
        }
    }
}