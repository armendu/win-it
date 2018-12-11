using System;

namespace Common.Helpers.Exceptions
{
    public sealed class NotFoundException: Exception
    {
        private static readonly string _message = "Record was not found.";

        public NotFoundException(): base(_message)
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }
    }
}