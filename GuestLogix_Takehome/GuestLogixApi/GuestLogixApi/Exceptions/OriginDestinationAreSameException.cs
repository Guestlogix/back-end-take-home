using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuestLogixApi.Exceptions
{
    public class OriginDestinationAreSameException : Exception
    {
        public OriginDestinationAreSameException(string message) : base(message)
        {
        }
    }
}
