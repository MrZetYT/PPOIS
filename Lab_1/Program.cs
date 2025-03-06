using System;
using System.Collections.Generic;
using TransportNetwork.Exceptions;

namespace TransportNetwork
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в систему моделирования транспортной сети");

            TrafficLight trafficLight = new TrafficLight();
            Crossroad crossroad = new Crossroad("Центральный перекресток", trafficLight, new string[] { "Улица 1", "Улица 2" });
            Transport vehicle = new Transport(1, "Автомобиль", "Начало маршрута");
            Road road = new Road("Главная дорога", 500, new string[] { "Перекресток A", "Перекресток B" }, new List<Transport> { vehicle });

            BikePath bikePath = new BikePath("Велодорожка 1", 1000, true);
            Sidewalk sidewalk = new Sidewalk("Пешеходный тротуар 1", 5, "Асфальт");

            List<TrafficLight> trafficLights = new List<TrafficLight> { trafficLight };
            List<Road> roads = new List<Road> { road };
            List<Transport> vehicles = new List<Transport> { vehicle };

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nВыберите операцию:");
                Console.WriteLine("1. Запланировать маршрут");
                Console.WriteLine("2. Переместить транспортное средство");
                Console.WriteLine("3. Изменить состояние светофора");
                Console.WriteLine("4. Показать текущее состояние системы");
                Console.WriteLine("5. Управление движением (Traffic Management)");
                Console.WriteLine("6. Техническое обслуживание инфраструктуры");
                Console.WriteLine("7. Обеспечение безопасности");
                Console.WriteLine("8. Обслуживание транспортного средства");
                Console.WriteLine("9. Симуляция жизни");
                Console.WriteLine("10. Выход");
                Console.Write("Введите номер операции: ");

                try
                {
                    string input = Console.ReadLine();
                    if (input == null) continue;
                    switch (input)
                    {
                        case "1":
                            Console.Write("Введите конечную точку маршрута: ");
                            string endPoint = Console.ReadLine();
                            vehicle.PlanRoute(endPoint);
                            Console.WriteLine("Маршрут запланирован:");
                            Console.WriteLine(string.Join(" -> ", vehicle.Route));
                            break;
                        case "2":
                            vehicle.Move();
                            Console.WriteLine($"Транспортное средство переместилось. Новая позиция: {vehicle.Position}");
                            break;
                        case "3":
                            trafficLight.ChangeState();
                            Console.WriteLine($"Новое состояние светофора: {trafficLight.State}");
                            break;
                        case "4":
                            Console.WriteLine("Состояние системы:");
                            Console.WriteLine($"  Положение транспортного средства: {vehicle.Position}");
                            Console.WriteLine($"  Запланированный маршрут: {(vehicle.Route.Count > 0 ? string.Join(" -> ", vehicle.Route) : "не задан")}");
                            Console.WriteLine($"  Состояние светофора: {trafficLight.State}");
                            Console.WriteLine($"  Дорога: {road.Name}, дистанция: {road.Distance} м, загруженность: {road.Congestion}");
                            Console.WriteLine($"  Перекресток: {crossroad.Name}, дороги: {string.Join(", ", crossroad.Roads)}");
                            Console.WriteLine($"  Велодорожка: {bikePath.Name}, длина: {bikePath.Length} м, выделена: {bikePath.IsDedicated}");
                            Console.WriteLine($"  Пешеходный тротуар: {sidewalk.Name}, ширина: {sidewalk.Width} м, материал: {sidewalk.Material}");
                            break;
                        case "5":
                            TrafficManager.ManageTraffic(trafficLights);
                            break;
                        case "6":
                            InfrastructureMaintenance.PerformMaintenance(roads);
                            break;
                        case "7":
                            SafetyAssurance.CheckSafety(vehicles, roads);
                            break;
                        case "8":
                            vehicle.ServiceVehicle();
                            break;
                        case "9":
                            RunSimulation(vehicles, trafficLights);
                            break;
                        case "10":
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Неверный выбор, попробуйте снова.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Произошла ошибка: {ex.Message}");
                }
            }
            Console.WriteLine("Завершение работы системы.");
        }
        public static void RunSimulation(List<Transport> vehicles, List<TrafficLight> trafficLights)
        {
            Console.Write("Введите количество шагов симуляции: ");
            if (!int.TryParse(Console.ReadLine(), out int steps) || steps <= 0)
            {
                Console.WriteLine("Некорректное число шагов.");
                return;
            }

            Random rnd = new Random();
            List<string> possibleDestinations = new List<string> { "A", "B", "C", "D", "E" };
            int nextVehicleId = vehicles.Any() ? vehicles.Max(v => v.VehicleId) + 1 : 1;

            for (int i = 0; i < steps; i++)
            {
                Console.WriteLine($"\n--- Шаг симуляции {i + 1} ---");

                if (rnd.NextDouble() < 0.3)
                {
                    string startPoint = possibleDestinations[rnd.Next(possibleDestinations.Count)];
                    string[] types = { "Автомобиль", "Такси", "Автобус", "Мотоцикл" };
                    string type = types[rnd.Next(types.Length)];
                    var newVehicle = new Transport(nextVehicleId, type, startPoint);
                    vehicles.Add(newVehicle);
                    Console.WriteLine($"Создано новое транспортное средство {newVehicle.VehicleId} ({newVehicle.VehicleType}) с позицией {newVehicle.Position}.");
                    nextVehicleId++;
                }

                var vehiclesCopy = new List<Transport>(vehicles);
                foreach (var vehicle in vehiclesCopy)
                {
                    if (vehicle.Route == null || vehicle.Route.Count < 2)
                    {
                        if (rnd.NextDouble() < 0.5)
                        {
                            string current = vehicle.Position;
                            string dest;
                            do
                            {
                                dest = possibleDestinations[rnd.Next(possibleDestinations.Count)];
                            } while (dest == current);

                            try
                            {
                                vehicle.PlanRoute(dest);
                                Console.WriteLine($"Транспортное средство {vehicle.VehicleId} запланировало маршрут: {string.Join(" -> ", vehicle.Route)}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Ошибка при планировании маршрута для транспортного средства {vehicle.VehicleId}: {ex.Message}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Транспортное средство {vehicle.VehicleId} завершило маршрут и покидает симуляцию.");
                            vehicles.Remove(vehicle);
                            continue;
                        }
                    }

                    try
                    {
                        vehicle.Move();
                        Console.WriteLine($"Транспортное средство {vehicle.VehicleId} переместилось. Новая позиция: {vehicle.Position}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка при перемещении транспортного средства {vehicle.VehicleId}: {ex.Message}");
                    }
                }

                TrafficManager.ManageTraffic(trafficLights);

                Thread.Sleep(1000);
            }
            Console.WriteLine("Симуляция жизни завершена.");
        }

    }
}