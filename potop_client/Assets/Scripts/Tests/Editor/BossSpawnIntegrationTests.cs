using NUnit.Framework;
using UnityEngine;
using System.Reflection;
using System.Collections.Generic;
using Potop.Client.Core;
using Potop.Client.Core.Events;
using Potop.Client.Gameplay;
using Potop.Client.Gameplay.Flow;
using Potop.Client.Gameplay.Wave;
using Potop.Client.Gameplay.Weapons;
using Potop.Client.Gameplay.Weapons.Strategies;

namespace Potop.Client.Tests.EditMode {
    public class BossSpawnIntegrationTests {
        private GameObject _managerGo;
        private GameObject _playerGo;
        private GameObject _flowGo;
        private GameManager _gameManager;
        private GameFlowController _flowController;
        private WaveManager _waveManager;
        private OverclockMode _overclockMode;

        private GameObject _bossPrefab;
        private GameObject _bulletPrefab;

        [SetUp]
        public void Setup() {
            EventBroker.ClearAllSubscriptions();

            _flowGo = new GameObject("GameFlowController");
            _flowController = _flowGo.AddComponent<GameFlowController>();
            typeof(GameFlowController).GetMethod("Awake", BindingFlags.Instance | BindingFlags.NonPublic)
                ?.Invoke(_flowController, null);

            _managerGo = new GameObject("GameManager");
            _gameManager = _managerGo.AddComponent<GameManager>();
            typeof(GameManager).GetMethod("Awake", BindingFlags.Instance | BindingFlags.NonPublic)
                ?.Invoke(_gameManager, null);

            _playerGo = new GameObject("Player");
            _playerGo.transform.position = Vector3.zero;
            _playerGo.transform.rotation = Quaternion.identity;
            _gameManager.PlayerTransform = _playerGo.transform;

            _waveManager = _managerGo.AddComponent<WaveManager>();
            _overclockMode = _managerGo.AddComponent<OverclockMode>();
            
            // Set WaveManager in OverclockMode
            var waveManagerField = typeof(OverclockMode).GetField("_waveManager", BindingFlags.Instance | BindingFlags.NonPublic);
            waveManagerField.SetValue(_overclockMode, _waveManager);

            typeof(OverclockMode).GetMethod("Awake", BindingFlags.Instance | BindingFlags.NonPublic)
                ?.Invoke(_overclockMode, null);
            typeof(OverclockMode).GetMethod("OnEnable", BindingFlags.Instance | BindingFlags.NonPublic)
                ?.Invoke(_overclockMode, null);

            // Create simple mock prefabs
            _bossPrefab = new GameObject("BossPrefab");
            _bossPrefab.AddComponent<Rigidbody>();
            _bossPrefab.SetActive(false);

            _bulletPrefab = new GameObject("BulletPrefab");
            _bulletPrefab.AddComponent<Projectile>();
            _bulletPrefab.AddComponent<Rigidbody>();
            _bulletPrefab.SetActive(false);
        }

        [TearDown]
        public void Teardown() {
            typeof(OverclockMode).GetMethod("OnDisable", BindingFlags.Instance | BindingFlags.NonPublic)
                ?.Invoke(_overclockMode, null);

            // Destroy all instantiated objects in the scene to avoid leakage
            var projectiles = Object.FindObjectsByType<Projectile>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            foreach (var p in projectiles) {
                Object.DestroyImmediate(p.gameObject);
            }
            
            var allGos = Object.FindObjectsByType<GameObject>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            foreach (var go in allGos) {
                if (go.name.Contains("(Clone)") || go.name.Contains("BossPrefab") || go.name.Contains("BulletPrefab")) {
                    if (go != _bossPrefab && go != _bulletPrefab) {
                        Object.DestroyImmediate(go);
                    }
                }
            }

            Object.DestroyImmediate(_managerGo);
            Object.DestroyImmediate(_playerGo);
            Object.DestroyImmediate(_flowGo);
            Object.DestroyImmediate(_bossPrefab);
            Object.DestroyImmediate(_bulletPrefab);

            // Clean up singletons
            typeof(GameManager).GetProperty("Instance", BindingFlags.Static | BindingFlags.Public)
                ?.GetSetMethod(true)?.Invoke(null, new object[] { null });
            typeof(GameFlowController).GetProperty("Instance", BindingFlags.Static | BindingFlags.Public)
                ?.GetSetMethod(true)?.Invoke(null, new object[] { null });
            typeof(OverclockMode).GetProperty("Instance", BindingFlags.Static | BindingFlags.Public)
                ?.GetSetMethod(true)?.Invoke(null, new object[] { null });

            CoreFlowBridge.GetCurrentState = null;
            CoreFlowBridge.TransitionTo = null;
            EventBroker.ClearAllSubscriptions();
        }

        [Test]
        public void WaveManager_BossSpawnsAfter900Seconds() {
            // Setup waves
            var wavesField = typeof(WaveManager).GetField("_waves", BindingFlags.Instance | BindingFlags.NonPublic);
            var waveList = new List<WaveData>();
            var waveData = ScriptableObject.CreateInstance<WaveData>();
            typeof(WaveData).GetField("_duration", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(waveData, 100f);
            waveList.Add(waveData);
            wavesField.SetValue(_waveManager, waveList);

            // Setup boss prefab
            var bossPrefabField = typeof(WaveManager).GetField("_bossPrefab", BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.IsNotNull(bossPrefabField, "WaveManager should have a _bossPrefab field.");
            bossPrefabField.SetValue(_waveManager, _bossPrefab);

            // Verify BossSpawnedEvent published
            bool bossSpawnedEventFired = false;
            EventBroker.Subscribe<BossSpawnedEvent>(e => {
                bossSpawnedEventFired = true;
            });

            // Run Start
            typeof(WaveManager).GetMethod("Start", BindingFlags.Instance | BindingFlags.NonPublic)
                ?.Invoke(_waveManager, null);

            // Fast-forward time to 900+ seconds
            var timerField = typeof(WaveManager).GetField("_gameplayTimer", BindingFlags.Instance | BindingFlags.NonPublic);
            if (timerField == null) {
                timerField = typeof(WaveManager).GetField("_totalTime", BindingFlags.Instance | BindingFlags.NonPublic);
            }
            Assert.IsNotNull(timerField, "WaveManager should track gameplay time in a float field.");
            timerField.SetValue(_waveManager, 900f);

            // Trigger Update to process boss spawn
            var updateMethod = typeof(WaveManager).GetMethod("Update", BindingFlags.Instance | BindingFlags.NonPublic);
            updateMethod?.Invoke(_waveManager, null);

            Assert.IsTrue(bossSpawnedEventFired, "BossSpawnedEvent should be fired when gameplay timer reaches 900 seconds.");
            Assert.AreEqual(GameFlowState.BossBattle, _flowController.CurrentState, "GameFlowState should transition to BossBattle.");
        }

        [Test]
        public void WaveManager_EnterContinuousMode_ResetsDelaysAndAllowsInfiniteLoop() {
            _waveManager.EnterContinuousMode();

            var defaultDelayField = typeof(WaveManager).GetField("_defaultWaveDelay", BindingFlags.Instance | BindingFlags.NonPublic);
            var delayTimerField = typeof(WaveManager).GetField("_delayTimer", BindingFlags.Instance | BindingFlags.NonPublic);

            Assert.AreEqual(0f, (float)defaultDelayField.GetValue(_waveManager));
            Assert.AreEqual(0f, (float)delayTimerField.GetValue(_waveManager));
        }

        [Test]
        public void OverclockMode_TriggersContinuousModeOnBossDefeated() {
            // Manually register event or trigger boss defeat
            EventBroker.Publish(new BossDefeatedEvent());

            var defaultDelayField = typeof(WaveManager).GetField("_defaultWaveDelay", BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.AreEqual(0f, (float)defaultDelayField.GetValue(_waveManager));
        }

        [Test]
        public void SpreadFireStrategy_SpawnsMultipleProjectilesSymmetrically() {
            var weaponGo = new GameObject("Weapon");
            var weapon = weaponGo.AddComponent<MockWeapon>();
            var weaponData = ScriptableObject.CreateInstance<WeaponData>();
            
            var spreadAngleField = typeof(WeaponData).GetField("SpreadAngle");
            var spreadCountField = typeof(WeaponData).GetField("SpreadProjectileCount");
            
            Assert.IsNotNull(spreadAngleField, "WeaponData should have a SpreadAngle field.");
            Assert.IsNotNull(spreadCountField, "WeaponData should have a SpreadProjectileCount field.");
            
            spreadAngleField.SetValue(weaponData, 15f);
            spreadCountField.SetValue(weaponData, 3);
            
            // Setup project prefab
            var projField = typeof(WeaponData).GetField("_projectilePrefab", BindingFlags.Instance | BindingFlags.NonPublic);
            projField.SetValue(weaponData, _bulletPrefab);

            weapon.SetWeaponData(weaponData);

            var strategy = new SpreadFireStrategy();
            strategy.ExecuteFire(weapon);

            // Filter out the prefab from the counting
            var projectiles = Object.FindObjectsByType<Projectile>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            int cloneCount = 0;
            foreach (var p in projectiles) {
                if (p.gameObject != _bulletPrefab) {
                    cloneCount++;
                }
            }
            Assert.AreEqual(3, cloneCount, "Should spawn exactly 3 projectiles.");

            Object.DestroyImmediate(weaponGo);
            Object.DestroyImmediate(weaponData);
        }

        [Test]
        public void LobFireStrategy_AppliesParabolicVelocityWithGravity() {
            var weaponGo = new GameObject("Weapon");
            var weapon = weaponGo.AddComponent<MockWeapon>();
            var weaponData = ScriptableObject.CreateInstance<WeaponData>();
            
            var launchAngleField = typeof(WeaponData).GetField("LaunchAngle");
            Assert.IsNotNull(launchAngleField, "WeaponData should have a LaunchAngle field.");
            launchAngleField.SetValue(weaponData, 45f);

            var projField = typeof(WeaponData).GetField("_projectilePrefab", BindingFlags.Instance | BindingFlags.NonPublic);
            projField.SetValue(weaponData, _bulletPrefab);

            weapon.SetWeaponData(weaponData);

            var strategy = new LobFireStrategy();
            strategy.ExecuteFire(weapon);

            var projectiles = Object.FindObjectsByType<Projectile>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            Projectile clone = null;
            foreach (var p in projectiles) {
                if (p.gameObject != _bulletPrefab) {
                    clone = p;
                    break;
                }
            }
            Assert.IsNotNull(clone);

            var rb = clone.GetComponent<Rigidbody>();
            Assert.IsTrue(rb.useGravity);
            Assert.IsFalse(rb.isKinematic);
            Assert.Greater(rb.linearVelocity.y, 0f, "Vertical velocity should be positive due to launch angle.");

            Object.DestroyImmediate(weaponGo);
            Object.DestroyImmediate(weaponData);
        }
    }

    public class MockWeapon : WeaponBase {
        public void SetWeaponData(WeaponData data) {
            _weaponData = data;
            _firePoint = transform;
        }
    }
}
