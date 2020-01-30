using System;

namespace IDGeneration.Common.Exceptions
{
    public class InvalidSystemClockException : Exception
    {
        public InvalidSystemClockException(string message) : base(message)
        {
        }
    }
}
