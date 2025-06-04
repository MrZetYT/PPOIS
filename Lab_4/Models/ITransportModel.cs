using System;
using System.Collections.Generic;

namespace TransportNetwork.Models
{
    public interface ITransportNetworkModel
    {
        // Events для MVP паттерна
        event Action<string> OnStatusChanged;
        event Action<string> OnErrorOccurred;
        event Action OnModelStateChanged;

        // Properties для доступа к данным (только чтение)
        IReadOnlyList<Transport> Vehicles { get; }
        IReadOnlyList<TrafficLight> TrafficLights { get; }
        IReadOnlyList<Road> Roads { get; }
        IReadOnlyList<Crossroad> Crossroads { get; }
        IReadOnlyList<BikePath> BikePaths { get; }
        IReadOnlyList<Sidewalk> Sidewalks { get; }

        // Методы для управления транспортными средствами
        void PlanRoute(int vehicleId, string endPoint);
        void MoveVehicle(int vehicleId);
        void ServiceVehicle(int vehicleId);
        void AddVehicle(string vehicleType, string startPosition);
        void RemoveVehicle(int vehicleId);

        // Методы для управления светофорами
        void ChangeTrafficLightState(int lightIndex);
        void ManageAllTrafficLights();

        // Методы для обслуживания инфраструктуры
        void PerformInfrastructureMaintenance();
        void CheckSafety();

        // Методы для симуляции
        void RunSimulationStep();

        // Получение информации о состоянии системы
        string GetSystemStatus();
    }
}