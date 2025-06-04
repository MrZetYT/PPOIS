using System;

namespace TransportNetwork.Exceptions
{
    public class InvalidRouteException : Exception
    {
        public InvalidRouteException() : base() { }
        public InvalidRouteException(string message) : base(message) { }
        public InvalidRouteException(string message, Exception innerException) : base(message, innerException) { }
    }
}