using Potop.Client.Core.Events;
using Potop.Client.Gameplay.Combat;
using UnityEngine;
using UnityEngine.UIElements;

namespace Potop.Client.UI {
    /// <summary>
    /// 오버차지 게이지와 상태를 표시하고 관리하는 위젯입니다.
    /// </summary>
    public class OverchargeWidget : MonoBehaviour {
        private VisualElement _overchargeContainer;
        private VisualElement _overchargeBarFill;

        public void Initialize(VisualElement overchargeContainer, VisualElement overchargeBarFill) {
            _overchargeContainer = overchargeContainer;
            _overchargeBarFill = overchargeBarFill;
            UpdateOvercharge(0f, 100f, OverchargeState.Idle);
        }

        private void OnEnable() {
            EventBroker.Subscribe<OverchargeChangedEvent>(OnOverchargeChanged);
        }

        private void OnDisable() {
            EventBroker.Unsubscribe<OverchargeChangedEvent>(OnOverchargeChanged);
        }

        private void OnOverchargeChanged(OverchargeChangedEvent evt) {
            UpdateOvercharge(evt.CurrentGauge, evt.MaxGauge, evt.State);
        }

        private void UpdateOvercharge(float gauge, float maxGauge, OverchargeState state) {
            if (_overchargeContainer != null) {
                float pct = (maxGauge > 0f) ? (gauge / maxGauge) * 100f : 0f;
                if (_overchargeBarFill != null) {
                    _overchargeBarFill.style.width = new Length(Mathf.Clamp(pct, 0f, 100f), LengthUnit.Percent);
                }

                if (gauge > 0f) {
                    _overchargeContainer.AddToClassList("visible");
                } else {
                    _overchargeContainer.RemoveFromClassList("visible");
                }

                _overchargeContainer.RemoveFromClassList("state-idle");
                _overchargeContainer.RemoveFromClassList("state-active");
                _overchargeContainer.RemoveFromClassList("state-overheat");

                if (state == OverchargeState.Idle) {
                    _overchargeContainer.AddToClassList("state-idle");
                } else if (state == OverchargeState.Active) {
                    _overchargeContainer.AddToClassList("state-active");
                } else if (state == OverchargeState.Overheat) {
                    _overchargeContainer.AddToClassList("state-overheat");
                }
            }
        }
    }
}
