using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TransportNetwork.Views;

namespace TransportNetwork
{
    public partial class TransportForm : Form, ITransportView
    {
        private TabControl tabControl;
        private ListBox vehiclesListBox, trafficLightsListBox, roadsListBox;
        private TextBox statusTextBox, systemStatusTextBox;
        private Button planRouteBtn, moveVehicleBtn, serviceVehicleBtn, addVehicleBtn, removeVehicleBtn;
        private Button changeTrafficLightBtn, manageAllLightsBtn, maintenanceBtn, safetyBtn, simulationBtn, refreshBtn;

        public TransportForm()
        {
            InitializeComponent();
        }

        #region Events
        public event Action<int, string> PlanRouteRequested;
        public event Action<int> MoveVehicleRequested;
        public event Action<int> ServiceVehicleRequested;
        public event Action<string, string> AddVehicleRequested;
        public event Action<int> RemoveVehicleRequested;
        public event Action<int> ChangeTrafficLightRequested;
        public event Action ManageAllTrafficLightsRequested;
        public event Action PerformMaintenanceRequested;
        public event Action CheckSafetyRequested;
        public event Action<int> RunSimulationRequested;
        public event Action RefreshStatusRequested;
        #endregion

        private void InitializeComponent()
        {
            this.Text = "Система моделирования транспортной сети";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Создаем основные панели
            CreateTabControl();
            CreateStatusPanel();
        }

        private void CreateTabControl()
        {
            tabControl = new TabControl
            {
                Dock = DockStyle.Fill
            };

            // Вкладка с транспортными средствами
            var vehiclesTab = new TabPage("Транспорт");
            CreateVehiclesTab(vehiclesTab);
            tabControl.TabPages.Add(vehiclesTab);

            // Вкладка с инфраструктурой
            var infrastructureTab = new TabPage("Инфраструктура");
            CreateInfrastructureTab(infrastructureTab);
            tabControl.TabPages.Add(infrastructureTab);

            // Вкладка управления
            var controlTab = new TabPage("Управление");
            CreateControlTab(controlTab);
            tabControl.TabPages.Add(controlTab);

            // Вкладка состояния системы
            var statusTab = new TabPage("Состояние системы");
            CreateStatusTab(statusTab);
            tabControl.TabPages.Add(statusTab);

            this.Controls.Add(tabControl);
        }

        private void CreateVehiclesTab(TabPage tab)
        {
            var panel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 3
            };

            // Список транспортных средств
            var vehiclesLabel = new Label { Text = "Транспортные средства:", Dock = DockStyle.Fill };
            vehiclesListBox = new ListBox { Dock = DockStyle.Fill };

            // Кнопки управления транспортом
            var buttonPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown };

            planRouteBtn = new Button { Text = "Запланировать маршрут", Width = 150 };
            planRouteBtn.Click += PlanRouteBtn_Click;

            moveVehicleBtn = new Button { Text = "Переместить ТС", Width = 150 };
            moveVehicleBtn.Click += MoveVehicleBtn_Click;

            serviceVehicleBtn = new Button { Text = "Обслужить ТС", Width = 150 };
            serviceVehicleBtn.Click += ServiceVehicleBtn_Click;

            addVehicleBtn = new Button { Text = "Добавить ТС", Width = 150 };
            addVehicleBtn.Click += AddVehicleBtn_Click;

            removeVehicleBtn = new Button { Text = "Удалить ТС", Width = 150 };
            removeVehicleBtn.Click += RemoveVehicleBtn_Click;

            buttonPanel.Controls.AddRange(new Control[] { planRouteBtn, moveVehicleBtn, serviceVehicleBtn, addVehicleBtn, removeVehicleBtn });

            panel.Controls.Add(vehiclesLabel, 0, 0);
            panel.Controls.Add(vehiclesListBox, 0, 1);
            panel.Controls.Add(buttonPanel, 1, 1);

            tab.Controls.Add(panel);
        }

        private void CreateInfrastructureTab(TabPage tab)
        {
            var panel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 3
            };

            // Светофоры
            var lightsLabel = new Label { Text = "Светофоры:", Dock = DockStyle.Fill };
            trafficLightsListBox = new ListBox { Dock = DockStyle.Fill };

            // Дороги
            var roadsLabel = new Label { Text = "Дороги:", Dock = DockStyle.Fill };
            roadsListBox = new ListBox { Dock = DockStyle.Fill };

            panel.Controls.Add(lightsLabel, 0, 0);
            panel.Controls.Add(trafficLightsListBox, 0, 1);
            panel.Controls.Add(roadsLabel, 1, 0);
            panel.Controls.Add(roadsListBox, 1, 1);

            tab.Controls.Add(panel);
        }

        private void CreateControlTab(TabPage tab)
        {
            var panel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown
            };

            changeTrafficLightBtn = new Button { Text = "Изменить светофор", Width = 200 };
            changeTrafficLightBtn.Click += ChangeTrafficLightBtn_Click;

            manageAllLightsBtn = new Button { Text = "Управить всеми светофорами", Width = 200 };
            manageAllLightsBtn.Click += (s, e) => ManageAllTrafficLightsRequested?.Invoke();

            maintenanceBtn = new Button { Text = "Техническое обслуживание", Width = 200 };
            maintenanceBtn.Click += (s, e) => PerformMaintenanceRequested?.Invoke();

            safetyBtn = new Button { Text = "Проверка безопасности", Width = 200 };
            safetyBtn.Click += (s, e) => CheckSafetyRequested?.Invoke();

            simulationBtn = new Button { Text = "Запустить симуляцию", Width = 200 };
            simulationBtn.Click += SimulationBtn_Click;

            refreshBtn = new Button { Text = "Обновить данные", Width = 200 };
            refreshBtn.Click += (s, e) => RefreshStatusRequested?.Invoke();

            panel.Controls.AddRange(new Control[] { changeTrafficLightBtn, manageAllLightsBtn, maintenanceBtn, safetyBtn, simulationBtn, refreshBtn });
            tab.Controls.Add(panel);
        }

        private void CreateStatusTab(TabPage tab)
        {
            systemStatusTextBox = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ReadOnly = true,
                Font = new Font("Consolas", 9)
            };
            tab.Controls.Add(systemStatusTextBox);
        }

        private void CreateStatusPanel()
        {
            var statusPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 100
            };

            statusTextBox = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ReadOnly = true
            };

            var statusLabel = new Label
            {
                Text = "Журнал событий:",
                Dock = DockStyle.Top,
                Height = 20
            };

            statusPanel.Controls.Add(statusTextBox);
            statusPanel.Controls.Add(statusLabel);
            this.Controls.Add(statusPanel);
        }

        #region Event Handlers
        private void PlanRouteBtn_Click(object sender, EventArgs e)
        {
            var vehicleId = GetSelectedVehicleId();
            if (vehicleId == -1) return;

            string endPoint = Microsoft.VisualBasic.Interaction.InputBox("Введите конечную точку маршрута:", "Планирование маршрута");
            if (!string.IsNullOrWhiteSpace(endPoint))
            {
                PlanRouteRequested?.Invoke(vehicleId, endPoint);
            }
        }

        private void MoveVehicleBtn_Click(object sender, EventArgs e)
        {
            var vehicleId = GetSelectedVehicleId();
            if (vehicleId != -1)
            {
                MoveVehicleRequested?.Invoke(vehicleId);
            }
        }

        private void ServiceVehicleBtn_Click(object sender, EventArgs e)
        {
            var vehicleId = GetSelectedVehicleId();
            if (vehicleId != -1)
            {
                ServiceVehicleRequested?.Invoke(vehicleId);
            }
        }

        private void AddVehicleBtn_Click(object sender, EventArgs e)
        {
            string vehicleType = Microsoft.VisualBasic.Interaction.InputBox("Введите тип ТС (Автомобиль, Такси, Автобус, Мотоцикл):", "Добавление ТС");
            if (string.IsNullOrWhiteSpace(vehicleType)) return;

            string startPosition = Microsoft.VisualBasic.Interaction.InputBox("Введите начальную позицию:", "Добавление ТС");
            if (!string.IsNullOrWhiteSpace(startPosition))
            {
                AddVehicleRequested?.Invoke(vehicleType, startPosition);
            }
        }

        private void RemoveVehicleBtn_Click(object sender, EventArgs e)
        {
            var vehicleId = GetSelectedVehicleId();
            if (vehicleId != -1)
            {
                RemoveVehicleRequested?.Invoke(vehicleId);
            }
        }

        private void ChangeTrafficLightBtn_Click(object sender, EventArgs e)
        {
            var index = GetSelectedTrafficLightIndex();
            if (index != -1)
            {
                ChangeTrafficLightRequested?.Invoke(index);
            }
        }

        private void SimulationBtn_Click(object sender, EventArgs e)
        {
            string stepsStr = Microsoft.VisualBasic.Interaction.InputBox("Введите количество шагов симуляции:", "Симуляция");
            if (int.TryParse(stepsStr, out int steps) && steps > 0)
            {
                RunSimulationRequested?.Invoke(steps);
            }
        }
        #endregion

        #region Helper Methods
        private int GetSelectedVehicleId()
        {
            if (vehiclesListBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите транспортное средство");
                return -1;
            }

            var selectedText = vehiclesListBox.SelectedItem.ToString();
            if (selectedText.StartsWith("ID: "))
            {
                var idStr = selectedText.Substring(4, selectedText.IndexOf(',') - 4);
                if (int.TryParse(idStr, out int id))
                    return id;
            }
            return -1;
        }

        private int GetSelectedTrafficLightIndex()
        {
            if (trafficLightsListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите светофор");
                return -1;
            }
            return trafficLightsListBox.SelectedIndex;
        }
        #endregion

        #region ITransportView Implementation
        public void UpdateVehiclesList(IReadOnlyList<Transport> vehicles)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateVehiclesList(vehicles)));
                return;
            }

            vehiclesListBox.Items.Clear();
            foreach (var vehicle in vehicles)
            {
                vehiclesListBox.Items.Add($"ID: {vehicle.VehicleId}, Тип: {vehicle.VehicleType}, Позиция: {vehicle.Position}");
            }
        }

        public void UpdateTrafficLightsList(IReadOnlyList<TrafficLight> trafficLights)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateTrafficLightsList(trafficLights)));
                return;
            }

            trafficLightsListBox.Items.Clear();
            for (int i = 0; i < trafficLights.Count; i++)
            {
                trafficLightsListBox.Items.Add($"Светофор {i}: {trafficLights[i].State}");
            }
        }

        public void UpdateRoadsList(IReadOnlyList<Road> roads)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateRoadsList(roads)));
                return;
            }

            roadsListBox.Items.Clear();
            foreach (var road in roads)
            {
                roadsListBox.Items.Add($"{road.Name}: {road.Distance}м, загруженность: {road.Congestion:F1}");
            }
        }

        public void UpdateCrossroadsList(IReadOnlyList<Crossroad> crossroads) { }
        public void UpdateBikePathsList(IReadOnlyList<BikePath> bikePaths) { }
        public void UpdateSidewalksList(IReadOnlyList<Sidewalk> sidewalks) { }

        public void UpdateSystemStatus(string status)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateSystemStatus(status)));
                return;
            }
            systemStatusTextBox.Text = status;
        }

        public void ShowStatusMessage(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => ShowStatusMessage(message)));
                return;
            }
            statusTextBox.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
            statusTextBox.ScrollToCaret();
        }

        public void ShowErrorMessage(string error)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => ShowErrorMessage(error)));
                return;
            }
            MessageBox.Show(error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            statusTextBox.AppendText($"[{DateTime.Now:HH:mm:ss}] ОШИБКА: {error}{Environment.NewLine}");
        }

        public void Invoke(Action action)
        {
            if (InvokeRequired)
                base.Invoke(action);
            else
                action();
        }
        #endregion
    }
}