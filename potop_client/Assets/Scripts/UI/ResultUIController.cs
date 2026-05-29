using System;
using UnityEngine;
using UnityEngine.UIElements;
using Potop.Client.Gameplay.Flow;

namespace Potop.Client.UI {
    /// <summary>
    /// 게임 결과(처치 수, 생존 시간, 획득 보석 등)를 UI Toolkit을 사용하여 화면에 표시하고,
    /// 로비로 돌아가는 이벤트를 처리하는 컨트롤러입니다.
    /// </summary>
    public class ResultUIController : MonoBehaviour {
        [Header("UI Document")]
        [SerializeField] private UIDocument _uiDocument;

        private Label _killsLabel;
        private Label _wavesLabel;
        private Label _gemsLabel;
        private Label _timeLabel;
        private Button _lobbyButton;

        private Action _onReturnToLobby;

        private void Awake() {
            if (_uiDocument == null) {
                _uiDocument = GetComponent<UIDocument>();
            }

            if (_uiDocument != null && _uiDocument.rootVisualElement != null) {
                BindVisualElements();
            }
        }

        private void OnEnable() {
            if (_lobbyButton != null) {
                _lobbyButton.clicked += OnLobbyButtonClicked;
            }
        }

        private void OnDisable() {
            if (_lobbyButton != null) {
                _lobbyButton.clicked -= OnLobbyButtonClicked;
            }
        }

        /// <summary>
        /// 결과 데이터를 받아 UI 레이블에 설정합니다.
        /// </summary>
        public void Setup(SettlementData data, Action onReturnToLobby) {
            _onReturnToLobby = onReturnToLobby;

            if (_uiDocument == null) {
                _uiDocument = GetComponent<UIDocument>();
            }

            if (_uiDocument != null && _uiDocument.rootVisualElement != null) {
                BindVisualElements();
            }

            if (_killsLabel != null) _killsLabel.text = data.Kills.ToString("N0");
            if (_wavesLabel != null) _wavesLabel.text = data.MaxWaves.ToString("N0");
            if (_gemsLabel != null) _gemsLabel.text = data.GemsEarned.ToString("N0");
            
            if (_timeLabel != null) {
                int minutes = Mathf.FloorToInt(data.SurvivalTime / 60f);
                int seconds = Mathf.FloorToInt(data.SurvivalTime % 60f);
                _timeLabel.text = $"{minutes:00}:{seconds:00}";
            }
        }

        private void BindVisualElements() {
            var root = _uiDocument.rootVisualElement;
            if (root == null) return;

            _killsLabel = root.Q<Label>("result-kills");
            _wavesLabel = root.Q<Label>("result-waves");
            _gemsLabel = root.Q<Label>("result-gems");
            _timeLabel = root.Q<Label>("result-time");
            _lobbyButton = root.Q<Button>("result-lobby-button");
        }

        private void OnLobbyButtonClicked() {
            _onReturnToLobby?.Invoke();
        }
    }
}
