using System;
using System.Collections.Generic;
using System.Linq;

namespace RouteSearch.Domain.Entities
{
    public class Airport : IEquatable<Airport>
    {
        public string Iata { get; private set; }
        public string Name { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }        

        public Airport(string iata)
        {
            Iata = iata;
        }

        public Airport(string iata, string name,
                        string city, string country,
                        double latitude, double longitude)
        {
            Iata = iata;
            Name = name;
            City = city;
            Country = country;
            Latitude = latitude;
            Longitude = longitude;
        }           

        public bool Equals(Airport other)
        {
            if (other is null)
                return false;

            if (!(Iata == other.Iata))
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            return Iata.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            var airport = obj as Airport;
            if (airport is null)
                return false;

            return Equals(airport);
        }

        public static bool operator == (Airport airport1, Airport airport2)
        {
            if (((object)airport1) == null || ((object)airport2) == null)
                return Object.Equals(airport1, airport2);

            return airport1.Equals(airport2);
        }

        public static bool operator != (Airport airport1, Airport airport2)
        {
            if (((object)airport1) == null || ((object)airport2) == null)
                return ! Object.Equals(airport1, airport2);

            return ! airport1.Equals(airport2);
        }
    }
}