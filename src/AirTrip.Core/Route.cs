using System;

namespace AirTrip.Core
{
    public sealed class Route
    {
        public Airline Airline { get; }

        public Airport Origin { get; }

        public Airport Destination { get; }

        public Route(Airline airline, Airport origin, Airport destination)
        {
            Airline = airline ?? throw new ArgumentNullException(nameof(airline));
            Origin = origin ?? throw new ArgumentNullException(nameof(origin));
            Destination = destination ?? throw new ArgumentNullException(nameof(destination));
        }
    }
}