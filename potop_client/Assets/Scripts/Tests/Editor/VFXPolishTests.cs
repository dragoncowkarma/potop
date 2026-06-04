using NUnit.Framework;
using UnityEngine;
using Potop.Client.Gameplay.VFX;
using Potop.Client.Core.Pooling;

namespace Potop.Client.Gameplay.VFX.Tests {
    public class VFXPolishTests {
        private GameObject _managerGo;
        private VFXBudgetManager _budgetManager;

        [SetUp]
        public void SetUp() {
            _managerGo = new GameObject("VFXBudgetManagerTest");
            _budgetManager = _managerGo.AddComponent<VFXBudgetManager>();
            
            // Invoke Awake to initialize singleton
            var awake = typeof(VFXBudgetManager).GetMethod("Awake", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            awake?.Invoke(_budgetManager, null);
        }

        [TearDown]
        public void TearDown() {
            Object.DestroyImmediate(_managerGo);
            VFXQualitySettings.CurrentQuality = VFXQuality.High;
        }

        [Test]
        public void VFXQualitySettings_SetsQualityCorrectly() {
            VFXQualitySettings.CurrentQuality = VFXQuality.Low;
            Assert.AreEqual(VFXQuality.Low, VFXQualitySettings.CurrentQuality);

            VFXQualitySettings.CurrentQuality = VFXQuality.High;
            Assert.AreEqual(VFXQuality.High, VFXQualitySettings.CurrentQuality);
        }

        [Test]
        public void ExplosionEffect_QualityFallback_AppliesCorrectly() {
            // Given
            GameObject effectGo = new GameObject("TestExplosion");
            var ps = effectGo.AddComponent<ParticleSystem>();
            var trail = effectGo.AddComponent<TrailRenderer>();
            var light = effectGo.AddComponent<Light>();
            var effect = effectGo.AddComponent<ExplosionEffect>();

            // Setup original values
            var main = ps.main;
            main.maxParticles = 100;
            var emission = ps.emission;
            emission.rateOverTimeMultiplier = 50f;

            // When quality is Low
            VFXQualitySettings.CurrentQuality = VFXQuality.Low;
            
            // Invoke Awake and OnEnable to simulate lifecycle in EditMode test
            var awake = typeof(ExplosionEffect).GetMethod("Awake", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            awake?.Invoke(effect, null);
            var onEnable = typeof(ExplosionEffect).GetMethod("OnEnable", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            onEnable?.Invoke(effect, null);

            // Then
            Assert.IsFalse(trail.enabled, "TrailRenderer should be disabled on Low quality.");
            Assert.IsFalse(light.enabled, "Light should be disabled on Low quality.");
            Assert.Less(ps.main.maxParticles, 100, "Max particles should be scaled down on Low quality.");
            Assert.Less(ps.emission.rateOverTimeMultiplier, 50f, "Emission rate should be scaled down on Low quality.");

            // Cleanup
            Object.DestroyImmediate(effectGo);
        }

        [Test]
        public void ExplosionEffect_ResetEffect_ClearsState() {
            // Given
            GameObject effectGo = new GameObject("TestExplosionReset");
            var ps = effectGo.AddComponent<ParticleSystem>();
            var trail = effectGo.AddComponent<TrailRenderer>();
            var effect = effectGo.AddComponent<ExplosionEffect>();

            // Invoke Awake
            var awake = typeof(ExplosionEffect).GetMethod("Awake", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            awake?.Invoke(effect, null);

            // When
            effect.ResetEffect();

            // Then - Particle system should be stopped
            Assert.IsFalse(ps.isPlaying, "Particle system should be stopped on reset.");

            // Cleanup
            Object.DestroyImmediate(effectGo);
        }

        [Test]
        public void VFXBudgetManager_BudgetCalculations_AreCorrect() {
            // Verify default budget is 10,000
            Assert.AreEqual(10000, _budgetManager.MaxParticlesBudget);

            // Test GetBudgetDensityMultiplier
            float multiplierFull = _budgetManager.GetBudgetDensityMultiplier();
            Assert.AreEqual(1.0f, multiplierFull, "Density multiplier should be 1.0 when active particles are 0.");
        }
    }
}
