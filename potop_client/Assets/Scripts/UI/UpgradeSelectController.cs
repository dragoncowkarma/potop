using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Potop.Client.Core.Events;
using Potop.Client.Gameplay.Progression;

namespace Potop.Client.UI
{
    /// <summary>
    /// 업그레이드 선택 UI를 관리하는 컨트롤러입니다.
    /// </summary>
    public class UpgradeSelectController : MonoBehaviour
    {
        [Header("Manager References")]
        [SerializeField] private LevelingManager _levelingManager;

        [Header("UI References")]
        [SerializeField] private UIDocument _uiDocument;
        
        private VisualElement _root;
        private VisualElement _panel;
        private VisualElement _cardContainer;

        private void Awake()
        {
            if (_uiDocument == null)
                _uiDocument = GetComponent<UIDocument>();

            _root = _uiDocument.rootVisualElement;
            _panel = _root.Q<VisualElement>("upgrade-panel");
            _cardContainer = _root.Q<VisualElement>("card-container");

            // 시작 시 숨김
            _panel.RemoveFromClassList("visible");
            _panel.style.display = DisplayStyle.None;
        }

        private void OnEnable()
        {
            EventBroker.Subscribe<LevelUpEvent>(OnLevelUp);
        }

        private void OnDisable()
        {
            EventBroker.Unsubscribe<LevelUpEvent>(OnLevelUp);
        }

        private void OnLevelUp(LevelUpEvent evt)
        {
            ShowUpgradePanel(evt.UpgradeOptions);
        }

        private void ShowUpgradePanel(List<UpgradeOption> options)
        {
            _cardContainer.Clear();

            for (int i = 0; i < options.Count; i++)
            {
                var option = options[i];
                var card = CreateUpgradeCard(option, i);
                _cardContainer.Add(card);
            }

            _panel.style.display = DisplayStyle.Flex;
            // 트랜지션을 위한 한 프레임 대기 대신 클래스 추가
            _panel.RegisterCallback<GeometryChangedEvent>(OnPanelLayoutChanged);
        }

        private void OnPanelLayoutChanged(GeometryChangedEvent evt)
        {
            _panel.UnregisterCallback<GeometryChangedEvent>(OnPanelLayoutChanged);
            _panel.AddToClassList("visible");
        }

        private VisualElement CreateUpgradeCard(UpgradeOption option, int index)
        {
            var card = new VisualElement();
            card.name = $"upgrade-card-{index}";
            card.AddToClassList("upgrade-card");
            card.AddToClassList($"rarity-{option.Rarity.ToString().ToLower()}");

            var icon = new VisualElement();
            icon.AddToClassList("card-icon");
            if (option.Icon != null)
            {
                icon.style.backgroundImage = new StyleBackground(option.Icon);
            }
            card.Add(icon);

            var nameLabel = new Label(option.DisplayName);
            nameLabel.AddToClassList("card-name");
            card.Add(nameLabel);

            var descLabel = new Label(option.Description);
            descLabel.AddToClassList("card-description");
            card.Add(descLabel);

            // 클릭 이벤트
            card.RegisterCallback<ClickEvent>(evt => OnCardClicked(option));

            return card;
        }

        private void OnCardClicked(UpgradeOption option)
        {
            // 선택 이벤트 발행 (필요한 경우)
            // EventBroker.Publish(new UpgradeSelectedEvent { SelectedId = option.UpgradeId });

            HideUpgradePanel();

            if (_levelingManager != null)
            {
                _levelingManager.ResolveLevelUp();
            }
        }

        private void HideUpgradePanel()
        {
            _panel.RemoveFromClassList("visible");
            
            // 트랜지션 완료 후 display none (0.3s)
            StartCoroutine(HideAfterDelay(0.3f));
        }

        private System.Collections.IEnumerator HideAfterDelay(float delay)
        {
            yield return new WaitForSecondsRealtime(delay);
            _panel.style.display = DisplayStyle.None;
        }
    }
}
