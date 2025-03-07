using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TransportNetwork.Tests
{
    [TestFixture]
    public class TrafficLightTests
    {
        [Test]
        public void ChangeState_RedToYellow_ChangesStateCorrectly()
        {
            var trafficLight = new TrafficLight();
            trafficLight.ChangeState();
            Assert.AreEqual(TrafficLight.TrafficLightState.Yellow, trafficLight.State);
        }

        [Test]
        public void ChangeState_YellowToGreen_ChangesStateCorrectly()
        {
            var trafficLight = new TrafficLight();
            trafficLight.ChangeState(); // Red → Yellow
            trafficLight.ChangeState(); // Yellow → Green
            Assert.AreEqual(TrafficLight.TrafficLightState.Green, trafficLight.State);
        }

        [Test]
        public void ChangeState_GreenToRed_ChangesStateCorrectly()
        {
            var trafficLight = new TrafficLight();
            trafficLight.ChangeState(); // Red → Yellow
            trafficLight.ChangeState(); // Yellow → Green
            trafficLight.ChangeState(); // Green → Red
            Assert.AreEqual(TrafficLight.TrafficLightState.Red, trafficLight.State);
        }

        [Test]
        public void ChangeState_InvalidState_ResetsToRed()
        {
            var trafficLight = new TrafficLight();

            trafficLight.State = (TransportNetwork.TrafficLight.TrafficLightState)999;
            trafficLight.ChangeState();
            Assert.AreEqual(TrafficLight.TrafficLightState.Red, trafficLight.State);
        }

        [Test]
        public void ChangeState_FullCycle_ReturnsToInitialState()
        {
            var trafficLight = new TrafficLight();

            // Red → Yellow → Green → Red
            trafficLight.ChangeState();
            trafficLight.ChangeState();
            trafficLight.ChangeState();

            Assert.AreEqual(TrafficLight.TrafficLightState.Red, trafficLight.State);
        }
    }
}
