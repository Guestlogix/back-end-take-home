using Guestlogix.resources;

namespace Guestlogix.exceptions
{
    public class DestinationNotFoundException : CustomException
    {
        public DestinationNotFoundException() : base(Resource.ERR_DESTINATION_NOT_FOUND) { }
    }
}