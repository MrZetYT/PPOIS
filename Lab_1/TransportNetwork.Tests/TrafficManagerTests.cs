using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportNetwork.Tests
{
    [TestFixture]
    public class TrafficManagerTests
    {
        [Test]
        public void ManageTraffic_ChangesAllTrafficLights()
        {
            var lights = new List<TrafficLight> { new TrafficLight(), new TrafficLight() };

            TrafficManager.ManageTraffic(lights);

            foreach (var light in lights)
            {
                Assert.AreEqual(TrafficLight.TrafficLightState.Yellow, light.State);
            }
        }
    }
}
