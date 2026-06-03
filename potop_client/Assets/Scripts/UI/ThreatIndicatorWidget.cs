using UnityEngine;
using UnityEngine.UIElements;
using Potop.Client.Core.Events;
using Potop.Client.Gameplay.Enemies;

namespace Potop.Client.UI {
    /// <summary>
    /// EventBroker의 ThreatUpdateEvent를 구독하여 가장 가까운 적의 위협 상태를 
    /// UI에 업데이트하고 검증 가능하도록 노출하는 위젯 컴포넌트입니다.
    /// </summary>
    public class ThreatIndicatorWidget : MonoBehaviour {
        public ThreatLevel CurrentThreatLevel { get; private set; } = ThreatLevel.None;
        public float CurrentDistance { get; private set; } = 0f;
        public float CurrentAngle { get; private set; } = 0f;
        public Vector3 CurrentDirection { get; private set; } = Vector3.zero;

        private VisualElement _indicatorElement;

        public void Initialize(VisualElement indicatorElement) {
            _indicatorElement = indicatorElement;
            UpdateVisuals();
        }

        private void OnEnable() {
            EventBroker.Subscribe<ThreatUpdateEvent>(OnThreatUpdated);
        }

        private void OnDisable() {
            EventBroker.Unsubscribe<ThreatUpdateEvent>(OnThreatUpdated);
        }

        private void OnThreatUpdated(ThreatUpdateEvent evt) {
            CurrentThreatLevel = evt.Level;
            CurrentDistance = evt.Distance;
            CurrentAngle = evt.Angle;
            CurrentDirection = evt.Direction;

            UpdateVisuals();
        }

        private void UpdateVisuals() {
            if (_indicatorElement == null) return;

            // Remove previous classes
            _indicatorElement.RemoveFromClassList("threat-yellow");
            _indicatorElement.RemoveFromClassList("threat-orange");
            _indicatorElement.RemoveFromClassList("threat-red");
            _indicatorElement.RemoveFromClassList("threat-pulse");

            switch (CurrentThreatLevel) {
                case ThreatLevel.Yellow:
                    _indicatorElement.AddToClassList("threat-yellow");
                    break;
                case ThreatLevel.Orange:
                    _indicatorElement.AddToClassList("threat-orange");
                    break;
                case ThreatLevel.Red:
                    _indicatorElement.AddToClassList("threat-red");
                    _indicatorElement.AddToClassList("threat-pulse");
                    break;
            }

            // UI Toolkit rotate
            _indicatorElement.style.rotate = new Rotate(Angle.Degrees(CurrentAngle));
        }
    }
}
