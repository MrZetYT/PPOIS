using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportNetwork.Tests
{
    [TestFixture]
    public class CrossroadTests
    {
        [Test]
        public void Constructor_InitializesPropertiesCorrectly()
        {
            var trafficLight = new TrafficLight();
            var roads = new[] { "Street A", "Street B" };

            var crossroad = new Crossroad("Central", trafficLight, roads);

            Assert.AreEqual("Central", crossroad.Name);
            Assert.AreEqual(trafficLight, crossroad.TrafficLight);
            CollectionAssert.AreEqual(roads, crossroad.Roads);
        }
    }
}
