using System;

namespace AirTrip.Core.Exceptions
{
    public class BadAirportException : Exception
    {
        public BadAirportException(string message) : base(message)
        {
        }
    }
}