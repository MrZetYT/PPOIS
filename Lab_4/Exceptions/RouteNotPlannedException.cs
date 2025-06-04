using System;

namespace TransportNetwork.Exceptions
{
    public class RouteNotPlannedException : Exception
    {
        public RouteNotPlannedException() : base("Маршрут не запланирован. Невозможно переместить транспортное средство.") { }
        public RouteNotPlannedException(string message) : base(message) { }
        public RouteNotPlannedException(string message, Exception innerException) : base(message, innerException) { }
    }
}