using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportNetwork.Tests
{
    [TestFixture]
    public class SidewalkTests
    {
        [Test]
        public void Constructor_InitializesPropertiesCorrectly()
        {
            var sidewalk = new Sidewalk("Main Walk", 3, "Concrete");

            Assert.AreEqual("Main Walk", sidewalk.Name);
            Assert.AreEqual(3, sidewalk.Width);
            Assert.AreEqual("Concrete", sidewalk.Material);
        }

        [Test]
        public void PropertySetters_ModifyValuesCorrectly()
        {
            var sidewalk = new Sidewalk("Temp", 1, "Gravel");

            sidewalk.Name = "New Walk";
            sidewalk.Width = 4;
            sidewalk.Material = "Asphalt";

            Assert.AreEqual("New Walk", sidewalk.Name);
            Assert.AreEqual(4, sidewalk.Width);
            Assert.AreEqual("Asphalt", sidewalk.Material);
        }
        [Test]
        public void Sidewalk_ShouldUpdateProperties_WhenModified()
        {
            var sidewalk = new Sidewalk("Test", 2, "Stone");
            sidewalk.Width = 5;
            Assert.AreEqual(5, sidewalk.Width);
        }
    }
}
