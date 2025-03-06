using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportNetwork
{
    internal interface IRoads
    {
        string Name { get; }
        int Distance { get; }
        string[] Crossroads { get; }
        List<Transport> Cars { get; }
        double Congestion { get; set; }
    }
}
