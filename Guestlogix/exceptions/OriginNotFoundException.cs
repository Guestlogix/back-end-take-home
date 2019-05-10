using Guestlogix.resources;

namespace Guestlogix.exceptions
{
    public class OriginNotFoundException : CustomException
    {
        public OriginNotFoundException() : base(Resource.ERR_ORIGIN_NOT_FOUND) { }
    }
}