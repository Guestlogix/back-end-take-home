using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Modules.Interfaces;

namespace Domain.Modules
{
    /// <summary>
    ///     AirPort
    /// </summary>
    public class AirPort : IImportCsv
    {
        public string Name { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }
        public string Iata3 { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public IList<Route> DestinationRoutes { get; private set; }
     
        public void AddRangeDestinationRoute(IList<Route> routes)
        {
            if (DestinationRoutes == null) DestinationRoutes = new List<Route>();
            DestinationRoutes = routes;
        }

        /// <summary>
        ///     Convert CSV to Class
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public object FromCsv(string[] values)
        {
            if (!values.Any()) return null;

            var airPort = new AirPort
            {
                Name = values[0].Trim(),
                City = values[1].Trim(),
                Country = values[2].Trim(),
                Iata3 = values[3].Trim(),
                Latitude = Convert.ToDouble(values[4].Trim()),
                Longitude = Convert.ToDouble(values[5].Trim())
            };

            return airPort;
        }

    }
}
