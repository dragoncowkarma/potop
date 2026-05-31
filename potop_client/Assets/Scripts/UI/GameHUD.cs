using Potop.Client.Gameplay.Combat;
using UnityEngine;
using UnityEngine.UIElements;

namespace Potop.Client.UI {
    /// <summary>
    /// UI 요소를 로드하고 각 기능별 위젯의 초기화를 담당하는 HUD 컨트롤러입니다.
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

        [Header("Widgets")]
        [SerializeField] private HealthBarWidget _healthBarWidget;
        [SerializeField] private ScoreWidget _scoreWidget;
        [SerializeField] private FeverGaugeWidget _feverGaugeWidget;
        [SerializeField] private OverchargeWidget _overchargeWidget;
        [SerializeField] private TacticalSkillBar _tacticalSkillBar;
        [SerializeField] private GameOverPanel _gameOverPanel;

        private void Start() {
            if (_uiDocument != null && _uiDocument.rootVisualElement != null) {
                VisualElement root = _uiDocument.rootVisualElement;

                EnsureWidgets();

                _healthBarWidget.Initialize(
                    root.Q<Label>("health-label"),
                    root.Q<VisualElement>("fullscreen-flash-overlay")
                );

                _scoreWidget.Initialize(
                    root.Q<Label>("score-label")
                );

                _feverGaugeWidget.Initialize(
                    root.Q<VisualElement>("fever-bar-fill")
                );

                _overchargeWidget.Initialize(
                    root.Q<VisualElement>("overcharge-container"),
                    root.Q<VisualElement>("overcharge-bar-fill")
                );

                _tacticalSkillBar.Initialize(
                    root.Q<VisualElement>("energy-bar-fill"),
                    root.Q<VisualElement>("emp-cooldown-overlay"), root.Q<Label>("emp-cooldown-label"),
                    root.Q<VisualElement>("orbital-cooldown-overlay"), root.Q<Label>("orbital-cooldown-label"),
                    root.Q<VisualElement>("shield-cooldown-overlay"), root.Q<Label>("shield-cooldown-label"),
                    _empAction, _orbitalStrikeAction, _shieldAction,
                    _empSkill, _orbitalStrikeSkill, _overloadShieldSkill
                );

                _gameOverPanel.Initialize(
                    root.Q<VisualElement>("game-over-screen"),
                    root.Q<Label>("final-score-label"),
                    root.Q<Button>("restart-button"),
                    root.Q<Button>("menu-button"),
                    root.Q<VisualElement>("crosshair-container")
                );
            }
        }

        private void EnsureWidgets() {
            if (_healthBarWidget == null) _healthBarWidget = GetComponent<HealthBarWidget>() ?? gameObject.AddComponent<HealthBarWidget>();
            if (_scoreWidget == null) _scoreWidget = GetComponent<ScoreWidget>() ?? gameObject.AddComponent<ScoreWidget>();
            if (_feverGaugeWidget == null) _feverGaugeWidget = GetComponent<FeverGaugeWidget>() ?? gameObject.AddComponent<FeverGaugeWidget>();
            if (_overchargeWidget == null) _overchargeWidget = GetComponent<OverchargeWidget>() ?? gameObject.AddComponent<OverchargeWidget>();
            if (_tacticalSkillBar == null) _tacticalSkillBar = GetComponent<TacticalSkillBar>() ?? gameObject.AddComponent<TacticalSkillBar>();
            if (_gameOverPanel == null) _gameOverPanel = GetComponent<GameOverPanel>() ?? gameObject.AddComponent<GameOverPanel>();
        }
    }
}
