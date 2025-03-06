using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportNetwork
{
    internal interface ICrossroads
    {
        string Name { get; }
        TrafficLight TrafficLight { get; }
        string[] Roads { get; }
    }
}
