using System;

namespace IDGeneration.Common.Exceptions
{
    public class SequenceOverflowException : Exception
    {
        public SequenceOverflowException(string message) : base(message)
        {
        }
    }
}
