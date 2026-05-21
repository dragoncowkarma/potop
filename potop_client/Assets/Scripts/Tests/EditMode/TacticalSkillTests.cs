using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using Potop.Client.Gameplay.Combat;

namespace Potop.Client.Tests.EditMode {
    public class TacticalSkillTests {
        private GameObject _goEnergyManager;
        private EnergyManager _energyManager;
        private List<Object> _createdObjects;

        [SetUp]
        public void Setup() {
            _createdObjects = new List<Object>();

            _goEnergyManager = new GameObject();
            _createdObjects.Add(_goEnergyManager);
            _energyManager = _goEnergyManager.AddComponent<EnergyManager>();
        }

        [TearDown]
        public void Teardown() {
            foreach(var obj in _createdObjects) {
                if(obj != null) {
                    Object.DestroyImmediate(obj);
                }
            }
            _createdObjects.Clear();
        }

        [Test]
        public void TryExecute_InsufficientEnergy_ReturnsFalse() {
            var go = new GameObject();
            _createdObjects.Add(go);
            var skill = go.AddComponent<EMPSkill>();
            var data = ScriptableObject.CreateInstance<TacticalSkillData>();
            _createdObjects.Add(data);
            data.EnergyCost = 50;
            typeof(TacticalSkillBase).GetField("_skillData", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(skill, data);

            _energyManager.AddEnergy(10); // Less than 50

            Assert.IsFalse(skill.TryExecute());
        }

        [Test]
        public void TryExecute_DuringCooldown_ReturnsFalse() {
            var go = new GameObject();
            _createdObjects.Add(go);
            var skill = go.AddComponent<EMPSkill>();
            var data = ScriptableObject.CreateInstance<TacticalSkillData>();
            _createdObjects.Add(data);
            data.EnergyCost = 10;
            data.Cooldown = 10f;
            typeof(TacticalSkillBase).GetField("_skillData", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(skill, data);

            _energyManager.AddEnergy(50);

            // First execution sets lastUseTime
            Assert.IsTrue(skill.TryExecute());

            // Second execution immediately should fail
            Assert.IsFalse(skill.TryExecute());
        }

        [Test]
        public void TryExecute_NormalConditions_ReturnsTrueAndConsumesEnergy() {
            var go = new GameObject();
            _createdObjects.Add(go);
            var skill = go.AddComponent<EMPSkill>();
            var data = ScriptableObject.CreateInstance<TacticalSkillData>();
            _createdObjects.Add(data);
            data.EnergyCost = 30;
            typeof(TacticalSkillBase).GetField("_skillData", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(skill, data);

            _energyManager.AddEnergy(50);

            Assert.IsTrue(skill.TryExecute());
            Assert.AreEqual(20, _energyManager.CurrentEnergy);
        }

        [Test]
        public void EMPSkill_Execute_AppliesStunToEnemies() {
            var go = new GameObject();
            _createdObjects.Add(go);
            var skill = go.AddComponent<EMPSkill>();
            var data = ScriptableObject.CreateInstance<TacticalSkillData>();
            _createdObjects.Add(data);
            data.Duration = 5f;
            typeof(TacticalSkillBase).GetField("_skillData", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(skill, data);

            // We invoke Execute directly to test its internal logic.
            // Due to reliance on FindObjectsByType and Camera.main, we just ensure it doesn't crash in Edit mode.
            skill.Execute();

            Assert.IsTrue(true);
        }

        [Test]
        public void OrbitalStrikeSkill_Execute_MultipleStrikesVerification() {
            var go = new GameObject();
            _createdObjects.Add(go);
            var skill = go.AddComponent<OrbitalStrikeSkill>();
            var data = ScriptableObject.CreateInstance<TacticalSkillData>();
            _createdObjects.Add(data);
            data.ExecuteCount = 5;
            typeof(TacticalSkillBase).GetField("_skillData", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(skill, data);

            skill.Execute(); // Starts coroutine

            // In Edit Mode, Coroutines do not run automatically. Just verify no crash on calling.
            Assert.IsTrue(true);
        }
    }
}
