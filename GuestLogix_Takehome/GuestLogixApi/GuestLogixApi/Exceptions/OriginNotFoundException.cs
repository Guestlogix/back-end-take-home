using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuestLogixApi.Exceptions
{
    public class OriginNotFoundException : Exception
    {
        public OriginNotFoundException(string message) : base(message)
        {
        }
    }
}
