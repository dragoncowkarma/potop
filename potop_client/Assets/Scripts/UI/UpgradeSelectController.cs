using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Potop.Client.Core.Events;
using Potop.Client.Gameplay;
using Potop.Client.Gameplay.Progression;

namespace Potop.Client.UI
{
    /// <summary>
    /// 업그레이드 선택 UI를 관리하는 컨트롤러입니다.
    /// </summary>
    public class UpgradeSelectController : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private UIDocument _uiDocument;
        
        private VisualElement _root;
        private VisualElement _panel;
        private VisualElement _cardContainer;
        private MutationSynergyManager _synergyManager;

        private void Start()
        {
            // 씬 내의 MutationSynergyManager를 찾아 연동합니다.
            _synergyManager = FindFirstObjectByType<MutationSynergyManager>();
        }

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

            // 시너지 힌트 UI 추가
            var synergyHint = CreateSynergyHintLabel(option);
            if (synergyHint != null)
            {
                card.Add(synergyHint);
            }

            return card;
        }

        private void OnCardClicked(UpgradeOption option)
        {
            // 선택된 카드의 모디파이어를 MutationSynergyManager에 추가합니다.
            if (_synergyManager != null)
            {
                ModifierType modifier = GetModifierFromOption(option);
                if (modifier != ModifierType.None)
                {
                    _synergyManager.AddModifier(modifier);
                }
            }

            // 선택 이벤트 발행 (LevelingManager가 구독하여 처리)
            EventBroker.Publish(new UpgradeSelectedEvent { SelectedId = option.UpgradeId });

            HideUpgradePanel();
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

        /// <summary>
        /// 업그레이드 선택지에서 연관 모디파이어를 결정합니다.
        /// </summary>
        private ModifierType GetModifierFromOption(UpgradeOption option)
        {
            if (option.AssociatedModifier != ModifierType.None)
            {
                return option.AssociatedModifier;
            }

            // ID 매칭을 통한 Fallback 처리
            string id = option.UpgradeId.ToLower();
            if (id.Contains("pierce")) return ModifierType.Pierce;
            if (id.Contains("explosion") || id.Contains("explode")) return ModifierType.Explosion;
            if (id.Contains("multi") || id.Contains("shot")) return ModifierType.MultiShot;
            if (id.Contains("bounce")) return ModifierType.Bounce;
            if (id.Contains("scale") || id.Contains("size")) return ModifierType.Scale;
            if (id.Contains("knockback") || id.Contains("push")) return ModifierType.Knockback;

            // 프리셋 데이터 예외 매핑
            if (id == "overdrive") return ModifierType.Pierce;

            return ModifierType.None;
        }

        /// <summary>
        /// 시너지 타입별 플레이어 친화적인 이름 문자열을 반환합니다.
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

        /// <summary>
        /// 업그레이드 옵션에 대응하는 시너지 진행 상황 힌트 라벨을 생성합니다.
        /// </summary>
        private Label CreateSynergyHintLabel(UpgradeOption option)
        {
            if (_synergyManager == null || _synergyManager.SynergyRuleData == null) return null;

            ModifierType modifier = GetModifierFromOption(option);
            if (modifier == ModifierType.None) return null;

            var rules = _synergyManager.SynergyRuleData.Rules;
            if (rules == null || rules.Count == 0) return null;

            List<string> hints = new List<string>();
            bool isAnySynergyCompletedByThis = false;

            foreach (var rule in rules)
            {
                if (rule.Modifier1 == modifier || rule.Modifier2 == modifier)
                {
                    ModifierType otherModifier = (rule.Modifier1 == modifier) ? rule.Modifier2 : rule.Modifier1;
                    
                    bool hasOtherMod = _synergyManager.HasModifier(otherModifier);

                    // 이 카드를 선택할 경우 시너지 진행도가 2/2가 되는지 판정합니다.
                    int progress = hasOtherMod ? 2 : 1;
                    if (progress == 2)
                    {
                        isAnySynergyCompletedByThis = true;
                    }

                    string synergyName = GetSynergyFriendlyName(rule.Synergy);
                    if (progress == 2)
                    {
                        hints.Add($"★ {synergyName} Synergy: 2/2 (Ready!)");
                    }
                    else
                    {
                        hints.Add($"{synergyName} Synergy: 1/2");
                    }
                }
            }

            if (hints.Count == 0) return null;

            var label = new Label(string.Join("\n", hints));
            label.AddToClassList("card-synergy-hint");
            if (isAnySynergyCompletedByThis)
            {
                label.AddToClassList("complete");
            }
            return label;
        }
    }
}
