using AirTrip.Main.Endpoints.Models;
using Swashbuckle.Examples;

namespace AirTrip.Main.Endpoints.Examples
{
    public sealed class SuccessfulResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new SuccessResponse
            {
                ShortestRoute = new[] {"YYZ", "YOW", "YUL"}
            };
        }
    }
}