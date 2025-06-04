using System;
using System.Windows.Forms;
using TransportNetwork.Models;
using TransportNetwork.Presenters;
using TransportNetwork.CLI;

namespace TransportNetwork
{
    public class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("=== СИСТЕМА МОДЕЛИРОВАНИЯ ТРАНСПОРТНОЙ СЕТИ ===");
            Console.WriteLine("Выберите режим запуска:");
            Console.WriteLine("1. Консольный интерфейс (CLI)");
            Console.WriteLine("2. Графический интерфейс (GUI)");
            Console.Write("Введите номер (1 или 2): ");

            string choice = Console.ReadLine();

            switch (choice?.Trim())
            {
                case "1":
                    RunCliMode();
                    break;
                case "2":
                    RunGuiMode();
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Запускается консольный режим по умолчанию.");
                    RunCliMode();
                    break;
            }
        }

        private static void RunCliMode()
        {
            Console.WriteLine("Запуск консольного интерфейса...");

            var model = new TransportNetworkModel();
            var cliInterface = new CLIInterface(model);

            cliInterface.Run();
        }

        private static void RunGuiMode()
        {
            Console.WriteLine("Запуск графического интерфейса...");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                var model = new TransportNetworkModel();
                var view = new TransportForm();
                var presenter = new TransportPresenter(model, view);

                presenter.Initialize();

                Application.Run(view);

                presenter.Cleanup();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка запуска приложения: {ex.Message}",
                    "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}