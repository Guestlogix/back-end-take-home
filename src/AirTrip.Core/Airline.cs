using System;
using System.Text.RegularExpressions;

namespace AirTrip.Core
{
    public sealed class Airline
    {
        private readonly Regex _twoCharacterRegex = new Regex("^[a-zA-Z0-9]{2}$", RegexOptions.Compiled);

        public Airline(string code)
        {
            if (string.IsNullOrEmpty(code)) throw new ArgumentException("Value cannot be null or empty.", nameof(code));

            if (_twoCharacterRegex.Match(code).Success)
            {
                Code = code.ToUpperInvariant();
            }
            else
            {
                throw new InvalidOperationException($"{nameof(code)} is not 2 digit alpha-numeric characters");
            }
        }

        private bool Equals(Airline other)
        {
            return string.Equals(Code, other.Code);
        }

        public override string ToString()
        {
            return Code;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is Airline other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }

        public static bool operator ==(Airline left, Airline right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Airline left, Airline right)
        {
            return !Equals(left, right);
        }

        public string Code { get; }
    }
}