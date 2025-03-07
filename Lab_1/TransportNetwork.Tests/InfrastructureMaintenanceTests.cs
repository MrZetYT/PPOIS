using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportNetwork.Tests
{
    [TestFixture]
    public class InfrastructureMaintenanceTests
    {
        [Test]
        public void PerformMaintenance_UpdatesRoadCongestion()
        {
            var road = new Road("Test", 200, new string[0], new List<Transport> { new Transport(1, "Truck", "Point") });
            var roads = new List<Road> { road };

            InfrastructureMaintenance.PerformMaintenance(roads);

            Assert.AreEqual(0.35, road.Congestion);
        }
    }
}
