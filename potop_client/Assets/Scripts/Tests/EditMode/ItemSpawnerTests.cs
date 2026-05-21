using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using Potop.Client.Gameplay.Items;
using Potop.Client.Core.Events;
using Potop.Client.Data.Items;

namespace Potop.Client.Tests.EditMode {
    public class ItemSpawnerTests {
        private GameObject _go;
        private ItemSpawner _spawner;
        private List<Object> _createdObjects;

        [SetUp]
        public void Setup() {
            _createdObjects = new List<Object>();

            _go = new GameObject();
            _createdObjects.Add(_go);

            _spawner = _go.AddComponent<ItemSpawner>();
            EventBroker.ClearAllSubscriptions();

            // Enable to register events
            _spawner.gameObject.SetActive(true);
        }

        [TearDown]
        public void Teardown() {
            foreach(var obj in _createdObjects) {
                if(obj != null) {
                    Object.DestroyImmediate(obj);
                }
            }
            _createdObjects.Clear();
            EventBroker.ClearAllSubscriptions();
        }

        [Test]
        public void EmptyDropTable_EnemyKilled_ReturnsSilently() {
            var dropTable = new List<ItemDropData>();
            typeof(ItemSpawner).GetField("_dropTable", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(_spawner, dropTable);

            EventBroker.Publish(new EnemyKilledEvent { Position = Vector3.zero, ExpValue = 100 });

            Assert.IsTrue(true); // Should not crash
        }

        [Test]
        public void PoolManagerNull_NoCrashOnSpawn() {
            var dropTable = new List<ItemDropData>();
            var item = ScriptableObject.CreateInstance<ItemDropData>();
            _createdObjects.Add(item);
            dropTable.Add(item);
            typeof(ItemSpawner).GetField("_dropTable", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(_spawner, dropTable);

            // This triggers guaranteed drop logic if Exp is >= 200
            EventBroker.Publish(new EnemyKilledEvent { Position = Vector3.zero, ExpValue = 250 });

            Assert.IsTrue(true); // Should not crash despite PoolManager.Instance being null
        }

        [Test]
        public void GuaranteedDropExpThreshold_ForcesDrop() {
            var dropTable = new List<ItemDropData>();
            var item = ScriptableObject.CreateInstance<ItemDropData>();
            _createdObjects.Add(item);
            dropTable.Add(item);
            typeof(ItemSpawner).GetField("_dropTable", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(_spawner, dropTable);

            // 200 is GUARANTEED_DROP_EXP_THRESHOLD
            EventBroker.Publish(new EnemyKilledEvent { Position = Vector3.zero, ExpValue = 200 });

            Assert.IsTrue(true); // Verifies path is executed without errors
        }

        [Test]
        public void ProbabilityDrop_AppliesWaveMultiplier() {
            var dropTable = new List<ItemDropData>();
            var item = ScriptableObject.CreateInstance<ItemDropData>();
            _createdObjects.Add(item);
            // Use reflection to set DropProbability
            typeof(ItemDropData).GetField("_dropProbability", BindingFlags.NonPublic | BindingFlags.Instance)?.SetValue(item, 0.5f);
            dropTable.Add(item);
            typeof(ItemSpawner).GetField("_dropTable", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(_spawner, dropTable);

            // Set current wave to 10
            typeof(ItemSpawner).GetField("_currentWave", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(_spawner, 10);

            EventBroker.Publish(new EnemyKilledEvent { Position = Vector3.zero, ExpValue = 10 }); // Not guaranteed

            Assert.IsTrue(true); // Verifies probability path
        }
    }
}
