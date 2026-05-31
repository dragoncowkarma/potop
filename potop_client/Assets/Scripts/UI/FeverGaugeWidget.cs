using Potop.Client.Core.Events;
using UnityEngine;
using UnityEngine.UIElements;

namespace Potop.Client.UI {
    /// <summary>
    /// 피버 게이지와 상태를 표시하고 관리하는 위젯입니다.
    /// </summary>
    public class FeverGaugeWidget : MonoBehaviour {
        private VisualElement _feverBarFill;

        public void Initialize(VisualElement feverBarFill) {
            _feverBarFill = feverBarFill;
            UpdateFever(0f, false);
        }

        private void OnEnable() {
            EventBroker.Subscribe<FeverChangedEvent>(OnFeverChanged);
        }

        private void OnDisable() {
            EventBroker.Unsubscribe<FeverChangedEvent>(OnFeverChanged);
        }

        private void OnFeverChanged(FeverChangedEvent evt) {
            UpdateFever(evt.Progress, evt.IsFeverActive);
        }

        private void UpdateFever(float progress, bool isActive) {
            if (_feverBarFill != null) {
                _feverBarFill.style.width = new Length(progress * 100f, LengthUnit.Percent);

                if (isActive) {
                    _feverBarFill.AddToClassList("fever-active");
                } else {
                    _feverBarFill.RemoveFromClassList("fever-active");
                }
            }
        }
    }
}
