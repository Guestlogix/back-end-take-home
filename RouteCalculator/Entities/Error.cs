using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouteCalculator.Entities
{
    public class Error
    {
        public string Code { get; set; }

        public Error(string errorCode)
        {
            Code = errorCode;
        }
        
        public static Error None = new Error($"{nameof(None)}");

        public static Error InvalidOriginAirport = new Error($"{nameof(InvalidOriginAirport)}");

        public static Error InvalidDestinationAirport = new Error($"{nameof(InvalidDestinationAirport)}");

        public static Error NoRouteFound = new Error($"{nameof(NoRouteFound)}");

        public static Error TryParse(string errorCode)
        {
            if (string.IsNullOrWhiteSpace(errorCode))
            {
                return None;
            }

            var errors = new[] {None, InvalidOriginAirport, InvalidDestinationAirport, NoRouteFound};
            
            return errors.FirstOrDefault(error =>
                error.Code.Trim().Equals(errorCode, StringComparison.OrdinalIgnoreCase));
        }
    }
}
