using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportNetwork.Exceptions;

namespace TransportNetwork
{
    public class Transport : IVehicle, IRoute
    {
        private int vehicleId;
        private string vehicleType;
        private string position;
        private List<string> route;

        public Transport(int id, string type, string position)
        {
            vehicleId = id;
            vehicleType = type;
            this.position = position;
            route = new List<string>();
        }

        public int VehicleId { get => vehicleId; }
        public string Position { get => position; set => position = value; }
        public string VehicleType { get => vehicleType; }
        public List<string> Route { get => route; set => route = value; }

        public void PlanRoute(string endPosition)
        {
            if (string.IsNullOrEmpty(endPosition) || endPosition == Position)
            {
                throw new InvalidRouteException("Конечная точка маршрута некорректна.");
            }
            route = new List<string>();
            route.Add(Position);
            route.Add(endPosition);
        }

        public void Move()
        {
            if (route.Count < 2)
            {
                throw new RouteNotPlannedException();
            }
            Position = route[1];
            route.RemoveAt(0);
        }
        public void ServiceVehicle()
        {
            Console.WriteLine($"Транспортное средство {vehicleId} ({vehicleType}) успешно обслужено.");
        }
    }
}
