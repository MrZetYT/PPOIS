using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportNetwork.Tests
{
    [TestFixture]
    public class RoadTests
    {
        [Test]
        public void Constructor_InitializesCongestionCorrectly()
        {
            var cars = new List<Transport> { new Transport(1, "Car", "Start") };

            var road = new Road("Test", 100, new string[] { "A", "B" }, cars);

            Assert.AreEqual(0.7, road.Congestion);
        }

        [Test]
        public void CongestionSetter_UpdatesValueCorrectly()
        {
            var road = new Road("Test", 100, new string[] { "A" }, new List<Transport>());

            road.Congestion = 10;

            Assert.AreEqual(7, road.Congestion);
        }
        [Test]
        public void Road_ShouldHandleMaxDistance()
        {
            var road = new Road("Test", int.MaxValue, new string[0], new List<Transport>());
            Assert.AreEqual(int.MaxValue, road.Distance);
        }
        [Test]
        public void Road_ShouldShowCrossroads()
        {
            var road = new Road("Test", 100, new string[] { "A", "B" }, new List<Transport> { new Transport(1, "Car", "Start") });
            Assert.AreEqual(new string[] { "A", "B" }, road.Crossroads);
        }
    }
}
