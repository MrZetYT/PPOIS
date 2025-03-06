using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportNetwork
{
    public class Crossroad : ICrossroads
    {
        private string name;
        private TrafficLight trafficLight;
        private string[] roads;

        public Crossroad(string name, TrafficLight trafficLight, string[] roads)
        {
            this.name = name;
            this.trafficLight = trafficLight;
            this.roads = roads;
        }
        public string Name { get => name; }
        public TrafficLight TrafficLight { get => trafficLight; }
        public string[] Roads { get => roads; }
    }
}
