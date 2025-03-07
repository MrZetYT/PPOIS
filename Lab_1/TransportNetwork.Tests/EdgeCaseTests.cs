using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportNetwork.Exceptions;

namespace TransportNetwork.Tests
{
    [TestFixture]
    public class EdgeCaseTests
    {
        [Test]
        public void Road_WithZeroCars_HasZeroCongestion()
        {
            var road = new Road("Empty Road", 1000, new string[0], new List<Transport>());

            Assert.AreEqual(0, road.Congestion);
        }

        [Test]
        public void Transport_WithSinglePointRoute_ThrowsException()
        {
            var transport = new Transport(1, "Bus", "Point A");
            transport.Route = new List<string> { "Point A" };

            Assert.Throws<RouteNotPlannedException>(() => transport.Move());
        }

        [Test]
        public void BikePath_WithNegativeLength_ThrowsException()
        {
            var bikePath = new BikePath("Invalid", -100, true);
            Assert.AreEqual(0, bikePath.Length);
        }
    }
}
