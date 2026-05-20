using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Potop.Client.Core.Events;
using Potop.Client.Gameplay;
using Potop.Client.Gameplay.Weapons.Overdrive;

namespace Potop.Client.UI
{
    /// <summary>
    /// 시너지 활성화 및 궁극 무기 진화(Overdrive)에 대한 연출 팝업 알림을 관리하는 UI 컴포넌트입니다.
    /// GameHUD의 UIDocument 하위에 알림 컨테이너를 동적으로 참조해 활용합니다.
    /// </summary>
    public class SynergyFeedbackUI : MonoBehaviour
    {
        [Header("HUD Reference")]
        [SerializeField] private GameHUD _gameHud;

        private VisualElement _notificationContainer;
        
        // UI가 화면에 머무르는 노출 시간 상수입니다.
        private const float DISPLAY_DURATION = 2.5f;
        
        // 페이드 아웃 연출 진행 시간 상수입니다.
        private const float FADE_OUT_DURATION = 0.5f;

        private void Start()
        {
            if (_gameHud == null)
            {
                _gameHud = FindFirstObjectByType<GameHUD>();
            }

            if (_gameHud != null && _gameHud.UiDocument != null)
            {
                var root = _gameHud.UiDocument.rootVisualElement;
                _notificationContainer = root.Q<VisualElement>("notification-container");
            }

            if (_notificationContainer == null)
            {
                // UI 레이아웃의 유연성을 위해, GameHUD UXML에 컨테이너가 준비되지 않은 상태라면 코드 상에서 안전하게 fallback 생성합니다.
                if (_gameHud != null && _gameHud.UiDocument != null)
                {
                    var root = _gameHud.UiDocument.rootVisualElement;
                    _notificationContainer = new VisualElement();
                    _notificationContainer.name = "notification-container";
                    _notificationContainer.AddToClassList("notification-container");
                    root.Add(_notificationContainer);
                }
            }
        }

        private void OnEnable()
        {
            EventBroker.Subscribe<SynergyActivatedEvent>(OnSynergyActivated);
            EventBroker.Subscribe<OverdriveEvolvedEvent>(OnOverdriveEvolved);
        }

        private void OnDisable()
        {
            EventBroker.Unsubscribe<SynergyActivatedEvent>(OnSynergyActivated);
            EventBroker.Unsubscribe<OverdriveEvolvedEvent>(OnOverdriveEvolved);
        }

        private void OnSynergyActivated(SynergyActivatedEvent evt)
        {
            string title = "SYNERGY ACTIVATED!";
            string desc = GetSynergyFriendlyName(evt.Synergy);
            ShowNotification(title, desc, false);
        }

        private void OnOverdriveEvolved(OverdriveEvolvedEvent evt)
        {
            string title = "WEAPON EVOLVED!";
            string desc = $"{evt.OverdriveName}\n({GetSynergyFriendlyName(evt.RequiredSynergy)})";
            ShowNotification(title, desc, true);
        }

        /// <summary>
        /// 화면 중앙 상단에 알림 메시지를 인스턴스화하고 CSS 기반 입출 트랜지션을 연출합니다.
        /// </summary>
        private void ShowNotification(string title, string desc, bool isOverdrive)
        {
            if (_notificationContainer == null)
            {
                if (_gameHud != null && _gameHud.UiDocument != null)
                {
                    _notificationContainer = _gameHud.UiDocument.rootVisualElement.Q<VisualElement>("notification-container");
                }
                if (_notificationContainer == null) return;
            }

            var popup = new VisualElement();
            popup.AddToClassList("notification-popup");
            if (isOverdrive)
            {
                popup.AddToClassList("overdrive");
            }

            var titleLabel = new Label(title);
            titleLabel.AddToClassList("notification-title");
            popup.Add(titleLabel);

            var descLabel = new Label(desc);
            descLabel.AddToClassList("notification-desc");
            popup.Add(descLabel);

            _notificationContainer.Add(popup);

            // 레이아웃이 적용되어 Geometry가 확정된 뒤 트랜지션용 클래스(visible)를 삽입해야 페이드인 애니메이션이 정상 동작합니다.
            popup.RegisterCallback<GeometryChangedEvent>(evt => 
            {
                popup.AddToClassList("visible");
            });

            StartCoroutine(RemoveNotificationRoutine(popup));
        }

        /// <summary>
        /// 노출 시간 대기 후 서서히 사라지도록 트랜지션 클래스를 스왑하고 파괴하는 비동기 루틴입니다.
        /// </summary>
        private IEnumerator RemoveNotificationRoutine(VisualElement popup)
        {
            yield return new WaitForSecondsRealtime(DISPLAY_DURATION);

            popup.RemoveFromClassList("visible");
            popup.AddToClassList("fade-out");

            yield return new WaitForSecondsRealtime(FADE_OUT_DURATION);

            if (_notificationContainer != null && _notificationContainer.Contains(popup))
            {
                _notificationContainer.Remove(popup);
            }
        }

        /// <summary>
        /// 시너지 타입별 인게임 출력용 명칭을 매핑합니다.
        /// </summary>
        private string GetSynergyFriendlyName(SynergyType synergy)
        {
            switch (synergy)
            {
                case SynergyType.PierceExplosion: return "Pierce Explosion";
                case SynergyType.BounceHoming: return "Bounce Homing";
                case SynergyType.ScaleShockwave: return "Scale Shockwave";
                default: return synergy.ToString();
            }
        }
    }
}
