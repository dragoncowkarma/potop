using NUnit.Framework;
using UnityEngine;
using Potop.Client.Gameplay.Combat;
using Potop.Client.Core.Events;

namespace Potop.Client.Tests.EditMode {
    public class EnergyManagerTests {
        private GameObject _go;
        private EnergyManager _manager;

        [SetUp]
        public void Setup() {
            _go = new GameObject();
            _manager = _go.AddComponent<EnergyManager>();
            EventBroker.ClearAllSubscriptions();
        }

        [TearDown]
        public void Teardown() {
            Object.DestroyImmediate(_go);
            EventBroker.ClearAllSubscriptions();
        }

        [Test]
        public void StartFromZero_AddEnergy50_Returns50() {
            Assert.AreEqual(0, _manager.CurrentEnergy);

            _manager.AddEnergy(50);

            Assert.AreEqual(50, _manager.CurrentEnergy);
        }

        [Test]
        public void AddEnergyMaxPlus100_ClampsToMax() {
            _manager.AddEnergy(EnergyManager.MAX_ENERGY + 100);

            Assert.AreEqual(EnergyManager.MAX_ENERGY, _manager.CurrentEnergy);
        }

        [Test]
        public void ConsumeEnergy_Insufficient_ReturnsFalse() {
            _manager.AddEnergy(10);

            bool result = _manager.ConsumeEnergy(20);

            Assert.IsFalse(result);
            Assert.AreEqual(10, _manager.CurrentEnergy); // unchanged
        }

        [Test]
        public void ConsumeEnergy_Sufficient_ReturnsTrueAndUpdatesResidual() {
            _manager.AddEnergy(50);

            bool result = _manager.ConsumeEnergy(20);

            Assert.IsTrue(result);
            Assert.AreEqual(30, _manager.CurrentEnergy);
        }

        [Test]
        public void AddEnergy_PublishesEnergyChangedEvent() {
            bool eventFired = false;
            EventBroker.Subscribe<EnergyChangedEvent>(e => {
                eventFired = true;
                Assert.AreEqual(50, e.CurrentEnergy);
                Assert.AreEqual(EnergyManager.MAX_ENERGY, e.MaxEnergy);
            });

            _manager.AddEnergy(50);

            Assert.IsTrue(eventFired);
        }
    }
}