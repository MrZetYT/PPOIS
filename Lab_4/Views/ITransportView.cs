using System;
using System.Collections.Generic;

namespace TransportNetwork.Views
{
    public interface ITransportView
    {
        // События для взаимодействия с презентером
        event Action<int, string> PlanRouteRequested;
        event Action<int> MoveVehicleRequested;
        event Action<int> ServiceVehicleRequested;
        event Action<string, string> AddVehicleRequested;
        event Action<int> RemoveVehicleRequested;
        event Action<int> ChangeTrafficLightRequested;
        event Action ManageAllTrafficLightsRequested;
        event Action PerformMaintenanceRequested;
        event Action CheckSafetyRequested;
        event Action<int> RunSimulationRequested;
        event Action RefreshStatusRequested;

        // Методы для обновления интерфейса
        void UpdateVehiclesList(IReadOnlyList<Transport> vehicles);
        void UpdateTrafficLightsList(IReadOnlyList<TrafficLight> trafficLights);
        void UpdateRoadsList(IReadOnlyList<Road> roads);
        void UpdateCrossroadsList(IReadOnlyList<Crossroad> crossroads);
        void UpdateBikePathsList(IReadOnlyList<BikePath> bikePaths);
        void UpdateSidewalksList(IReadOnlyList<Sidewalk> sidewalks);
        void UpdateSystemStatus(string status);

        // Методы для отображения сообщений
        void ShowStatusMessage(string message);
        void ShowErrorMessage(string error);

        // Метод для выполнения действий в UI потоке
        void Invoke(Action action);

        // Методы управления формой
        void Show();
        void Hide();
        void Close();
    }
}