using NUnit.Framework;
using UnityEngine;
using Potop.Client.Core;
using Potop.Client.Core.Events;
using Potop.Client.Gameplay;
using Potop.Client.Gameplay.Enemies;
using Potop.Client.UI;

namespace Potop.Client.Tests.EditMode {
    [TestFixture]
    public class ThreatIndicatorTests {
        private GameObject _gmGo;
        private GameManager _gameManager;
        private GameObject _playerGo;
        private GameObject _detectorGo;
        private ThreatDetector _detector;
        private GameObject _widgetGo;
        private ThreatIndicatorWidget _widget;

        [SetUp]
        public void Setup() {
            EventBroker.ClearAllSubscriptions();
            EnemyBase.ActiveEnemies.Clear();

            // Setup GameManager
            _gmGo = new GameObject("GameManager");
            _gameManager = _gmGo.AddComponent<GameManager>();
            typeof(GameManager).GetProperty("Instance", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
                .SetValue(null, _gameManager);

            // Setup Player
            _playerGo = new GameObject("Player");
            _playerGo.transform.position = Vector3.zero;
            _playerGo.transform.rotation = Quaternion.identity; // Forward is (0, 0, 1)
            _gameManager.PlayerTransform = _playerGo.transform;

            // Setup ThreatDetector
            _detectorGo = new GameObject("ThreatDetector");
            _detector = _detectorGo.AddComponent<ThreatDetector>();

            // Setup ThreatIndicatorWidget
            _widgetGo = new GameObject("ThreatIndicatorWidget");
            _widget = _widgetGo.AddComponent<ThreatIndicatorWidget>();
            
            // Trigger OnEnable manually to subscribe
            var onEnableMethod = typeof(ThreatIndicatorWidget).GetMethod("OnEnable", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (onEnableMethod != null) {
                onEnableMethod.Invoke(_widget, null);
            }
        }

        [TearDown]
        public void Teardown() {
            var onDisableMethod = typeof(ThreatIndicatorWidget).GetMethod("OnDisable", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (onDisableMethod != null) {
                onDisableMethod.Invoke(_widget, null);
            }

            Object.DestroyImmediate(_widgetGo);
            Object.DestroyImmediate(_detectorGo);
            Object.DestroyImmediate(_playerGo);
            Object.DestroyImmediate(_gmGo);
            
            typeof(GameManager).GetProperty("Instance", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
                .SetValue(null, null);
            
            EnemyBase.ActiveEnemies.Clear();
            EventBroker.ClearAllSubscriptions();
        }

        [Test]
        public void Test_ThreatLevel_None_WhenNoEnemies() {
            // Update detector
            var updateMethod = typeof(ThreatDetector).GetMethod("Update", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (updateMethod != null) {
                updateMethod.Invoke(_detector, null);
            }

            Assert.AreEqual(ThreatLevel.None, _widget.CurrentThreatLevel);
            Assert.AreEqual(0f, _widget.CurrentDistance);
        }

        [Test]
        public void Test_ThreatLevel_Yellow_Threshold() {
            // Spawn enemy at 12m (Yellow threshold is 15m)
            var enemyGo = new GameObject("Enemy_Yellow");
            enemyGo.transform.position = new Vector3(0f, 0f, 12f);
            var enemy = enemyGo.AddComponent<DummyEnemy>();
            
            EnemyBase.ActiveEnemies.Add(enemy);

            // Update detector
            var updateMethod = typeof(ThreatDetector).GetMethod("Update", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (updateMethod != null) {
                updateMethod.Invoke(_detector, null);
            }

            Assert.AreEqual(ThreatLevel.Yellow, _widget.CurrentThreatLevel);
            Assert.AreEqual(12f, _widget.CurrentDistance);

            Object.DestroyImmediate(enemyGo);
        }

        [Test]
        public void Test_ThreatLevel_Orange_Threshold() {
            // Spawn enemy at 6m (Orange threshold is 8m)
            var enemyGo = new GameObject("Enemy_Orange");
            enemyGo.transform.position = new Vector3(0f, 0f, 6f);
            var enemy = enemyGo.AddComponent<DummyEnemy>();
            
            EnemyBase.ActiveEnemies.Add(enemy);

            // Update detector
            var updateMethod = typeof(ThreatDetector).GetMethod("Update", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (updateMethod != null) {
                updateMethod.Invoke(_detector, null);
            }

            Assert.AreEqual(ThreatLevel.Orange, _widget.CurrentThreatLevel);
            Assert.AreEqual(6f, _widget.CurrentDistance);

            Object.DestroyImmediate(enemyGo);
        }

        [Test]
        public void Test_ThreatLevel_Red_Threshold() {
            // Spawn enemy at 2m (Red threshold is 3m)
            var enemyGo = new GameObject("Enemy_Red");
            enemyGo.transform.position = new Vector3(0f, 0f, 2f);
            var enemy = enemyGo.AddComponent<DummyEnemy>();
            
            EnemyBase.ActiveEnemies.Add(enemy);

            // Update detector
            var updateMethod = typeof(ThreatDetector).GetMethod("Update", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (updateMethod != null) {
                updateMethod.Invoke(_detector, null);
            }

            Assert.AreEqual(ThreatLevel.Red, _widget.CurrentThreatLevel);
            Assert.AreEqual(2f, _widget.CurrentDistance);

            Object.DestroyImmediate(enemyGo);
        }

        [Test]
        public void Test_ThreatAngleCalculation() {
            // Spawn enemy at angle of 90 degrees relative to player (right side)
            var enemyGo = new GameObject("Enemy_Right");
            enemyGo.transform.position = new Vector3(5f, 0f, 0f); // Player is at (0,0,0) looking at (0,0,1)
            var enemy = enemyGo.AddComponent<DummyEnemy>();
            
            EnemyBase.ActiveEnemies.Add(enemy);

            // Update detector
            var updateMethod = typeof(ThreatDetector).GetMethod("Update", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (updateMethod != null) {
                updateMethod.Invoke(_detector, null);
            }

            // Vector3.SignedAngle((0,0,1), (1,0,0), Vector3.up) should be 90 degrees
            Assert.AreEqual(90f, _widget.CurrentAngle, 0.01f);
            Assert.AreEqual(new Vector3(1f, 0f, 0f), _widget.CurrentDirection);

            Object.DestroyImmediate(enemyGo);
        }

        // Helper Dummy class inheriting from EnemyBase
        private class DummyEnemy : EnemyBase {
            protected override void Awake() {
                // Bypass regular initialization for tests
            }
            protected override void OnEnable() {
                // Do not register automatically in test to control manually
            }
            protected override void OnDisable() {
                // Do not unregister automatically
            }
        }
    }
}
