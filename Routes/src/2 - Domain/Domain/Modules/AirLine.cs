using System.Linq;
using Domain.Modules.Interfaces;

namespace Domain.Modules
{
    /// <summary>
    ///     AirLine
    /// </summary>
    public class AirLine : IImportCsv
    {
        public string Name { get; private set; }
        public string TwoDigitCode { get; private set; }
        public string ThreeDigitCode { get; private set; }
        public string Country { get; private set; }

        /// <summary>
        ///     Convert CSV to Class
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public object FromCsv(string[] values)
        {
            if (!values.Any()) return null;

            var airPort = new AirLine
            {
                Name = values[0].Trim(),
                TwoDigitCode = values[1].Trim(),
                ThreeDigitCode = values[2].Trim(),
                Country = values[3].Trim()
            };

            return airPort;
        }
    }
}
