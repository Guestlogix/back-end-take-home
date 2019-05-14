using System;
using JetBrains.Annotations;

namespace AirTrip.Core.Models
{
    public sealed class Route
    {
        public Airport Origin { get; }

        public Airport Destination { get; }

        public Route([NotNull] Airport origin, [NotNull] Airport destination)
        {
            Origin = origin ?? throw new ArgumentNullException(nameof(origin));
            Destination = destination ?? throw new ArgumentNullException(nameof(destination));
        }

        private bool Equals(Route other)
        {
            return Origin.Equals(other.Origin) && Destination.Equals(other.Destination);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is Route other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Origin.GetHashCode() * 397) ^ Destination.GetHashCode();
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
    }
}