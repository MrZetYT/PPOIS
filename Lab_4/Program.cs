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
            Console.WriteLine("=== ������� ������������� ������������ ���� ===");
            Console.WriteLine("�������� ����� �������:");
            Console.WriteLine("1. ���������� ��������� (CLI)");
            Console.WriteLine("2. ����������� ��������� (GUI)");
            Console.Write("������� ����� (1 ��� 2): ");

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
                    Console.WriteLine("�������� �����. ����������� ���������� ����� �� ���������.");
                    RunCliMode();
                    break;
            }
        }

        private static void RunCliMode()
        {
            Console.WriteLine("������ ����������� ����������...");

            var model = new TransportNetworkModel();
            var cliInterface = new CLIInterface(model);

            cliInterface.Run();
        }

        private static void RunGuiMode()
        {
            Console.WriteLine("������ ������������ ����������...");

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
                MessageBox.Show($"������ ������� ����������: {ex.Message}",
                    "����������� ������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}