using System;
using TransportNetwork.Models;
using TransportNetwork.Views;

namespace TransportNetwork.Presenters
{
    public class TransportPresenter
    {
        private readonly ITransportNetworkModel model;
        private readonly ITransportView view;

        public TransportPresenter(ITransportNetworkModel model, ITransportView view)
        {
            this.model = model;
            this.view = view;

            // Подписываемся на события модели
            model.OnStatusChanged += OnModelStatusChanged;
            model.OnErrorOccurred += OnModelErrorOccurred;
            model.OnModelStateChanged += OnModelStateChanged;

            // Подписываемся на события представления
            view.PlanRouteRequested += OnPlanRouteRequested;
            view.MoveVehicleRequested += OnMoveVehicleRequested;
            view.ServiceVehicleRequested += OnServiceVehicleRequested;
            view.AddVehicleRequested += OnAddVehicleRequested;
            view.RemoveVehicleRequested += OnRemoveVehicleRequested;
            view.ChangeTrafficLightRequested += OnChangeTrafficLightRequested;
            view.ManageAllTrafficLightsRequested += OnManageAllTrafficLightsRequested;
            view.PerformMaintenanceRequested += OnPerformMaintenanceRequested;
            view.CheckSafetyRequested += OnCheckSafetyRequested;
            view.RunSimulationRequested += OnRunSimulationRequested;
            view.RefreshStatusRequested += OnRefreshStatusRequested;

            // Инициализируем представление данными из модели
            UpdateViewData();
        }

        #region Model Event Handlers

        private void OnModelStatusChanged(string message)
        {
            view.ShowStatusMessage(message);
        }

        private void OnModelErrorOccurred(string error)
        {
            view.ShowErrorMessage(error);
        }

        private void OnModelStateChanged()
        {
            UpdateViewData();
        }

        #endregion

        #region View Event Handlers

        private void OnPlanRouteRequested(int vehicleId, string endPoint)
        {
            try
            {
                model.PlanRoute(vehicleId, endPoint);
            }
            catch (Exception ex)
            {
                view.ShowErrorMessage($"Ошибка планирования маршрута: {ex.Message}");
            }
        }

        private void OnMoveVehicleRequested(int vehicleId)
        {
            try
            {
                model.MoveVehicle(vehicleId);
            }
            catch (Exception ex)
            {
                view.ShowErrorMessage($"Ошибка перемещения ТС: {ex.Message}");
            }
        }

        private void OnServiceVehicleRequested(int vehicleId)
        {
            try
            {
                model.ServiceVehicle(vehicleId);
            }
            catch (Exception ex)
            {
                view.ShowErrorMessage($"Ошибка обслуживания ТС: {ex.Message}");
            }
        }

        private void OnAddVehicleRequested(string vehicleType, string startPosition)
        {
            try
            {
                model.AddVehicle(vehicleType, startPosition);
            }
            catch (Exception ex)
            {
                view.ShowErrorMessage($"Ошибка добавления ТС: {ex.Message}");
            }
        }

        private void OnRemoveVehicleRequested(int vehicleId)
        {
            try
            {
                model.RemoveVehicle(vehicleId);
            }
            catch (Exception ex)
            {
                view.ShowErrorMessage($"Ошибка удаления ТС: {ex.Message}");
            }
        }

        private void OnChangeTrafficLightRequested(int lightIndex)
        {
            try
            {
                model.ChangeTrafficLightState(lightIndex);
            }
            catch (Exception ex)
            {
                view.ShowErrorMessage($"Ошибка изменения светофора: {ex.Message}");
            }
        }

        private void OnManageAllTrafficLightsRequested()
        {
            try
            {
                model.ManageAllTrafficLights();
            }
            catch (Exception ex)
            {
                view.ShowErrorMessage($"Ошибка управления светофорами: {ex.Message}");
            }
        }

        private void OnPerformMaintenanceRequested()
        {
            try
            {
                model.PerformInfrastructureMaintenance();
            }
            catch (Exception ex)
            {
                view.ShowErrorMessage($"Ошибка технического обслуживания: {ex.Message}");
            }
        }

        private void OnCheckSafetyRequested()
        {
            try
            {
                model.CheckSafety();
            }
            catch (Exception ex)
            {
                view.ShowErrorMessage($"Ошибка проверки безопасности: {ex.Message}");
            }
        }

        private void OnRunSimulationRequested(int steps)
        {
            try
            {
                view.ShowStatusMessage($"Запуск симуляции на {steps} шагов...");

                // Запуск симуляции в отдельном потоке, чтобы не блокировать UI
                var simulationTask = System.Threading.Tasks.Task.Run(() =>
                {
                    for (int i = 0; i < steps; i++)
                    {
                        model.RunSimulationStep();
                        System.Threading.Thread.Sleep(1000);

                        // Обновляем UI в основном потоке
                        view.Invoke(() => view.ShowStatusMessage($"Шаг симуляции {i + 1}/{steps}"));
                    }

                    view.Invoke(() => view.ShowStatusMessage("Симуляция завершена"));
                });
            }
            catch (Exception ex)
            {
                view.ShowErrorMessage($"Ошибка симуляции: {ex.Message}");
            }
        }

        private void OnRefreshStatusRequested()
        {
            UpdateViewData();
            view.ShowStatusMessage("Данные обновлены");
        }

        #endregion

        #region Private Methods

        private void UpdateViewData()
        {
            try
            {
                view.UpdateVehiclesList(model.Vehicles);
                view.UpdateTrafficLightsList(model.TrafficLights);
                view.UpdateRoadsList(model.Roads);
                view.UpdateCrossroadsList(model.Crossroads);
                view.UpdateBikePathsList(model.BikePaths);
                view.UpdateSidewalksList(model.Sidewalks);
                view.UpdateSystemStatus(model.GetSystemStatus());
            }
            catch (Exception ex)
            {
                view.ShowErrorMessage($"Ошибка обновления данных: {ex.Message}");
            }
        }

        #endregion

        #region Public Methods

        public void Initialize()
        {
            UpdateViewData();
            view.ShowStatusMessage("Система инициализирована");
        }

        public void Cleanup()
        {
            // Отписываемся от событий модели
            model.OnStatusChanged -= OnModelStatusChanged;
            model.OnErrorOccurred -= OnModelErrorOccurred;
            model.OnModelStateChanged -= OnModelStateChanged;

            // Отписываемся от событий представления
            view.PlanRouteRequested -= OnPlanRouteRequested;
            view.MoveVehicleRequested -= OnMoveVehicleRequested;
            view.ServiceVehicleRequested -= OnServiceVehicleRequested;
            view.AddVehicleRequested -= OnAddVehicleRequested;
            view.RemoveVehicleRequested -= OnRemoveVehicleRequested;
            view.ChangeTrafficLightRequested -= OnChangeTrafficLightRequested;
            view.ManageAllTrafficLightsRequested -= OnManageAllTrafficLightsRequested;
            view.PerformMaintenanceRequested -= OnPerformMaintenanceRequested;
            view.CheckSafetyRequested -= OnCheckSafetyRequested;
            view.RunSimulationRequested -= OnRunSimulationRequested;
            view.RefreshStatusRequested -= OnRefreshStatusRequested;
        }

        #endregion
    }
}