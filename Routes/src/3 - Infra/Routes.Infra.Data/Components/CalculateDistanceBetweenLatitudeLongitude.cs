using System;
namespace Routes.Infra.Data.Components
{
    /// <summary>
    ///  Calculate distance between origin and destination
    /// </summary>
    public class CalculateDistanceBetweenLatitudeLongitude
    {
        /// <summary>
        ///     Calculate distance between origin and destination and return in kilometers.
        /// </summary>
        /// <param name="oLatitude"></param>
        /// <param name="oLongitude"></param>
        /// <param name="dLatitude"></param>
        /// <param name="dLongitude"></param>
        /// <returns></returns>
        public static double CalculateByKilometers(double oLatitude, double oLongitude, double dLatitude, double dLongitude)
        {
            var r = 6371;
            var dLat = (dLatitude - oLatitude) * Math.PI / 180;
            var dLon = (dLongitude - oLongitude) * Math.PI / 180;
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(oLatitude * Math.PI / 180) * Math.Cos(dLatitude * Math.PI / 180) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = r * c;
            return d <= 1 ? Convert.ToDouble(d * 1000) : d;
        }
    }
}
