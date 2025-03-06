using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportNetwork
{
    public static class SafetyAssurance
    {
        public static void CheckSafety(List<Transport> transports, List<Road> roads)
        {
            Console.WriteLine("Проверка безопасности:");
            foreach (var vehicle in transports)
            {
                if (vehicle.Route.Count == 0)
                {
                    Console.WriteLine($"  Предупреждение: транспортное средство {vehicle.VehicleId} не имеет запланированного маршрута!");
                }
            }
            foreach (var road in roads)
            {
                if (road.Congestion > road.Distance / 100.0)
                {
                    Console.WriteLine($"  Предупреждение: на дороге {road.Name} высокая загруженность!");
                }
            }
            Console.WriteLine("Проверка безопасности завершена.");
        }
    }
}
