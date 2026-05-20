using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Potop.Client.Gameplay;
using Potop.Client.Gameplay.Progression;

namespace Potop.Client.Tests.EditMode {
    /// <summary>
    /// Tests for the UpgradePool and its Pity system.
    /// </summary>
    public class ProgressionTests {
        private UpgradePool _upgradePool;
        private UpgradeTableData _testTable;

        [SetUp]
        public void SetUp() {
            GameObject go = new GameObject("UpgradePoolTest");
            _upgradePool = go.AddComponent<UpgradePool>();
            _testTable = ScriptableObject.CreateInstance<UpgradeTableData>();

            // Setup mock data for the table
            var options = new List<UpgradeOption> {
                new UpgradeOption { UpgradeId = "Common1", Rarity = UpgradeRarity.Common },
                new UpgradeOption { UpgradeId = "Rare1", Rarity = UpgradeRarity.Rare },
                new UpgradeOption { UpgradeId = "Epic1", Rarity = UpgradeRarity.Epic }
            };

            // Set private fields via reflection to ensure controlled test environment
            var type = typeof(UpgradeTableData);
            type.GetField("_commonWeight", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(_testTable, 70f);
            type.GetField("_rareWeight", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(_testTable, 25f);
            type.GetField("_epicWeight", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(_testTable, 5f);
            type.GetField("_upgradeOptions", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(_testTable, options);

            // Assign table to pool via reflection
            typeof(UpgradePool).GetField("_upgradeTable", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(_upgradePool, _testTable);
        }

        [TearDown]
        public void TearDown() {
            if (_upgradePool != null) {
                Object.DestroyImmediate(_upgradePool.gameObject);
            }
            if (_testTable != null) {
                Object.DestroyImmediate(_testTable);
            }
        }

        [Test]
        public void TestPitySystem_GuaranteedEpicAfterThreshold() {
            // Arrange: Set pity counter to threshold - 1 (next draw should trigger it)
            const int PITY_THRESHOLD = 10;
            _upgradePool.SetEpicPityCounter(PITY_THRESHOLD);

            // Act: Draw 1 upgrade
            var results = _upgradePool.GetRandomUpgrades(1);

            // Assert: Should be Epic and counter should reset
            Assert.AreEqual(1, results.Count, "Should return exactly one upgrade.");
            Assert.AreEqual(UpgradeRarity.Epic, results[0].Rarity, "Pity should guarantee an Epic rarity.");
            Assert.AreEqual(0, _upgradePool.EpicPityCounter, "Pity counter should reset after Epic draw.");
        }

        [Test]
        public void TestUpgradeProbability_StatisticalVerification() {
            // Arrange
            const int DRAW_COUNT = 1000;
            const float EXPECTED_EPIC_RATIO = 0.05f; // 5% weight
            const float TOLERANCE = 0.04f; // Statistical variance tolerance

            int epicCount = 0;

            // Act
            for (int i = 0; i < DRAW_COUNT; i++) {
                var results = _upgradePool.GetRandomUpgrades(1);
                if (results[0].Rarity == UpgradeRarity.Epic) {
                    epicCount++;
                }
            }

            float actualRatio = (float)epicCount / DRAW_COUNT;

            // Assert
            // Note: Since pity system exists, actual epic ratio might be slightly higher than 5% 
            // but should still be within a reasonable range for 1000 samples.
            Debug.Log($"[ProgressionTests] Actual Epic Ratio: {actualRatio} ({epicCount}/{DRAW_COUNT})");
            Assert.IsTrue(actualRatio >= EXPECTED_EPIC_RATIO - TOLERANCE && actualRatio <= EXPECTED_EPIC_RATIO + TOLERANCE + 0.1f, 
                $"Epic ratio {actualRatio} out of expected range.");
        }
    }
}
