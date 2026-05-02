using Potop.Client.Core;
using UnityEngine;
using UnityEngine.UIElements;

namespace Potop.Client.UI {
    /// <summary>
    /// 실시간 게임플레이 피드백(HP, 점수, 게임오버 등)을 표시하는 UI 컨트롤러입니다.
    /// </summary>
    public class GameHUD : MonoBehaviour {
        [Header("UI Document")]
        [SerializeField] private UIDocument _uiDocument;

        private Label _scoreLabel;
        private Label _healthLabel;

        private VisualElement _crosshairContainer;
        private VisualElement _gameOverScreen;
        private Label _finalScoreLabel;
        private Button _restartButton;
        private Button _menuButton;

        private const string SCORE_PREFIX = "SCORE: ";
        private const string FINAL_SCORE_PREFIX = "FINAL SCORE: ";
        private const string HP_SEPARATOR = " / ";

        private void OnEnable() {
            GameManager.OnHealthChanged += UpdateHP;
            GameManager.OnScoreChanged += UpdateScore;
            GameManager.OnGameOver += ShowGameOver;
            GameManager.OnStateChanged += HandleStateChanged;
        }

        private void OnDisable() {
            GameManager.OnHealthChanged -= UpdateHP;
            GameManager.OnScoreChanged -= UpdateScore;
            GameManager.OnGameOver -= ShowGameOver;
            GameManager.OnStateChanged -= HandleStateChanged;
        }

        private void Start() {
            if (_uiDocument != null && _uiDocument.rootVisualElement != null) {
                VisualElement root = _uiDocument.rootVisualElement;
                _scoreLabel = root.Q<Label>("score-label");
                _healthLabel = root.Q<Label>("health-label");

                _crosshairContainer = root.Q<VisualElement>("crosshair-container");
                _gameOverScreen = root.Q<VisualElement>("game-over-screen");
                _finalScoreLabel = root.Q<Label>("final-score-label");
                _restartButton = root.Q<Button>("restart-button");
                _menuButton = root.Q<Button>("menu-button");

                if (_restartButton != null) _restartButton.clicked += OnRestartClicked;
                if (_menuButton != null) _menuButton.clicked += OnMenuClicked;
            }

            if (GameManager.Instance != null) {
                UpdateScore(GameManager.Instance.Score);
                UpdateHP(GameManager.Instance.Health, 100);
                HandleStateChanged(GameManager.Instance.CurrentState);
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
