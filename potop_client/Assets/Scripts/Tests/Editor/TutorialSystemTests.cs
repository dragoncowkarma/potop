using NUnit.Framework;
using UnityEngine;
using Potop.Client.Core.Events;
using Potop.Client.Gameplay.Tutorial;

namespace Potop.Client.Tests.EditMode {
    [TestFixture]
    public class TutorialSystemTests {
        private GameObject _tutorialGo;
        private TutorialSystem _tutorialSystem;

        [SetUp]
        public void Setup() {
            EventBroker.ClearAllSubscriptions();
            _tutorialGo = new GameObject("TutorialSystem");
            _tutorialSystem = _tutorialGo.AddComponent<TutorialSystem>();

            // Manually call OnEnable to register event subscriptions in EditMode test
            var onEnableMethod = typeof(TutorialSystem).GetMethod("OnEnable", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (onEnableMethod != null) {
                onEnableMethod.Invoke(_tutorialSystem, null);
            }

            // Manually call Start to trigger initialization in EditMode test
            var startMethod = typeof(TutorialSystem).GetMethod("Start", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (startMethod != null) {
                startMethod.Invoke(_tutorialSystem, null);
            }
        }

        [TearDown]
        public void Teardown() {
            Object.DestroyImmediate(_tutorialGo);
            EventBroker.ClearAllSubscriptions();
            Time.timeScale = 1.0f; // Restore normal time scale
        }

        [Test]
        public void InitialState_IsLook() {
            // TutorialSystem starts at Look step by default
            Assert.AreEqual(TutorialStep.Look, _tutorialSystem.CurrentStep);
            Assert.AreEqual(0f, Time.timeScale);
        }

        [Test]
        public void LookState_TransitionToShoot_OnThresholdReached() {
            TutorialStep? previousStep = null;
            TutorialStep? newStep = null;
            int transitionCount = 0;

            EventBroker.Subscribe<TutorialStepChangedEvent>(e => {
                previousStep = e.PreviousStep;
                newStep = e.NewStep;
                transitionCount++;
            });

            // Look threshold is set to 500f by default in our class.
            // Add look delta below threshold
            _tutorialSystem.AddLookDelta(200f);
            Assert.AreEqual(TutorialStep.Look, _tutorialSystem.CurrentStep);
            Assert.AreEqual(0, transitionCount);

            // Add look delta to cross threshold
            _tutorialSystem.AddLookDelta(350f);

            Assert.AreEqual(TutorialStep.Shoot, _tutorialSystem.CurrentStep);
            Assert.AreEqual(1, transitionCount);
            Assert.AreEqual(TutorialStep.Look, previousStep);
            Assert.AreEqual(TutorialStep.Shoot, newStep);
            Assert.AreEqual(1f, Time.timeScale);
        }

        [Test]
        public void ShootState_TransitionToComplete_OnEnemyDiedEvent() {
            TutorialStep? previousStep = null;
            TutorialStep? newStep = null;
            int transitionCount = 0;

            EventBroker.Subscribe<TutorialStepChangedEvent>(e => {
                previousStep = e.PreviousStep;
                newStep = e.NewStep;
                transitionCount++;
            });

            // Transition to Shoot step manually to test this phase in isolation
            _tutorialSystem.TransitionToStep(TutorialStep.Shoot);
            Assert.AreEqual(TutorialStep.Shoot, _tutorialSystem.CurrentStep);
            Assert.AreEqual(1f, Time.timeScale);

            // Publish an unrelated event should not trigger progression
            EventBroker.Publish(new ScoreChangedEvent { CurrentScore = 100 });
            Assert.AreEqual(TutorialStep.Shoot, _tutorialSystem.CurrentStep);

            // Publish EnemyDiedEvent (training enemy killed)
            EventBroker.Publish(new EnemyDiedEvent { ScoreValue = 10, EnergyReward = 5 });

            Assert.AreEqual(TutorialStep.Complete, _tutorialSystem.CurrentStep);
            Assert.AreEqual(2, transitionCount); // First transition to Shoot, then to Complete
            Assert.AreEqual(TutorialStep.Shoot, previousStep);
            Assert.AreEqual(TutorialStep.Complete, newStep);
            Assert.AreEqual(1f, Time.timeScale);
        }
    }
}
