using System;
using System.Text.RegularExpressions;

namespace AirTrip.Core.Tests
{
    public sealed class Airport
    {
        public string Name { get; }
        public string City { get; }
        public string Country { get; }
        public string Code { get; }

        private readonly Regex _threeCharacterRegex = new Regex("^[a-zA-Z]{3}$", RegexOptions.Compiled);

        public Airport(string name, string city, string country, string code)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            }
            Name = name;

            if (string.IsNullOrEmpty(city))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(city));
            }
            City = city;

            if (string.IsNullOrEmpty(country))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(country));
            }
            Country = country;

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
                throw new InvalidOperationException($"{nameof(code)} is not 3 digit alpha characters");
            }
        }
    }
}