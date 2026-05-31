using System.Collections;
using Potop.Client.Core;
using Potop.Client.Core.Events;
using Potop.Client.Data.Items;
using Potop.Client.Gameplay.Items;
using UnityEngine;
using UnityEngine.UIElements;

namespace Potop.Client.UI {
    /// <summary>
    /// 플레이어 HP와 관련 플래시 효과를 관리하는 위젯입니다.
    /// </summary>
    public class HealthBarWidget : MonoBehaviour {
        private Label _healthLabel;
        private VisualElement _fullscreenFlashOverlay;

        private const string HP_SEPARATOR = " / ";

        public void Initialize(Label healthLabel, VisualElement fullscreenFlashOverlay) {
            _healthLabel = healthLabel;
            _fullscreenFlashOverlay = fullscreenFlashOverlay;

            if (PlayerHealthController.Instance != null) {
                UpdateHP(PlayerHealthController.Instance.Health, PlayerHealthController.Instance.MaxHealth);
            } else {
                UpdateHP(100, 100);
            }
        }

        private void OnEnable() {
            EventBroker.Subscribe<PlayerHealthChangedEvent>(OnPlayerHealthChanged);
            EventBroker.Subscribe<ItemCollectedEvent>(OnItemCollected);
        }

        private void OnDisable() {
            EventBroker.Unsubscribe<PlayerHealthChangedEvent>(OnPlayerHealthChanged);
            EventBroker.Unsubscribe<ItemCollectedEvent>(OnItemCollected);
        }

        private void OnPlayerHealthChanged(PlayerHealthChangedEvent evt) {
            UpdateHP(evt.CurrentHealth, evt.MaxHealth);
        }

        private void OnItemCollected(ItemCollectedEvent evt) {
            if (evt.ItemData == null) {
                return;
            }

            switch (evt.ItemData.Type) {
                case ItemDropType.RepairKit:
                    TriggerHealthFlash();
                    break;
                case ItemDropType.Magnet:
                    TriggerMagnetFlash();
                    break;
                case ItemDropType.SmartBomb:
                    TriggerSmartBombFlash();
                    break;
            }
        }

        private void UpdateHP(int current, int max) {
            if (_healthLabel != null) {
                _healthLabel.text = $"{current}{HP_SEPARATOR}{max}";
            }
        }

        private void TriggerHealthFlash() {
            if (_healthLabel != null) {
                _healthLabel.AddToClassList("health-flash");
                StartCoroutine(RemoveClassAfterDelay(_healthLabel, "health-flash", 0.5f));
            }
        }

        private void TriggerMagnetFlash() {
            if (_fullscreenFlashOverlay != null) {
                _fullscreenFlashOverlay.RemoveFromClassList("bomb-flash");
                _fullscreenFlashOverlay.AddToClassList("magnet-active");
                StartCoroutine(RemoveClassAfterDelay(_fullscreenFlashOverlay, "magnet-active", 1.0f));
            }
        }

        private void TriggerSmartBombFlash() {
            if (_fullscreenFlashOverlay != null) {
                _fullscreenFlashOverlay.RemoveFromClassList("magnet-active");
                _fullscreenFlashOverlay.AddToClassList("bomb-flash");
                StartCoroutine(RemoveClassAfterDelay(_fullscreenFlashOverlay, "bomb-flash", 0.8f));
            }
        }

        private IEnumerator RemoveClassAfterDelay(VisualElement element, string className, float delay) {
            yield return new WaitForSeconds(delay);
            if (element != null) {
                element.RemoveFromClassList(className);
            }
        }
    }
}
