using System;

namespace Common.Helpers.Exceptions
{
    public sealed class ConnectionException : Exception
    {
        private static readonly string _message = "A connection to the server couldn't be established.";

        public ConnectionException() : base(_message)
        {
        }

        public ConnectionException(string message) : base(message)
        {
        }
    }
}