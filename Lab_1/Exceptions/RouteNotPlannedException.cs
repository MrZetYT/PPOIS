using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportNetwork.Exceptions
{
    public class RouteNotPlannedException : Exception
    {
        public RouteNotPlannedException() : base("Маршрут не запланирован!") { }
    }
}
