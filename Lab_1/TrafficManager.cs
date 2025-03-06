using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportNetwork
{
    public static class TrafficManager
    {
        public static void ManageTraffic(List<TrafficLight> trafficLights)
        {
            Console.WriteLine("Управление движением:");
            foreach (var light in trafficLights)
            {
                light.ChangeState();
                Console.WriteLine($"  Светофор изменён. Новое состояние: {light.State}");
            }
        }
    }
}
