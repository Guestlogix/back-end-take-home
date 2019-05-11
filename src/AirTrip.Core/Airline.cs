using System;
using System.Text.RegularExpressions;

namespace AirTrip.Core
{
    public sealed class Airline
    {
        public string Name { get; }

        public string TwoDigitCode { get; }

        public string ThreeDigitCode { get; }

        public string Country { get; }

        private readonly Regex _twoCharacterRegex = new Regex("^[a-zA-Z0-9]{2}$", RegexOptions.Compiled);
        private readonly Regex _threeCharacterRegex = new Regex("^[a-zA-Z]{3}$", RegexOptions.Compiled);

        public Airline(string name, string twoDigitCode, string threeDigitCode, string country)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            }

            Name = name;

            if (string.IsNullOrEmpty(twoDigitCode))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(twoDigitCode));
            }

            if (_twoCharacterRegex.Match(twoDigitCode).Success)
            {
                TwoDigitCode = twoDigitCode.ToUpperInvariant();
            }
            else
            {
                throw new InvalidOperationException($"{nameof(twoDigitCode)} is not 2 digit alpha-numeric characters");
            }

            if (string.IsNullOrEmpty(threeDigitCode))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(threeDigitCode));
            }

            if (_threeCharacterRegex.Match(threeDigitCode).Success)
            {
                ThreeDigitCode = threeDigitCode.ToUpperInvariant();
            }
            else
            {
                throw new InvalidOperationException($"{nameof(threeDigitCode)} is not 3 digit alpha characters");
            }

            if (string.IsNullOrEmpty(country))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(country));
            }

            Country = country;
        }
    }
}
