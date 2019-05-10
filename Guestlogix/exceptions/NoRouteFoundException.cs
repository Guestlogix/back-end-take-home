using Guestlogix.resources;

namespace Guestlogix.exceptions
{
    public class NoRouteFoundException : CustomException
    {
        public NoRouteFoundException() : base(Resource.ERR_NO_ROUTE_FOUND) { }
    }
}