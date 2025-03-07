using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Interfaces;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TransportNetwork;
using TransportNetwork.Exceptions;

namespace TransportNetwork.Tests
{
    [TestFixture]
    public class TransportTests
    {
        private Transport _transport;

        [SetUp]
        public void Setup()
        {
            _transport = new Transport(1, "����������", "������");
        }

        [Test]
        public void PlanRoute_ValidEndPoint_SetsRoute()
        {
            var endPoint = "�����";

            _transport.PlanRoute(endPoint);

            Assert.AreEqual(2, _transport.Route.Count);
            Assert.AreEqual("������", _transport.Route[0]);
            Assert.AreEqual("�����", _transport.Route[1]);
        }

        [Test]
        public void PlanRoute_SameAsCurrentPosition_ThrowsException()
        {
            var ex = Assert.Throws<InvalidRouteException>(() => _transport.PlanRoute("������"));
            Assert.AreEqual("�������� ����� �������� �����������.", ex.Message);
        }

        [Test]
        public void Move_WithValidRoute_UpdatesPosition()
        {
            _transport.PlanRoute("�����");

            _transport.Move();

            Assert.AreEqual("�����", _transport.Position);
            Assert.AreEqual(1, _transport.Route.Count);
        }

        [Test]
        public void Move_WithoutRoute_ThrowsException()
        {
            Assert.Throws<RouteNotPlannedException>(() => _transport.Move());
        }
        [Test]
        public void Move_ShouldClearRoute_AfterCompletion()
        {
            var transport = new Transport(1, "Car", "A");
            transport.PlanRoute("B");
            transport.Move();
            Assert.AreEqual(1, transport.Route.Count);
        }
        [Test]
        public void ServiceVehicle_GetMessage()
        {
            using (var consoleOutput = new ConsoleOutput())
            {
                _transport.ServiceVehicle();
                StringAssert.Contains("������������ �������� 1 (����������) ������� ���������.", consoleOutput.GetOutput());
            }
        }
        [Test]
        public void VehicleType_GetType()
        {
            Assert.AreEqual("����������", _transport.VehicleType);
        }
    }
}