using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportNetwork
{
    internal interface IRoute
    {
        List<string> Route { get; set; }
        void PlanRoute(string endOfRoute);
    }
}
