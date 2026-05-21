using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Potop.Client.Core.Events;
using Potop.Client.Gameplay.Meta;

namespace Potop.Client.UI {
    /// <summary>
    /// 로비 화면의 UI와 메타 강화 로직을 연결하는 컨트롤러입니다.
    /// 카드 클릭 시 GemWallet을 소모하고 MetaUpgradeManager를 통해 레벨을 증가시킵니다.
    /// </summary>
    public class LobbyController : MonoBehaviour {
        // Gameplay scene name — assign via inspector or keep as convention.
        private const string GAMEPLAY_SCENE = "MainScene";

        [Header("UI Document")]
        [SerializeField] private UIDocument _uiDocument;

        private VisualElement _upgradeGrid;
        private Label _gemLabel;
        private Button _playButton;

        private void Awake() {
            if (_uiDocument == null)
                _uiDocument = GetComponent<UIDocument>();

            VisualElement root = _uiDocument.rootVisualElement;
            _upgradeGrid = root.Q<VisualElement>("upgrade-grid");
            _gemLabel    = root.Q<Label>("gem-label");
            _playButton  = root.Q<Button>("play-button");
        }

        private void OnEnable() {
            EventBroker.Subscribe<GemChangedEvent>(OnGemChanged);
            EventBroker.Subscribe<MetaUpgradePurchasedEvent>(OnUpgradePurchased);

            if (_playButton != null)
                _playButton.clicked += OnPlayClicked;
        }

        private void OnDisable() {
            EventBroker.Unsubscribe<GemChangedEvent>(OnGemChanged);
            EventBroker.Unsubscribe<MetaUpgradePurchasedEvent>(OnUpgradePurchased);

            if (_playButton != null)
                _playButton.clicked -= OnPlayClicked;
        }

        private void Start() {
            RefreshGemLabel();
            PopulateUpgradeCards();
        }

        // ── Event handlers ───────────────────────────────────────────

        private void OnGemChanged(GemChangedEvent evt) {
            if (_gemLabel != null)
                _gemLabel.text = evt.NewBalance.ToString("N0");
            // Refresh all cards so cost labels update affordability state.
            PopulateUpgradeCards();
        }

        private void OnUpgradePurchased(MetaUpgradePurchasedEvent evt) {
            // Full refresh on purchase to update level pips and cost.
            PopulateUpgradeCards();
        }

        // ── UI building ──────────────────────────────────────────────

        private void RefreshGemLabel() {
            if (_gemLabel == null || GemWallet.Instance == null) return;
            _gemLabel.text = GemWallet.Instance.Balance.ToString("N0");
        }

        private void PopulateUpgradeCards() {
            if (_upgradeGrid == null || MetaUpgradeManager.Instance == null) return;

            _upgradeGrid.Clear();

            foreach (var data in MetaUpgradeManager.Instance.Upgrades) {
                if (data == null) continue;
                _upgradeGrid.Add(BuildCard(data));
            }
        }

        private VisualElement BuildCard(MetaUpgradeData data) {
            int currentLevel = MetaUpgradeManager.Instance.GetLevel(data.UpgradeId);
            bool isMaxed     = currentLevel >= data.MaxLevel;
            int  nextCost    = isMaxed ? 0 : data.GetCost(currentLevel + 1);
            bool canAfford   = !isMaxed && GemWallet.Instance != null && GemWallet.Instance.Balance >= nextCost;

            var card = new VisualElement();
            card.name = $"card-{data.UpgradeId}";
            card.AddToClassList("upgrade-card");
            if (isMaxed) card.AddToClassList("maxed");

            // Icon placeholder
            var icon = new VisualElement();
            icon.AddToClassList("card-icon");
            card.Add(icon);

            // Name
            var nameLabel = new Label(data.DisplayName);
            nameLabel.AddToClassList("card-name");
            card.Add(nameLabel);

            // Description
            var descLabel = new Label(data.Description);
            descLabel.AddToClassList("card-description");
            card.Add(descLabel);

            // Level pips
            var pipContainer = new VisualElement();
            pipContainer.name = "level-bar-container";
            pipContainer.AddToClassList("level-bar-container");
            for (int i = 0; i < data.MaxLevel; i++) {
                var pip = new VisualElement();
                pip.AddToClassList("level-pip");
                if (i < currentLevel) pip.AddToClassList("filled");
                pipContainer.Add(pip);
            }
            card.Add(pipContainer);

            // Cost label
            var costLabel = new Label(isMaxed ? "MAX LEVEL" : $"Cost: {nextCost:N0} 💎");
            costLabel.AddToClassList("card-cost");
            if (!isMaxed && !canAfford) costLabel.AddToClassList("insufficient");
            card.Add(costLabel);

            // Upgrade button
            var btn = new Button();
            btn.name = $"upgrade-btn-{data.UpgradeId}";
            btn.text = isMaxed ? "MAXED" : "UPGRADE";
            btn.AddToClassList("upgrade-button");
            btn.SetEnabled(!isMaxed && canAfford);

            // Capture data for closure.
            var capturedData = data;
            btn.clicked += () => OnUpgradeButtonClicked(capturedData);

            card.Add(btn);
            return card;
        }

        // ── Button callbacks ─────────────────────────────────────────

        private void OnUpgradeButtonClicked(MetaUpgradeData data) {
            if (MetaUpgradeManager.Instance == null) return;
            MetaUpgradeManager.Instance.TryPurchase(data);
            // PopulateUpgradeCards() is called reactively via MetaUpgradePurchasedEvent.
        }

        private void OnPlayClicked() {
            SceneManager.LoadScene(GAMEPLAY_SCENE);
        }
    }
}
