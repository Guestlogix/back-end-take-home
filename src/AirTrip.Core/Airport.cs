using System;
using System.Text.RegularExpressions;

namespace AirTrip.Core
{
    public sealed class Airport
    {
        private readonly Regex _threeCharacterRegex = new Regex("^[a-zA-Z]{3}$", RegexOptions.Compiled);

        public Airport(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(code));
            }

            if (_threeCharacterRegex.Match(code).Success)
            {
                Code = code.ToUpperInvariant();
            }
            else
            {
                throw new InvalidOperationException($"{nameof(code)} is not a valid Airport Code");
            }
        }

        public string Code { get; }

        private bool Equals(Airport other)
        {
            return string.Equals(Code, other.Code);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is Airport other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (Code != null ? Code.GetHashCode() : 0);
        }

        public static bool operator ==(Airport left, Airport right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Airport left, Airport right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return Code;
        }
    }
}