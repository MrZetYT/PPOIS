using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportNetwork.Tests
{
    [TestFixture]
    public class BikePathTests
    {
        [Test]
        public void Constructor_InitializesPropertiesCorrectly()
        {
            var bikePath = new BikePath("City Trail", 1500, true);

            Assert.AreEqual("City Trail", bikePath.Name);
            Assert.AreEqual(1500, bikePath.Length);
            Assert.IsTrue(bikePath.IsDedicated);
        }
    }
}
