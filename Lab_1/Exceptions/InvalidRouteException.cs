using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportNetwork.Exceptions
{
    public class InvalidRouteException : Exception
    {
        public InvalidRouteException(string message) : base(message) { }
    }
}
