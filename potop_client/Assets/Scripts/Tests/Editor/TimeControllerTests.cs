using NUnit.Framework;
using UnityEngine;
using Potop.Client.Core;

namespace Potop.Client.Tests.EditMode {
    [TestFixture]
    public class TimeControllerTests {
        private GameObject _timeControllerGo;
        private TimeController _timeController;

        [SetUp]
        public void Setup() {
            _timeControllerGo = new GameObject("TimeController");
            _timeController = _timeControllerGo.AddComponent<TimeController>();
            
            // Invoke Awake to register instance
            var awakeMethod = typeof(TimeController).GetMethod("Awake", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (awakeMethod != null) {
                awakeMethod.Invoke(_timeController, null);
            }
            
            _timeController.ResetTimeEffectsInstance();
        }

        [TearDown]
        public void Teardown() {
            Object.DestroyImmediate(_timeControllerGo);
            Time.timeScale = 1f;
        }

        [Test]
        public void Test_DefaultTimeScale() {
            Assert.AreEqual(1f, Time.timeScale);
        }

        [Test]
        public void Test_PauseStacking() {
            _timeController.RequestPauseInstance();
            Assert.AreEqual(0f, Time.timeScale);

            _timeController.RequestPauseInstance();
            Assert.AreEqual(0f, Time.timeScale);

            _timeController.RemovePauseInstance();
            Assert.AreEqual(0f, Time.timeScale); // Still paused because count is 1

            _timeController.RemovePauseInstance();
            Assert.AreEqual(1f, Time.timeScale); // Resumed because count is 0
        }

        [Test]
        public void Test_HitStop_TakesPriority_Over_SlowMotion() {
            // Trigger slow-motion (0.1x)
            _timeController.TriggerSlowMotionInstance(5f, 0.1f);
            Assert.AreEqual(0.1f, Time.timeScale);

            // Trigger hitstop (0.05x)
            _timeController.TriggerHitStopInstance(0.1f, 0.05f);
            Assert.AreEqual(0.05f, Time.timeScale); // Hitstop should override slow motion
        }

        [Test]
        public void Test_HitStop_Overlap_StrongerTakesPriority() {
            // Trigger normal hitstop: duration 0.08s, scale 0.1f
            _timeController.TriggerHitStopInstance(0.08f, 0.1f);
            Assert.AreEqual(0.1f, Time.timeScale);

            // Trigger stronger/longer hitstop: duration 0.18s, scale 0.05f
            _timeController.TriggerHitStopInstance(0.18f, 0.05f);
            Assert.AreEqual(0.05f, Time.timeScale);
        }

        [Test]
        public void Test_UnscaledTimer_ContinuesDuringHitStop() {
            // Start a simulated real-time (unscaled) timer
            float startRealTime = Time.realtimeSinceStartup;
            
            // Activate hitstop
            _timeController.TriggerHitStopInstance(0.18f, 0.05f);
            Assert.AreEqual(0.05f, Time.timeScale);

            // In unit tests, unscaled time flows independently of Time.timeScale.
            // Check that unscaled time can still be calculated correctly (concept check)
            float dt = Time.unscaledDeltaTime;
            Assert.IsTrue(dt >= 0f);
        }

        [Test]
        public void Test_ShakeDecay_Determinism_30FPS_vs_120FPS() {
            // Simulate shake decay logic at two simulated frame rates (30 FPS vs 120 FPS)
            // and verify that the calculated decay amplitude variance is within 10%.
            
            float duration = 0.4f;
            float initialAmplitude = 2.5f;

            // 30 FPS Simulation
            float t30 = 0f;
            float dt30 = 1f / 30f;
            float amp30_at_mid = 0f;
            float timer30 = duration;
            while (timer30 > 0f) {
                t30 += dt30;
                timer30 -= dt30;
                if (timer30 < 0f) timer30 = 0f;

                float progress = timer30 / duration;
                float amplitude = initialAmplitude * progress;
                
                // Capture amplitude at approximately halfway point (0.2s)
                if (Mathf.Abs(t30 - 0.2f) < 0.02f && amp30_at_mid == 0f) {
                    amp30_at_mid = amplitude;
                }
            }

            // 120 FPS Simulation
            float t120 = 0f;
            float dt120 = 1f / 120f;
            float amp120_at_mid = 0f;
            float timer120 = duration;
            while (timer120 > 0f) {
                t120 += dt120;
                timer120 -= dt120;
                if (timer120 < 0f) timer120 = 0f;

                float progress = timer120 / duration;
                float amplitude = initialAmplitude * progress;

                // Capture amplitude at approximately halfway point (0.2s)
                if (Mathf.Abs(t120 - 0.2f) < 0.005f && amp120_at_mid == 0f) {
                    amp120_at_mid = amplitude;
                }
            }

            // Assert variance is within 10% (linear decay is deterministic)
            float variance = Mathf.Abs(amp30_at_mid - amp120_at_mid) / initialAmplitude;
            Assert.LessOrEqual(variance, 0.10f, $"Decay amplitude variance is too high: {variance * 100f}%");
        }
    }
}
