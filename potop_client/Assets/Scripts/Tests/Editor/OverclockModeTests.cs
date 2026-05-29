using NUnit.Framework;
using UnityEngine;
using System.Reflection;
using Potop.Client.Data;
using Potop.Client.Gameplay.Flow;
using Potop.Client.Core.Events;

namespace Potop.Client.Tests.EditMode {
    /// <summary>
    /// OverclockMode의 스케일링 계산 간격 및 수치 정밀도, 오버플로우 방지 로직을 검증하는 단위 테스트 클래스입니다.
    /// </summary>
    public class OverclockModeTests {
        private GameObject _overclockGo;
        private OverclockMode _overclockMode;
        private OverclockData _data;

        [SetUp]
        public void Setup() {
            _overclockGo = new GameObject("OverclockManager");
            _overclockMode = _overclockGo.AddComponent<OverclockMode>();

            _data = ScriptableObject.CreateInstance<OverclockData>();

            // Reflection to set fields in ScriptableObject
            typeof(OverclockData).GetField("_scalingInterval", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(_data, 30f);
            typeof(OverclockData).GetField("_hpScalingFactor", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(_data, 1.5f);
            typeof(OverclockData).GetField("_speedScalingFactor", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(_data, 1.5f);
            typeof(OverclockData).GetField("_damageScalingFactor", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(_data, 1.5f);
            typeof(OverclockData).GetField("_baseHpMultiplier", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(_data, 1.0f);
            typeof(OverclockData).GetField("_baseSpeedMultiplier", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(_data, 1.0f);
            typeof(OverclockData).GetField("_baseDamageMultiplier", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(_data, 1.0f);
            typeof(OverclockData).GetField("_overclockSpawnInterval", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(_data, 1.0f);

            // Set data reference on OverclockMode
            typeof(OverclockMode).GetField("_overclockData", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(_overclockMode, _data);

            _overclockGo.SetActive(true);
            EventBroker.ClearAllSubscriptions();
        }

        [TearDown]
        public void Teardown() {
            Object.DestroyImmediate(_overclockGo);
            Object.DestroyImmediate(_data);
            EventBroker.ClearAllSubscriptions();
        }

        [Test]
        public void StartOverclock_InitializesMultipliersCorrectly() {
            _overclockMode.StartOverclock();

            Assert.IsTrue(_overclockMode.IsActive);
            Assert.AreEqual(1.0f, _overclockMode.CurrentHpMultiplier);
            Assert.AreEqual(1.0f, _overclockMode.CurrentSpeedMultiplier);
            Assert.AreEqual(1.0f, _overclockMode.CurrentDamageMultiplier);
        }

        [Test]
        public void Update_ScalesStatsExactlyEvery30Seconds() {
            _overclockMode.StartOverclock();

            var timerField = typeof(OverclockMode).GetField("_timer", BindingFlags.NonPublic | BindingFlags.Instance);
            var updateMethod = typeof(OverclockMode).GetMethod("Update", BindingFlags.NonPublic | BindingFlags.Instance);

            // 1. Tick 29.9s -> Should NOT scale
            timerField.SetValue(_overclockMode, 29.9f);
            updateMethod.Invoke(_overclockMode, null);

            Assert.AreEqual(1.0f, _overclockMode.CurrentHpMultiplier);

            // 2. Tick to 30.0s (timer set to 29.9 + delta 0.1)
            // Simulating update delta by setting timer directly and calling Update with deltaTime handled internally.
            // Since Unity's Time.deltaTime is 0 in EditMode, we simulate timer overflow directly.
            timerField.SetValue(_overclockMode, 30.0f);
            updateMethod.Invoke(_overclockMode, null);

            Assert.AreEqual(1.5f, _overclockMode.CurrentHpMultiplier);

            // 3. Tick to 59.9s total (timer reset to 0, setting to 29.9s again) -> Should NOT scale
            timerField.SetValue(_overclockMode, 29.9f);
            updateMethod.Invoke(_overclockMode, null);

            Assert.AreEqual(1.5f, _overclockMode.CurrentHpMultiplier);

            // 4. Tick to 60s total (timer set to 30s) -> Should scale to 1.5 * 1.5 = 2.25
            timerField.SetValue(_overclockMode, 30.0f);
            updateMethod.Invoke(_overclockMode, null);

            Assert.AreEqual(2.25f, _overclockMode.CurrentHpMultiplier);
        }

        [Test]
        public void ScaleStats_PreventsPrecisionDriftAndFloatOverflow() {
            _overclockMode.StartOverclock();

            var scaleMethod = typeof(OverclockMode).GetMethod("ScaleStats", BindingFlags.NonPublic | BindingFlags.Instance);

            // Simulate 5 iterations -> 1.5^5 = 7.59375
            for (int i = 0; i < 5; i++) {
                scaleMethod.Invoke(_overclockMode, null);
            }

            Assert.AreEqual(7.59375f, _overclockMode.CurrentHpMultiplier);

            // Simulate 1000 iterations to check for precision drift and overflow clamping
            for (int i = 0; i < 1000; i++) {
                scaleMethod.Invoke(_overclockMode, null);
            }

            // Must be clamped to the overflow limit, not Infinity or NaN
            Assert.IsFalse(float.IsInfinity(_overclockMode.CurrentHpMultiplier));
            Assert.IsFalse(float.IsNaN(_overclockMode.CurrentHpMultiplier));
            Assert.AreEqual(1e9f, _overclockMode.CurrentHpMultiplier);
            Assert.AreEqual(1e9f, _overclockMode.CurrentSpeedMultiplier);
            Assert.AreEqual(1e9f, _overclockMode.CurrentDamageMultiplier);
        }
    }
}
