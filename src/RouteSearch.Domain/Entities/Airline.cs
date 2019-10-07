namespace RouteSearch.Domain.Entities
{
    public class Airline
    {
        public string TwoDigitCode { get; private set; }
        public string Name { get; private set; }
        public string Country { get; private set; }
        public string ThreeDigitCode { get; private set; }

        public Airline(string twoDigitCode, string name, string country, string threeDigitCode)
        {
            TwoDigitCode = twoDigitCode;
            Name = name;
            Country = country;
            ThreeDigitCode = threeDigitCode;
        }
    }
}