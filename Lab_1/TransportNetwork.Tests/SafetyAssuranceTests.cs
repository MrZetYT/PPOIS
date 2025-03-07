using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportNetwork.Tests
{
    [TestFixture]
    public class SafetyAssuranceTests
    {
        [Test]
        public void CheckSafety_DetectsUnplannedRoutes()
        {
            var transport = new Transport(1, "Bus", "Start");
            var transports = new List<Transport> { transport };
            var roads = new List<Road>();

            using (var consoleOutput = new ConsoleOutput())
            {
                SafetyAssurance.CheckSafety(transports, roads);
                StringAssert.Contains("не имеет запланированного маршрута", consoleOutput.GetOutput());
            }
        }
        [Test]
        public void CheckSafety_DetectsHighCongestion()
        {
            var transport1 = new Transport(1, "Bus", "Start");
            var transport2 = new Transport(2, "Bus", "Start");
            var transport3 = new Transport(3, "Bus", "Start");
            var transport4 = new Transport(4, "Bus", "Start");
            var transport5 = new Transport(5, "Bus", "Start");
            var transport6 = new Transport(6, "Bus", "Start");
            var transport7 = new Transport(7, "Bus", "Start");
            var transport8 = new Transport(8, "Bus", "Start");
            var transport9 = new Transport(9, "Bus", "Start");
            var transport10 = new Transport(10, "Bus", "Start");
            var transport11 = new Transport(11, "Bus", "Start");
            var transports = new List<Transport> { transport1, transport2, transport3, transport4, transport5, transport6, transport7,
                                                    transport8, transport9, transport10, transport11};
            string[] crossroads = { "Тепловой", "Холодный" };
            var road = new Road("Первомайская", 100, crossroads, transports);
            var roads = new List<Road>() { road};

            using (var consoleOutput = new ConsoleOutput())
            {
                SafetyAssurance.CheckSafety(transports, roads);
                StringAssert.Contains("Предупреждение: на дороге Первомайская высокая загруженность!", consoleOutput.GetOutput());
            }
        }
    }
}
