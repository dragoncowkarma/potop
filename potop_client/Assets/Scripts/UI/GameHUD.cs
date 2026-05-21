using System.Collections;
using Potop.Client.Core;
using Potop.Client.Core.Events;
using Potop.Client.Data.Items;
using Potop.Client.Gameplay.Combat;
using Potop.Client.Gameplay.Items;
using UnityEngine;
using UnityEngine.UIElements;

namespace Potop.Client.UI {
    /// <summary>
    /// 실시간 게임플레이 피드백(HP, 점수, 게임오버 등)을 표시하는 UI 컨트롤러입니다.
    /// </summary>
    public class GameHUD : MonoBehaviour {
        [Header("UI Document")]
        [SerializeField] private UIDocument _uiDocument;

        /// <summary>
        /// UI를 렌더링하는 데 사용되는 UIDocument입니다.
        /// </summary>
        public UIDocument UiDocument => _uiDocument;

        [Header("Tactical Skills Inputs")]
        [SerializeField] private UnityEngine.InputSystem.InputActionReference _empAction;
        [SerializeField] private UnityEngine.InputSystem.InputActionReference _orbitalStrikeAction;
        [SerializeField] private UnityEngine.InputSystem.InputActionReference _shieldAction;

        [Header("Tactical Skill References")]
        [SerializeField] private EMPSkill _empSkill;
        [SerializeField] private OrbitalStrikeSkill _orbitalStrikeSkill;
        [SerializeField] private OverloadShieldSkill _overloadShieldSkill;

        [Header("Overcharge Reference")]
        [SerializeField] private OverchargeController _overchargeController;

        private Label _scoreLabel;
        private Label _healthLabel;
        private VisualElement _feverBarFill;

        private VisualElement _crosshairContainer;
        private VisualElement _gameOverScreen;
        private Label _finalScoreLabel;
        private Button _restartButton;
        private Button _menuButton;

        // Tactical UI Element References
        private VisualElement _energyBarFill;
        private VisualElement _empCooldownOverlay;
        private Label _empCooldownLabel;
        private VisualElement _orbitalCooldownOverlay;
        private Label _orbitalCooldownLabel;
        private VisualElement _shieldCooldownOverlay;
        private Label _shieldCooldownLabel;
        private VisualElement _overchargeContainer;
        private VisualElement _overchargeBarFill;
        private VisualElement _fullscreenFlashOverlay;

        private const string SCORE_PREFIX = "SCORE: ";
        private const string FINAL_SCORE_PREFIX = "FINAL SCORE: ";
        private const string HP_SEPARATOR = " / ";

        private int _lastEnergy = -1;
        private int _lastEmpRemainingTenths = -1;
        private int _lastOrbitalRemainingTenths = -1;
        private int _lastShieldRemainingTenths = -1;
        private Coroutine _monitorCoroutine;

        private void OnEnable() {
            EventBroker.Subscribe<HealthChangedEvent>(OnHealthChanged);
            EventBroker.Subscribe<ScoreChangedEvent>(OnScoreChanged);
            EventBroker.Subscribe<FeverProgressChangedEvent>(OnFeverProgressChanged);
            EventBroker.Subscribe<FeverStateChangedEvent>(OnFeverStateChanged);
            EventBroker.Subscribe<EnergyChangedEvent>(OnEnergyChanged);
            EventBroker.Subscribe<ItemCollectedEvent>(OnItemCollected);
            GameManager.OnGameOver += ShowGameOver;
            GameManager.OnStateChanged += HandleStateChanged;

            if (_empAction != null && _empAction.action != null) {
                _empAction.action.Enable();
                _empAction.action.started += OnEmpTriggered;
            }
            if (_orbitalStrikeAction != null && _orbitalStrikeAction.action != null) {
                _orbitalStrikeAction.action.Enable();
                _orbitalStrikeAction.action.started += OnOrbitalStrikeTriggered;
            }
            if (_shieldAction != null && _shieldAction.action != null) {
                _shieldAction.action.Enable();
                _shieldAction.action.started += OnShieldTriggered;
            }

            if (_uiDocument != null && _uiDocument.rootVisualElement != null) {
                SubscribeToButtons();
            }

            // Start frame-by-frame monitoring
            if (_monitorCoroutine == null) {
                _monitorCoroutine = StartCoroutine(MonitorRoutine());
            }
        }

        private void OnDisable() {
            EventBroker.Unsubscribe<HealthChangedEvent>(OnHealthChanged);
            EventBroker.Unsubscribe<ScoreChangedEvent>(OnScoreChanged);
            EventBroker.Unsubscribe<FeverProgressChangedEvent>(OnFeverProgressChanged);
            EventBroker.Unsubscribe<FeverStateChangedEvent>(OnFeverStateChanged);
            EventBroker.Unsubscribe<EnergyChangedEvent>(OnEnergyChanged);
            EventBroker.Unsubscribe<ItemCollectedEvent>(OnItemCollected);
            GameManager.OnGameOver -= ShowGameOver;
            GameManager.OnStateChanged -= HandleStateChanged;

            if (_empAction != null && _empAction.action != null) {
                _empAction.action.started -= OnEmpTriggered;
                _empAction.action.Disable();
            }
            if (_orbitalStrikeAction != null && _orbitalStrikeAction.action != null) {
                _orbitalStrikeAction.action.started -= OnOrbitalStrikeTriggered;
                _orbitalStrikeAction.action.Disable();
            }
            if (_shieldAction != null && _shieldAction.action != null) {
                _shieldAction.action.started -= OnShieldTriggered;
                _shieldAction.action.Disable();
            }

            if (_monitorCoroutine != null) {
                StopCoroutine(_monitorCoroutine);
                _monitorCoroutine = null;
            }

            UnsubscribeFromButtons();
        }

        private void Start() {
            if (_uiDocument != null && _uiDocument.rootVisualElement != null) {
                VisualElement root = _uiDocument.rootVisualElement;
                _scoreLabel = root.Q<Label>("score-label");
                _healthLabel = root.Q<Label>("health-label");
                _feverBarFill = root.Q<VisualElement>("fever-bar-fill");

                // Bind Tactical UI elements
                _energyBarFill = root.Q<VisualElement>("energy-bar-fill");
                _empCooldownOverlay = root.Q<VisualElement>("emp-cooldown-overlay");
                _empCooldownLabel = root.Q<Label>("emp-cooldown-label");
                _orbitalCooldownOverlay = root.Q<VisualElement>("orbital-cooldown-overlay");
                _orbitalCooldownLabel = root.Q<Label>("orbital-cooldown-label");
                _shieldCooldownOverlay = root.Q<VisualElement>("shield-cooldown-overlay");
                _shieldCooldownLabel = root.Q<Label>("shield-cooldown-label");
                _overchargeContainer = root.Q<VisualElement>("overcharge-container");
                _overchargeBarFill = root.Q<VisualElement>("overcharge-bar-fill");
                _fullscreenFlashOverlay = root.Q<VisualElement>("fullscreen-flash-overlay");

                _crosshairContainer = root.Q<VisualElement>("crosshair-container");
                _gameOverScreen = root.Q<VisualElement>("game-over-screen");
                _finalScoreLabel = root.Q<Label>("final-score-label");
                _restartButton = root.Q<Button>("restart-button");
                _menuButton = root.Q<Button>("menu-button");

                SubscribeToButtons();
            }

            EnsureReferences();

            // Check if any skills are already on cooldown at start
            CheckInitialCooldowns();

            if (GameManager.Instance != null) {
                UpdateScore(GameManager.Instance.Score);
                int hp = PlayerHealthController.Instance != null ? PlayerHealthController.Instance.Health : 100;
                int maxHp = PlayerHealthController.Instance != null ? PlayerHealthController.Instance.MaxHealth : 100;
                UpdateHP(hp, maxHp);
                HandleStateChanged(GameManager.Instance.CurrentState);
            }
        }

        private IEnumerator MonitorRoutine() {
            while (true) {
                EnsureReferences();
                UpdateEnergyTracking();
                UpdateOvercharge();
                yield return null;
            }
        }

        private void EnsureReferences() {
            if (_empSkill != null && _orbitalStrikeSkill != null && _overloadShieldSkill != null && _overchargeController != null) {
                return;
            }

            if (GameManager.Instance != null && GameManager.Instance.PlayerTransform != null) {
                var playerGo = GameManager.Instance.PlayerTransform.gameObject;
                if (_empSkill == null) _empSkill = playerGo.GetComponent<EMPSkill>();
                if (_orbitalStrikeSkill == null) _orbitalStrikeSkill = playerGo.GetComponent<OrbitalStrikeSkill>();
                if (_overloadShieldSkill == null) _overloadShieldSkill = playerGo.GetComponent<OverloadShieldSkill>();
                if (_overchargeController == null) _overchargeController = playerGo.GetComponent<OverchargeController>();
            }

            // Fallback to searching this GameObject
            if (_empSkill == null) _empSkill = GetComponent<EMPSkill>();
            if (_orbitalStrikeSkill == null) _orbitalStrikeSkill = GetComponent<OrbitalStrikeSkill>();
            if (_overloadShieldSkill == null) _overloadShieldSkill = GetComponent<OverloadShieldSkill>();
            if (_overchargeController == null) _overchargeController = GetComponent<OverchargeController>();
        }

        private void CheckInitialCooldowns() {
            if (_empSkill != null && _empSkill.GetRemainingCooldown() > 0f) {
                StartCoroutine(CooldownRoutine(_empSkill, _empCooldownOverlay, _empCooldownLabel));
            }
            if (_orbitalStrikeSkill != null && _orbitalStrikeSkill.GetRemainingCooldown() > 0f) {
                StartCoroutine(CooldownRoutine(_orbitalStrikeSkill, _orbitalCooldownOverlay, _orbitalCooldownLabel));
            }
            if (_overloadShieldSkill != null && _overloadShieldSkill.GetRemainingCooldown() > 0f) {
                StartCoroutine(CooldownRoutine(_overloadShieldSkill, _shieldCooldownOverlay, _shieldCooldownLabel));
            }
        }

        private IEnumerator CooldownRoutine(TacticalSkillBase skill, VisualElement overlay, Label label) {
            if (skill == null || overlay == null || label == null) {
                yield break;
            }

            overlay.style.display = DisplayStyle.Flex;
            label.style.display = DisplayStyle.Flex;

            float cooldown = skill.Cooldown;
            int lastTenths = -1;

            while (true) {
                float remaining = skill.GetRemainingCooldown();
                if (remaining <= 0f) {
                    break;
                }

                if (cooldown > 0f) {
                    float pct = (remaining / cooldown) * 100f;
                    overlay.style.height = new Length(pct, LengthUnit.Percent);
                }

                int currentTenths = Mathf.RoundToInt(remaining * 10f);
                if (currentTenths != lastTenths) {
                    lastTenths = currentTenths;
                    label.text = string.Format("{0:F1}s", remaining);
                }

                yield return new WaitForSeconds(0.1f);
            }

            overlay.style.display = DisplayStyle.None;
            label.style.display = DisplayStyle.None;
        }

        private void UpdateEnergyTracking() {
            if (EnergyManager.Instance != null) {
                int current = EnergyManager.Instance.CurrentEnergy;
                if (current != _lastEnergy) {
                    _lastEnergy = current;
                    EventBroker.Publish(new EnergyChangedEvent {
                        CurrentEnergy = current,
                        MaxEnergy = EnergyManager.MAX_ENERGY
                    });
                }
            }
        }

        private void UpdateOvercharge() {
            if (_overchargeController != null && _overchargeContainer != null) {
                float gauge = _overchargeController.CurrentGauge;
                float maxGauge = 100f;

                var field = _overchargeController.GetType().GetField("_overchargeData", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                var data = field?.GetValue(_overchargeController);
                if (data != null) {
                    var maxGaugeField = data.GetType().GetField("MaxGauge", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                    if (maxGaugeField != null) {
                        maxGauge = (float)maxGaugeField.GetValue(data);
                    }
                }

                float pct = (maxGauge > 0f) ? (gauge / maxGauge) * 100f : 0f;
                if (_overchargeBarFill != null) {
                    _overchargeBarFill.style.width = new Length(Mathf.Clamp(pct, 0f, 100f), LengthUnit.Percent);
                }

                if (gauge > 0f) {
                    _overchargeContainer.AddToClassList("visible");
                } else {
                    _overchargeContainer.RemoveFromClassList("visible");
                }

                var state = _overchargeController.CurrentState;
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

        private void SubscribeToButtons() {
            if (_restartButton != null) _restartButton.clicked += OnRestartClicked;
            if (_menuButton != null) _menuButton.clicked += OnMenuClicked;
        }

        private void UnsubscribeFromButtons() {
            if (_restartButton != null) _restartButton.clicked -= OnRestartClicked;
            if (_menuButton != null) _menuButton.clicked -= OnMenuClicked;
        }

        private void OnHealthChanged(HealthChangedEvent evt) {
            UpdateHP(evt.CurrentHealth, evt.MaxHealth);
        }

        private void OnScoreChanged(ScoreChangedEvent evt) {
            UpdateScore(evt.CurrentScore);
        }

        private void OnFeverProgressChanged(FeverProgressChangedEvent evt) {
            if (_feverBarFill != null) {
                _feverBarFill.style.width = new Length(evt.Progress * 100f, LengthUnit.Percent);
            }
        }

        private void OnFeverStateChanged(FeverStateChangedEvent evt) {
            if (_feverBarFill != null) {
                if (evt.IsFeverActive) {
                    _feverBarFill.AddToClassList("fever-active");
                } else {
                    _feverBarFill.RemoveFromClassList("fever-active");
                }
            }
        }

        private void OnEnergyChanged(EnergyChangedEvent evt) {
            if (_energyBarFill != null) {
                float pct = (float)evt.CurrentEnergy / evt.MaxEnergy * 100f;
                _energyBarFill.style.width = new Length(Mathf.Clamp(pct, 0f, 100f), LengthUnit.Percent);
            }
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

        private void OnEmpTriggered(UnityEngine.InputSystem.InputAction.CallbackContext context) {
            if (_empSkill != null && _empSkill.TryExecute()) {
                StartCoroutine(CooldownRoutine(_empSkill, _empCooldownOverlay, _empCooldownLabel));
            }
        }

        private void OnOrbitalStrikeTriggered(UnityEngine.InputSystem.InputAction.CallbackContext context) {
            if (_orbitalStrikeSkill != null && _orbitalStrikeSkill.TryExecute()) {
                StartCoroutine(CooldownRoutine(_orbitalStrikeSkill, _orbitalCooldownOverlay, _orbitalCooldownLabel));
            }
        }

        private void OnShieldTriggered(UnityEngine.InputSystem.InputAction.CallbackContext context) {
            if (_overloadShieldSkill != null && _overloadShieldSkill.TryExecute()) {
                StartCoroutine(CooldownRoutine(_overloadShieldSkill, _shieldCooldownOverlay, _shieldCooldownLabel));
            }
        }

        private void UpdateHP(int current, int max) {
            if (_healthLabel != null) {
                _healthLabel.text = $"{current}{HP_SEPARATOR}{max}";
            }
        }

        private void UpdateScore(int score) {
            if (_scoreLabel != null) {
                _scoreLabel.text = $"{SCORE_PREFIX}{score}";
            }
        }

        private void ShowGameOver() {
            if (_gameOverScreen != null) {
                _gameOverScreen.style.display = DisplayStyle.Flex;
            }
            if (_finalScoreLabel != null && GameManager.Instance != null) {
                _finalScoreLabel.text = $"{FINAL_SCORE_PREFIX}{GameManager.Instance.Score}";
            }
            if (_crosshairContainer != null) {
                _crosshairContainer.style.display = DisplayStyle.None;
            }
        }

        private void HandleStateChanged(GameState state) {
            if (state == GameState.Playing) {
                if (_gameOverScreen != null) _gameOverScreen.style.display = DisplayStyle.None;
                if (_crosshairContainer != null) _crosshairContainer.style.display = DisplayStyle.Flex;
            }
        }

        private void OnRestartClicked() {
            if (GameManager.Instance != null) {
                GameManager.Instance.RestartGame();
            }
        }

        private void OnMenuClicked() {
            if (GameManager.Instance != null) {
                GameManager.Instance.GoToMainMenu();
            }
        }
    }
}
