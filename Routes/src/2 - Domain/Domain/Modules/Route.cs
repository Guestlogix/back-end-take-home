using Domain.Modules.ValueObject;

namespace Domain.Modules
{
    /// <summary>
    ///     Route
    /// </summary>
    public class Route
    {
        public AirLine AirLine { get; private set; }
        public AirPort Origin { get; private set; }
        public AirPort Destination { get; private set; }
        public Distance Distance { get; private set; }

        public static class RouteFactory
        {
            public static Route NewRoute(AirLine airLine, AirPort origin, AirPort destination, Distance distance)
            {
                return new Route
                {
                    AirLine = airLine,
                    Origin = origin,
                    Destination = destination,
                    Distance = distance
                };
            }
        }
    }
}
