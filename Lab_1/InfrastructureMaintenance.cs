using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportNetwork
{
    public static class InfrastructureMaintenance
    {
        public static void PerformMaintenance(List<Road> roads)
        {
            Console.WriteLine("Техническое обслуживание инфраструктуры:");
            foreach (var road in roads)
            {
                road.Congestion = road.Cars.Count * 0.5;
                Console.WriteLine($"  Обслужена дорога {road.Name}. Новая загруженность: {road.Congestion}");
            }
        }
    }
}
