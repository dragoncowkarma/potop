using Potop.Client.Core;
using Potop.Client.Core.Events;
using Potop.Client.Gameplay.Flow;
using UnityEngine;
using UnityEngine.UIElements;

namespace Potop.Client.UI {
    /// <summary>
    /// 게임오버 화면과 버튼 액션을 제어하고 상태 이벤트를 처리하는 위젯입니다.
    /// </summary>
    public class GameOverPanel : MonoBehaviour {
        private VisualElement _gameOverScreen;
        private Label _finalScoreLabel;
        private Button _restartButton;
        private Button _menuButton;
        private VisualElement _crosshairContainer;

        private const string FINAL_SCORE_PREFIX = "FINAL SCORE: ";

        public void Initialize(
            VisualElement gameOverScreen,
            Label finalScoreLabel,
            Button restartButton,
            Button menuButton,
            VisualElement crosshairContainer
        ) {
            _gameOverScreen = gameOverScreen;
            _finalScoreLabel = finalScoreLabel;
            _restartButton = restartButton;
            _menuButton = menuButton;
            _crosshairContainer = crosshairContainer;

            if (GameFlowController.Instance != null) {
                HandleFlowState(GameFlowController.Instance.CurrentState);
            } else {
                if (_gameOverScreen != null) _gameOverScreen.style.display = DisplayStyle.None;
                if (_crosshairContainer != null) _crosshairContainer.style.display = DisplayStyle.Flex;
            }

            SubscribeToButtons();
        }

        private void OnEnable() {
            EventBroker.Subscribe<GameFlowStateChangedEvent>(OnGameFlowStateChanged);
            GameManager.OnGameOver += ShowGameOver;

            SubscribeToButtons();
        }

        private void OnDisable() {
            EventBroker.Unsubscribe<GameFlowStateChangedEvent>(OnGameFlowStateChanged);
            GameManager.OnGameOver -= ShowGameOver;

            UnsubscribeFromButtons();
        }

        private void OnGameFlowStateChanged(GameFlowStateChangedEvent evt) {
            HandleFlowState(evt.NewState);
        }

        private void HandleFlowState(GameFlowState state) {
            if (state == GameFlowState.InGame || state == GameFlowState.BossBattle || state == GameFlowState.Overclock) {
                if (_gameOverScreen != null) _gameOverScreen.style.display = DisplayStyle.None;
                if (_crosshairContainer != null) _crosshairContainer.style.display = DisplayStyle.Flex;
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

        private void SubscribeToButtons() {
            if (_restartButton != null) {
                _restartButton.clicked -= OnRestartClicked;
                _restartButton.clicked += OnRestartClicked;
            }
            if (_menuButton != null) {
                _menuButton.clicked -= OnMenuClicked;
                _menuButton.clicked += OnMenuClicked;
            }
        }

        private void UnsubscribeFromButtons() {
            if (_restartButton != null) {
                _restartButton.clicked -= OnRestartClicked;
            }
            if (_menuButton != null) {
                _menuButton.clicked -= OnMenuClicked;
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
