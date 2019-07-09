using System.Linq;
using Domain.Modules.Interfaces;

namespace Domain.Modules.Dto
{
    public class Route : IImportCsv
    {
        public string AirLine { get; private set; }
        public string Origin { get; private set; }
        public string Destination { get; private set; }

        /// <summary>
        ///     Convert CSV to Class
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public object FromCsv(string[] values)
        {
            if (!values.Any()) return null;

            var airPort = new Route
            {
                AirLine = values[0].Trim(),
                Origin = values[1].Trim(),
                Destination = values[2].Trim()
            };

            return airPort;
        }
    }
}