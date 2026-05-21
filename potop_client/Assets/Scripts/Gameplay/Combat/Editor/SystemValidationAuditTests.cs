using NUnit.Framework;
using UnityEngine;
using Potop.Client.Gameplay.Combat;
using Potop.Client.Gameplay;
using Potop.Client.Gameplay.VFX;
using Potop.Client.Gameplay.SFX;
using Potop.Client.Core.Pooling;
using System.Collections;
using UnityEngine.TestTools;

namespace Potop.Client.Gameplay.Combat.Tests {
    /// <summary>
    /// Phase 2.5 시스템 통합 검증을 위한 테스트 클래스입니다.
    /// 전투 피드백 루프(데미지 -> 피격 플래시 -> 사운드) 및 풀링 시스템의 정합성을 검증합니다.
    /// </summary>
    public class SystemValidationAuditTests {
        private GameObject _enemyGo;
        private Health _health;
        private HitFlash _hitFlash;
        private VFXTrigger _vfxTrigger;
        private SFXTrigger _sfxTrigger;
        private AudioSource _audioSource;

        [SetUp]
        public void SetUp() {
            _enemyGo = new GameObject("TestEnemy");
            _enemyGo.tag = "Enemy";
            
            // Add components
            _health = _enemyGo.AddComponent<Health>();
            _health.InitializeHealth(100);
            
            _hitFlash = _enemyGo.AddComponent<HitFlash>();
            _vfxTrigger = _enemyGo.AddComponent<VFXTrigger>();
            _audioSource = _enemyGo.AddComponent<AudioSource>();
            _sfxTrigger = _enemyGo.AddComponent<SFXTrigger>();

            // Setup a child with renderer for HitFlash
            GameObject visual = new GameObject("Visual");
            visual.transform.SetParent(_enemyGo.transform);
            visual.AddComponent<MeshRenderer>();
            
            // Initialize
            _enemyGo.SetActive(false);
            _enemyGo.SetActive(true);
        }

        [TearDown]
        public void TearDown() {
            Object.DestroyImmediate(_enemyGo);
        }

        [Test]
        public void CombatFeedbackLoop_Verification() {
            // Given
            int initialHealth = _health.CurrentHealth;
            var damageInfo = new DamageInfo {
                Amount = 20,
                HitPoint = Vector3.zero,
                HitNormal = Vector3.up
            };

            bool damagedEventCalled = false;
            _health.OnDamaged += (info) => damagedEventCalled = true;

            // When
            _health.TakeDamage(damageInfo);

            // Then
            Assert.AreEqual(initialHealth - 20, _health.CurrentHealth, "Health should decrease by damage amount.");
            Assert.IsTrue(damagedEventCalled, "OnDamaged event should be triggered.");
            
            // Note: HitFlash, VFXTrigger, SFXTrigger are verified via their side effects in play mode.
            // In EditMode tests, we verify they are properly attached and initialized.
            Assert.IsNotNull(_enemyGo.GetComponent<HitFlash>(), "HitFlash should be attached.");
            Assert.IsNotNull(_enemyGo.GetComponent<VFXTrigger>(), "VFXTrigger should be attached.");
            Assert.IsNotNull(_enemyGo.GetComponent<SFXTrigger>(), "SFXTrigger should be attached.");
        }

        [Test]
        public void PoolManager_Existence_Check() {
            // PoolManager should be accessible via Instance (requires being in a scene or created)
            var poolGo = new GameObject("PoolManager");
            var manager = poolGo.AddComponent<PoolManager>();
            
            // Invoke Awake to assign PoolManager.Instance singleton in EditMode test
            var awake = typeof(PoolManager).GetMethod("Awake", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            awake?.Invoke(manager, null);
            
            Assert.IsNotNull(PoolManager.Instance, "PoolManager Instance should be valid.");
            
            Object.DestroyImmediate(poolGo);
        }
    }
}

