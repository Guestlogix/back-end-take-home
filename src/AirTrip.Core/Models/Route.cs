using System;

namespace AirTrip.Core.Models
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

        private bool Equals(Route other)
        {
            return Airline.Equals(other.Airline) && Origin.Equals(other.Origin) && Destination.Equals(other.Destination);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is Route other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Airline.GetHashCode();
                hashCode = (hashCode * 397) ^ Origin.GetHashCode();
                hashCode = (hashCode * 397) ^ Destination.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(Route left, Route right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Route left, Route right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return $"{Airline}--{Origin}--{Destination}";
        }
    }
}