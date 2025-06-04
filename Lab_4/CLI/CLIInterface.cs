using System;
using TransportNetwork.Models;

namespace TransportNetwork.CLI
{
    public class CLIInterface
    {
        private readonly ITransportNetworkModel model;

        public CLIInterface(ITransportNetworkModel model)
        {
            this.model = model;

            // Подписываемся на события модели
            model.OnStatusChanged += (message) => Console.WriteLine($"[СТАТУС] {message}");
            model.OnErrorOccurred += (error) => Console.WriteLine($"[ОШИБКА] {error}");
        }

        public void Run()
        {
            Console.WriteLine("=== СИСТЕМА МОДЕЛИРОВАНИЯ ТРАНСПОРТНОЙ СЕТИ (CLI) ===");
            Console.WriteLine("Добро пожаловать в консольный интерфейс системы");

            bool exit = false;
            while (!exit)
            {
                ShowMenu();
                Console.Write("Введите номер операции: ");

                try
                {
                    string input = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(input)) continue;

                    switch (input.Trim())
                    {
                        case "1":
                            PlanRouteCommand();
                            break;
                        case "2":
                            MoveVehicleCommand();
                            break;
                        case "3":
                            ServiceVehicleCommand();
                            break;
                        case "4":
                            AddVehicleCommand();
                            break;
                        case "5":
                            RemoveVehicleCommand();
                            break;
                        case "6":
                            ChangeTrafficLightCommand();
                            break;
                        case "7":
                            ManageAllTrafficLightsCommand();
                            break;
                        case "8":
                            PerformMaintenanceCommand();
                            break;
                        case "9":
                            CheckSafetyCommand();
                            break;
                        case "10":
                            RunSimulationCommand();
                            break;
                        case "11":
                            ShowSystemStatusCommand();
                            break;
                        case "12":
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

                if (!exit)
                {
                    Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }

            Console.WriteLine("Завершение работы CLI системы.");
        }

        private void ShowMenu()
        {
            Console.WriteLine("\n=== ГЛАВНОЕ МЕНЮ ===");
            Console.WriteLine("1.  Запланировать маршрут");
            Console.WriteLine("2.  Переместить транспортное средство");
            Console.WriteLine("3.  Обслужить транспортное средство");
            Console.WriteLine("4.  Добавить транспортное средство");
            Console.WriteLine("5.  Удалить транспортное средство");
            Console.WriteLine("6.  Изменить состояние светофора");
            Console.WriteLine("7.  Управление всеми светофорами");
            Console.WriteLine("8.  Техническое обслуживание инфраструктуры");
            Console.WriteLine("9.  Проверка безопасности");
            Console.WriteLine("10. Запустить симуляцию");
            Console.WriteLine("11. Показать состояние системы");
            Console.WriteLine("12. Выход");
        }

        private void PlanRouteCommand()
        {
            Console.Write("Введите ID транспортного средства: ");
            if (int.TryParse(Console.ReadLine(), out int vehicleId))
            {
                Console.Write("Введите конечную точку маршрута: ");
                string endPoint = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(endPoint))
                {
                    model.PlanRoute(vehicleId, endPoint);
                }
                else
                {
                    Console.WriteLine("Конечная точка не может быть пустой.");
                }
            }
            else
            {
                Console.WriteLine("Неверный ID транспортного средства.");
            }
        }

        private void MoveVehicleCommand()
        {
            Console.Write("Введите ID транспортного средства: ");
            if (int.TryParse(Console.ReadLine(), out int vehicleId))
            {
                model.MoveVehicle(vehicleId);
            }
            else
            {
                Console.WriteLine("Неверный ID транспортного средства.");
            }
        }

        private void ServiceVehicleCommand()
        {
            Console.Write("Введите ID транспортного средства: ");
            if (int.TryParse(Console.ReadLine(), out int vehicleId))
            {
                model.ServiceVehicle(vehicleId);
            }
            else
            {
                Console.WriteLine("Неверный ID транспортного средства.");
            }
        }

        private void AddVehicleCommand()
        {
            Console.WriteLine("Доступные типы ТС: Автомобиль, Такси, Автобус, Мотоцикл");
            Console.Write("Введите тип транспортного средства: ");
            string vehicleType = Console.ReadLine();

            Console.Write("Введите начальную позицию: ");
            string startPosition = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(vehicleType) && !string.IsNullOrWhiteSpace(startPosition))
            {
                model.AddVehicle(vehicleType, startPosition);
            }
            else
            {
                Console.WriteLine("Тип и позиция не могут быть пустыми.");
            }
        }

        private void RemoveVehicleCommand()
        {
            Console.Write("Введите ID транспортного средства для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int vehicleId))
            {
                model.RemoveVehicle(vehicleId);
            }
            else
            {
                Console.WriteLine("Неверный ID транспортного средства.");
            }
        }

        private void ChangeTrafficLightCommand()
        {
            Console.Write("Введите индекс светофора (начиная с 0): ");
            if (int.TryParse(Console.ReadLine(), out int lightIndex))
            {
                model.ChangeTrafficLightState(lightIndex);
            }
            else
            {
                Console.WriteLine("Неверный индекс светофора.");
            }
        }

        private void ManageAllTrafficLightsCommand()
        {
            model.ManageAllTrafficLights();
        }

        private void PerformMaintenanceCommand()
        {
            model.PerformInfrastructureMaintenance();
        }

        private void CheckSafetyCommand()
        {
            model.CheckSafety();
        }

        private void RunSimulationCommand()
        {
            Console.Write("Введите количество шагов симуляции: ");
            if (int.TryParse(Console.ReadLine(), out int steps) && steps > 0)
            {
                Console.WriteLine($"Запуск симуляции на {steps} шагов...");
                for (int i = 0; i < steps; i++)
                {
                    Console.WriteLine($"\n--- Шаг {i + 1} ---");
                    model.RunSimulationStep();
                    System.Threading.Thread.Sleep(1000);
                }
                Console.WriteLine("Симуляция завершена.");
            }
            else
            {
                Console.WriteLine("Неверное количество шагов.");
            }
        }

        private void ShowSystemStatusCommand()
        {
            Console.WriteLine(model.GetSystemStatus());
        }
    }
}