using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Potop.Client.Gameplay;

namespace Potop.Client.Tests.EditMode {
    /// <summary>
    /// Tests for the MutationSynergyManager and its activation logic.
    /// </summary>
    public class SynergyTests {
        private MutationSynergyManager _synergyManager;
        private SynergyRuleData _testRuleData;

        [SetUp]
        public void SetUp() {
            GameObject go = new GameObject("SynergyManagerTest");
            _synergyManager = go.AddComponent<MutationSynergyManager>();
            _testRuleData = ScriptableObject.CreateInstance<SynergyRuleData>();

            // Define a test synergy: Pierce + Explosion = PierceExplosion
            _testRuleData.Rules = new List<SynergyRule> {
                new SynergyRule {
                    Modifier1 = ModifierType.Pierce,
                    Modifier2 = ModifierType.Explosion,
                    Synergy = SynergyType.PierceExplosion
                }
            };

            // Inject rule data via reflection
            typeof(MutationSynergyManager).GetField("_synergyRuleData", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(_synergyManager, _testRuleData);
        }

        [TearDown]
        public void TearDown() {
            if (_synergyManager != null) {
                Object.DestroyImmediate(_synergyManager.gameObject);
            }
            if (_testRuleData != null) {
                Object.DestroyImmediate(_testRuleData);
            }
        }

        [Test]
        public void TestSynergyActivation_WhenBothModifiersAdded() {
            // Arrange
            SynergyType activatedSynergy = SynergyType.None;
            _synergyManager.SynergyActivated += (synergy) => activatedSynergy = synergy;

            // Act
            _synergyManager.AddModifier(ModifierType.Pierce);
            Assert.AreEqual(SynergyType.None, activatedSynergy, "Synergy should not activate with only one modifier.");
            
            _synergyManager.AddModifier(ModifierType.Explosion);

            // Assert
            Assert.AreEqual(SynergyType.PierceExplosion, activatedSynergy, "Synergy should activate when both requirements are met.");
            Assert.IsTrue(_synergyManager.HasSynergy(SynergyType.PierceExplosion), "HasSynergy should return true.");
        }

        [Test]
        public void TestSynergyDeactivation_WhenModifierRemoved() {
            // Arrange
            _synergyManager.AddModifier(ModifierType.Pierce);
            _synergyManager.AddModifier(ModifierType.Explosion);
            Assert.IsTrue(_synergyManager.HasSynergy(SynergyType.PierceExplosion));

            SynergyType deactivatedSynergy = SynergyType.None;
            _synergyManager.SynergyDeactivated += (synergy) => deactivatedSynergy = synergy;

            // Act
            _synergyManager.RemoveModifier(ModifierType.Pierce);

            // Assert
            Assert.AreEqual(SynergyType.PierceExplosion, deactivatedSynergy, "Synergy should deactivate when requirement is removed.");
            Assert.IsFalse(_synergyManager.HasSynergy(SynergyType.PierceExplosion), "HasSynergy should return false.");
        }
    }
}
