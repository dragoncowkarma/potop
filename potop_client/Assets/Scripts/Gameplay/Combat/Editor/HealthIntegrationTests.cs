using NUnit.Framework;
using UnityEngine;
using Potop.Client.Gameplay.Combat;
using Potop.Client.Gameplay;
using Potop.Client.Core.Events;

namespace Potop.Client.Gameplay.Combat.Tests {
    public class HealthIntegrationTests {
        [Test]
        public void Health_TakeDamage_ReducesHealthAndPublishesEvents() {
            var go = new GameObject();
            var health = go.AddComponent<Health>();

            bool damagedCalled = false;
            bool changedCalled = false;
            bool deathCalled = false;

            health.OnDamaged += (info) => damagedCalled = true;
            health.OnHealthChanged += (current, max) => changedCalled = true;
            health.OnDeath += () => deathCalled = true;

            // Trigger OnEnable
            go.SetActive(false);
            go.SetActive(true);

            Assert.AreEqual(100, health.CurrentHealth);

            var info = new DamageInfo { Amount = 40 };
            health.TakeDamage(info);

            Assert.AreEqual(60, health.CurrentHealth);
            Assert.IsTrue(damagedCalled);
            Assert.IsTrue(changedCalled);
            Assert.IsFalse(deathCalled);

            health.TakeDamage(new DamageInfo { Amount = 60 });

            Assert.AreEqual(0, health.CurrentHealth);
            Assert.IsTrue(deathCalled);

            Object.DestroyImmediate(go);
        }

        [Test]
        public void EnemyBase_IntegratesWithHealth() {
            var go = new GameObject();
            var health = go.AddComponent<Health>();
            var bot = go.AddComponent<Potop.Client.Gameplay.AI.Variants.BlitzEnemy>(); // Use a concrete variant instead of abstract Base

            // Setup
            go.SetActive(false);
            go.SetActive(true);

            // Using the legacy int signature for damage
            bot.TakeDamage(100);

            Assert.AreEqual(0, health.CurrentHealth);
            Assert.AreEqual(0, bot.Health);

            Object.DestroyImmediate(go);
        }
    }
}
