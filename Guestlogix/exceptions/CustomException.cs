using System;

namespace Guestlogix.exceptions
{
    public class CustomException : Exception
    {
        public CustomException(string message) : base(message) { }
    }
}