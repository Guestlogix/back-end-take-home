using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuestLogixApi.Exceptions
{
    public class DestinationNotFoundException : Exception
    {
        public DestinationNotFoundException(string message) : base(message)
        {
        }
    }
}
