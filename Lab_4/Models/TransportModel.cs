using System;
using System.Collections.Generic;
using System.Linq;
using TransportNetwork.Exceptions;

namespace TransportNetwork.Models
{
    public class TransportNetworkModel : ITransportNetworkModel
    {
        private readonly List<Transport> vehicles;
        private readonly List<TrafficLight> trafficLights;
        private readonly List<Road> roads;
        private readonly List<Crossroad> crossroads;
        private readonly List<BikePath> bikePaths;
        private readonly List<Sidewalk> sidewalks;
        private int nextVehicleId;
        private readonly Random random;

        public TransportNetworkModel()
        {
            vehicles = new List<Transport>();
            trafficLights = new List<TrafficLight>();
            roads = new List<Road>();
            crossroads = new List<Crossroad>();
            bikePaths = new List<BikePath>();
            sidewalks = new List<Sidewalk>();
            nextVehicleId = 1;
            random = new Random();

            InitializeDefaultData();
        }

        // Events для MVP паттерна
        public event Action<string> OnStatusChanged;
        public event Action<string> OnErrorOccurred;
        public event Action OnModelStateChanged;

        // Properties для доступа к данным
        public IReadOnlyList<Transport> Vehicles => vehicles.AsReadOnly();
        public IReadOnlyList<TrafficLight> TrafficLights => trafficLights.AsReadOnly();
        public IReadOnlyList<Road> Roads => roads.AsReadOnly();
        public IReadOnlyList<Crossroad> Crossroads => crossroads.AsReadOnly();
        public IReadOnlyList<BikePath> BikePaths => bikePaths.AsReadOnly();
        public IReadOnlyList<Sidewalk> Sidewalks => sidewalks.AsReadOnly();

        private void InitializeDefaultData()
        {
            // Создание базовых объектов как в оригинальной программе
            var trafficLight = new TrafficLight();
            trafficLights.Add(trafficLight);

            var crossroad = new Crossroad("Центральный перекресток", trafficLight,
                new string[] { "Улица 1", "Улица 2" });
            crossroads.Add(crossroad);

            var vehicle = new Transport(nextVehicleId++, "Автомобиль", "Начало маршрута");
            vehicles.Add(vehicle);

            var road = new Road("Главная дорога", 500,
                new string[] { "Перекресток A", "Перекресток B" },
                new List<Transport> { vehicle });
            roads.Add(road);

            var bikePath = new BikePath("Велодорожка 1", 1000, true);
            bikePaths.Add(bikePath);

            var sidewalk = new Sidewalk("Пешеходный тротуар 1", 5, "Асфальт");
            sidewalks.Add(sidewalk);

            NotifyModelStateChanged();
        }

        // Методы для управления транспортными средствами
        public void PlanRoute(int vehicleId, string endPoint)
        {
            try
            {
                var vehicle = FindVehicleById(vehicleId);
                vehicle.PlanRoute(endPoint);
                OnStatusChanged?.Invoke($"Маршрут запланирован для ТС {vehicleId}: {string.Join(" -> ", vehicle.Route)}");
                NotifyModelStateChanged();
            }
            catch (Exception ex)
            {
                OnErrorOccurred?.Invoke($"Ошибка планирования маршрута: {ex.Message}");
            }
        }

        public void MoveVehicle(int vehicleId)
        {
            try
            {
                var vehicle = FindVehicleById(vehicleId);
                vehicle.Move();
                OnStatusChanged?.Invoke($"ТС {vehicleId} переместилось. Новая позиция: {vehicle.Position}");
                NotifyModelStateChanged();
            }
            catch (Exception ex)
            {
                OnErrorOccurred?.Invoke($"Ошибка перемещения ТС {vehicleId}: {ex.Message}");
            }
        }

        public void ServiceVehicle(int vehicleId)
        {
            try
            {
                var vehicle = FindVehicleById(vehicleId);
                vehicle.ServiceVehicle();
                OnStatusChanged?.Invoke($"ТС {vehicleId} успешно обслужено");
                NotifyModelStateChanged();
            }
            catch (Exception ex)
            {
                OnErrorOccurred?.Invoke($"Ошибка обслуживания ТС {vehicleId}: {ex.Message}");
            }
        }

        public void AddVehicle(string vehicleType, string startPosition)
        {
            try
            {
                ValidateVehicleType(vehicleType);
                ValidatePosition(startPosition);

                var vehicle = new Transport(nextVehicleId++, vehicleType, startPosition);
                vehicles.Add(vehicle);

                // Обновляем загруженность дорог
                UpdateRoadCongestion();

                OnStatusChanged?.Invoke($"Создано новое ТС {vehicle.VehicleId} ({vehicleType}) в позиции {startPosition}");
                NotifyModelStateChanged();
            }
            catch (Exception ex)
            {
                OnErrorOccurred?.Invoke($"Ошибка создания ТС: {ex.Message}");
            }
        }

        public void RemoveVehicle(int vehicleId)
        {
            try
            {
                var vehicle = FindVehicleById(vehicleId);
                vehicles.Remove(vehicle);

                // Обновляем загруженность дорог
                UpdateRoadCongestion();

                OnStatusChanged?.Invoke($"ТС {vehicleId} удалено из системы");
                NotifyModelStateChanged();
            }
            catch (Exception ex)
            {
                OnErrorOccurred?.Invoke($"Ошибка удаления ТС {vehicleId}: {ex.Message}");
            }
        }

        // Методы для управления светофорами
        public void ChangeTrafficLightState(int lightIndex)
        {
            try
            {
                ValidateTrafficLightIndex(lightIndex);

                trafficLights[lightIndex].ChangeState();
                OnStatusChanged?.Invoke($"Светофор {lightIndex} изменен. Новое состояние: {trafficLights[lightIndex].State}");
                NotifyModelStateChanged();
            }
            catch (Exception ex)
            {
                OnErrorOccurred?.Invoke($"Ошибка изменения светофора: {ex.Message}");
            }
        }

        public void ManageAllTrafficLights()
        {
            try
            {
                TrafficManager.ManageTraffic(trafficLights.ToList());
                OnStatusChanged?.Invoke("Управление всеми светофорами выполнено");
                NotifyModelStateChanged();
            }
            catch (Exception ex)
            {
                OnErrorOccurred?.Invoke($"Ошибка управления светофорами: {ex.Message}");
            }
        }

        // Методы для обслуживания инфраструктуры
        public void PerformInfrastructureMaintenance()
        {
            try
            {
                InfrastructureMaintenance.PerformMaintenance(roads.ToList());
                OnStatusChanged?.Invoke("Техническое обслуживание инфраструктуры выполнено");
                NotifyModelStateChanged();
            }
            catch (Exception ex)
            {
                OnErrorOccurred?.Invoke($"Ошибка технического обслуживания: {ex.Message}");
            }
        }

        public void CheckSafety()
        {
            try
            {
                SafetyAssurance.CheckSafety(vehicles.ToList(), roads.ToList());
                OnStatusChanged?.Invoke("Проверка безопасности выполнена");
            }
            catch (Exception ex)
            {
                OnErrorOccurred?.Invoke($"Ошибка проверки безопасности: {ex.Message}");
            }
        }

        // Методы для симуляции
        public void RunSimulationStep()
        {
            try
            {
                // Возможность добавления нового ТС
                if (random.NextDouble() < 0.3)
                {
                    var possibleDestinations = GetPossibleDestinations();
                    var startPoint = possibleDestinations[random.Next(possibleDestinations.Count)];
                    var vehicleTypes = GetAvailableVehicleTypes();
                    var type = vehicleTypes[random.Next(vehicleTypes.Length)];
                    AddVehicle(type, startPoint);
                }

                // Обработка существующих ТС
                var vehiclesCopy = new List<Transport>(vehicles);
                foreach (var vehicle in vehiclesCopy)
                {
                    ProcessVehicleInSimulation(vehicle);
                }

                // Управление светофорами
                ManageAllTrafficLights();

                OnStatusChanged?.Invoke("Выполнен шаг симуляции");
            }
            catch (Exception ex)
            {
                OnErrorOccurred?.Invoke($"Ошибка симуляции: {ex.Message}");
            }
        }

        // Получение строкового представления состояния системы
        public string GetSystemStatus()
        {
            var status = new System.Text.StringBuilder();
            status.AppendLine("=== СОСТОЯНИЕ СИСТЕМЫ ===");

            AppendVehiclesStatus(status);
            AppendTrafficLightsStatus(status);
            AppendRoadsStatus(status);
            AppendCrossroadsStatus(status);
            AppendBikePathsStatus(status);
            AppendSidewalksStatus(status);

            return status.ToString();
        }

        #region Private Helper Methods

        private Transport FindVehicleById(int vehicleId)
        {
            var vehicle = vehicles.FirstOrDefault(v => v.VehicleId == vehicleId);
            if (vehicle == null)
            {
                throw new ArgumentException($"Транспортное средство с ID {vehicleId} не найдено");
            }
            return vehicle;
        }

        private void ValidateVehicleType(string vehicleType)
        {
            if (string.IsNullOrWhiteSpace(vehicleType))
            {
                throw new ArgumentException("Тип транспортного средства не может быть пустым");
            }

            var validTypes = GetAvailableVehicleTypes();
            if (!validTypes.Contains(vehicleType))
            {
                throw new ArgumentException($"Недопустимый тип транспортного средства: {vehicleType}");
            }
        }

        private void ValidatePosition(string position)
        {
            if (string.IsNullOrWhiteSpace(position))
            {
                throw new ArgumentException("Позиция не может быть пустой");
            }
        }

        private void ValidateTrafficLightIndex(int lightIndex)
        {
            if (lightIndex < 0 || lightIndex >= trafficLights.Count)
            {
                throw new ArgumentOutOfRangeException($"Неверный индекс светофора: {lightIndex}");
            }
        }

        private void UpdateRoadCongestion()
        {
            foreach (var road in roads)
            {
                var vehiclesOnRoad = vehicles.Where(v => IsVehicleOnRoad(v, road)).ToList();
                road.Congestion = vehiclesOnRoad.Count * 0.7;
            }
        }

        private bool IsVehicleOnRoad(Transport vehicle, Road road)
        {
            // Упрощенная логика - проверяем, находится ли ТС на одном из перекрестков дороги
            return road.Crossroads.Contains(vehicle.Position);
        }

        private void ProcessVehicleInSimulation(Transport vehicle)
        {
            if (vehicle.Route == null || vehicle.Route.Count < 2)
            {
                if (random.NextDouble() < 0.5)
                {
                    var possibleDestinations = GetPossibleDestinations();
                    string dest;
                    do
                    {
                        dest = possibleDestinations[random.Next(possibleDestinations.Count)];
                    } while (dest == vehicle.Position);

                    PlanRoute(vehicle.VehicleId, dest);
                }
                else
                {
                    RemoveVehicle(vehicle.VehicleId);
                    return;
                }
            }
            MoveVehicle(vehicle.VehicleId);
        }

        private List<string> GetPossibleDestinations()
        {
            return new List<string> { "A", "B", "C", "D", "E" };
        }

        private string[] GetAvailableVehicleTypes()
        {
            return new string[] { "Автомобиль", "Такси", "Автобус", "Мотоцикл" };
        }

        private void AppendVehiclesStatus(System.Text.StringBuilder status)
        {
            status.AppendLine("\nТранспортные средства:");
            if (vehicles.Count == 0)
            {
                status.AppendLine("  Нет транспортных средств");
                return;
            }

            foreach (var vehicle in vehicles)
            {
                status.AppendLine($"  ID: {vehicle.VehicleId}, Тип: {vehicle.VehicleType}, " +
                                 $"Позиция: {vehicle.Position}, Маршрут: {(vehicle.Route.Count > 0 ? string.Join(" -> ", vehicle.Route) : "не задан")}");
            }
        }

        private void AppendTrafficLightsStatus(System.Text.StringBuilder status)
        {
            status.AppendLine("\nСветофоры:");
            for (int i = 0; i < trafficLights.Count; i++)
            {
                status.AppendLine($"  Светофор {i}: {trafficLights[i].State}");
            }
        }

        private void AppendRoadsStatus(System.Text.StringBuilder status)
        {
            status.AppendLine("\nДороги:");
            foreach (var road in roads)
            {
                status.AppendLine($"  {road.Name}: дистанция {road.Distance}м, загруженность {road.Congestion:F1}");
            }
        }

        private void AppendCrossroadsStatus(System.Text.StringBuilder status)
        {
            status.AppendLine("\nПерекрестки:");
            foreach (var crossroad in crossroads)
            {
                status.AppendLine($"  {crossroad.Name}: дороги [{string.Join(", ", crossroad.Roads)}]");
            }
        }

        private void AppendBikePathsStatus(System.Text.StringBuilder status)
        {
            status.AppendLine("\nВелодорожки:");
            foreach (var bikePath in bikePaths)
            {
                status.AppendLine($"  {bikePath.Name}: {bikePath.Length}м, выделена: {bikePath.IsDedicated}");
            }
        }

        private void AppendSidewalksStatus(System.Text.StringBuilder status)
        {
            status.AppendLine("\nТротуары:");
            foreach (var sidewalk in sidewalks)
            {
                status.AppendLine($"  {sidewalk.Name}: {sidewalk.Width}м, материал: {sidewalk.Material}");
            }
        }

        private void NotifyModelStateChanged()
        {
            OnModelStateChanged?.Invoke();
        }

        #endregion
    }
}