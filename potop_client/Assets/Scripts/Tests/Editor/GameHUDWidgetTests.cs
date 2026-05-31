using NUnit.Framework;
using Potop.Client.Core.Events;
using Potop.Client.UI;
using UnityEngine;
using UnityEngine.UIElements;

namespace Potop.Client.Tests.EditMode {
    public class GameHUDWidgetTests {
        private GameObject _go;

        [SetUp]
        public void Setup() {
            _go = new GameObject();
            EventBroker.ClearAllSubscriptions();
        }

        [TearDown]
        public void Teardown() {
            Object.DestroyImmediate(_go);
            EventBroker.ClearAllSubscriptions();
        }

        [Test]
        public void ScoreWidget_OnScoreChangedEvent_UpdatesLabelText() {
            var widget = _go.AddComponent<ScoreWidget>();
            var label = new Label();
            widget.Initialize(label);

            Assert.AreEqual("SCORE: 0", label.text);

            EventBroker.Publish(new ScoreChangedEvent { CurrentScore = 150 });

            Assert.AreEqual("SCORE: 150", label.text);
        }

        [Test]
        public void HealthBarWidget_OnPlayerHealthChangedEvent_UpdatesLabelText() {
            var widget = _go.AddComponent<HealthBarWidget>();
            var label = new Label();
            widget.Initialize(label, null);

            EventBroker.Publish(new PlayerHealthChangedEvent { CurrentHealth = 80, MaxHealth = 120 });

            Assert.AreEqual("80 / 120", label.text);
        }

        [Test]
        public void FeverGaugeWidget_OnFeverChangedEvent_UpdatesProgressAndStyle() {
            var widget = _go.AddComponent<FeverGaugeWidget>();
            var barFill = new VisualElement();
            widget.Initialize(barFill);

            // Initially progress should be 0, not active
            Assert.AreEqual(0f, barFill.style.width.value.value);

            // Active fever progress 75%
            EventBroker.Publish(new FeverChangedEvent { Progress = 0.75f, IsFeverActive = true, Level = 1 });

            Assert.AreEqual(75f, barFill.style.width.value.value);
            Assert.IsTrue(barFill.ClassListContains("fever-active"));

            // Non-active fever progress 20%
            EventBroker.Publish(new FeverChangedEvent { Progress = 0.20f, IsFeverActive = false, Level = 0 });

            Assert.AreEqual(20f, barFill.style.width.value.value);
            Assert.IsFalse(barFill.ClassListContains("fever-active"));
        }
    }
}
