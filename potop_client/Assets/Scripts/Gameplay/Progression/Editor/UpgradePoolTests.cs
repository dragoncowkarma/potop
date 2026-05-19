using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Potop.Client.Gameplay;
using Potop.Client.Gameplay.Progression;

namespace Potop.Client.Gameplay.Tests {
    public class UpgradePoolTests {
        private GameObject _poolGo;
        private UpgradePool _pool;

        [SetUp]
        public void SetUp() {
            _poolGo = new GameObject("UpgradePoolTest");
            _pool = _poolGo.AddComponent<UpgradePool>();
        }

        [TearDown]
        public void TearDown() {
            Object.DestroyImmediate(_poolGo);
        }

        [Test]
        public void PitySystem_GuaranteesEpic_AfterTenNonEpics() {
            // Set up test upgrade options using Reflection to populate the fallback pool
            var upgradesField = typeof(UpgradePool).GetField("_availableUpgrades", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var upgradeTableField = typeof(UpgradePool).GetField("_upgradeTable", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            
            var options = new List<UpgradeOption> {
                new UpgradeOption { UpgradeId = "common1", Rarity = UpgradeRarity.Common },
                new UpgradeOption { UpgradeId = "common2", Rarity = UpgradeRarity.Common },
                new UpgradeOption { UpgradeId = "common3", Rarity = UpgradeRarity.Common },
                new UpgradeOption { UpgradeId = "epic1", Rarity = UpgradeRarity.Epic }
            };
            
            upgradesField.SetValue(_pool, options);

            var upgradeTable = ScriptableObject.CreateInstance<UpgradeTableData>();
            upgradeTableField.SetValue(_pool, upgradeTable);

            // Force pity state close to triggering
            _pool.SetEpicPityCounter(9);

            // Draw options to trigger and verify the pity system
            var results = _pool.GetRandomUpgrades(3);
            
            bool hasEpic = false;
            foreach (var opt in results) {
                if (opt.Rarity == UpgradeRarity.Epic) {
                    hasEpic = true;
                }
            }

            Assert.IsTrue(hasEpic, "Epic option should be guaranteed by the pity system when pity counter >= 10.");
            Assert.Less(_pool.EpicPityCounter, 10, "Pity counter should reset after rolling Epic.");
        }
    }
}
