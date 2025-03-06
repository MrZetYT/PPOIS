using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportNetwork
{
    internal interface IVehicle
    {
        int VehicleId { get; }
        string VehicleType { get; }
        string Position { get; set; }
        void Move();
    }
}
