using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportNetwork.Exceptions;

namespace TransportNetwork.Tests
{
    [TestFixture]
    public class ExceptionTests
    {
        [Test]
        public void RouteNotPlannedException_ThrowsCorrectMessage()
        {
            var transport = new Transport(1, "Car", "Start");

            var ex = Assert.Throws<RouteNotPlannedException>(() => transport.Move());
            Assert.AreEqual("Маршрут не запланирован!", ex.Message);
        }

        [Test]
        public void InvalidRouteException_ThrowsCorrectMessage()
        {
            var transport = new Transport(1, "Car", "Start");

            var ex = Assert.Throws<InvalidRouteException>(() => transport.PlanRoute("Start"));
            Assert.AreEqual("Конечная точка маршрута некорректна.", ex.Message);
        }
        [Test]
        public void PlanRoute_ShouldThrowException_WhenEmptyDestination()
        {
            var transport = new Transport(1, "Car", "Start");
            var ex = Assert.Throws<InvalidRouteException>(() => transport.PlanRoute(""));
            Assert.That(ex.Message, Does.Contain("некорректна"));
        }
    }
}
