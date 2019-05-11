using System;

namespace AirTrip.Core
{
    // TODO: Remove stringly types
    public sealed class Route
    {
        public string Airline { get; }

        public string Origin { get; }

        public string Destination { get; }

        public Route(string airline, string origin, string destination)
        {
            Airline = airline ?? throw new ArgumentNullException(nameof(airline));
            Origin = origin ?? throw new ArgumentNullException(nameof(origin));
            Destination = destination ?? throw new ArgumentNullException(nameof(destination));
        }
    }
}