using Potop.Client.Core;
using Potop.Client.Core.Events;
using UnityEngine;
using UnityEngine.UIElements;

namespace Potop.Client.UI {
    /// <summary>
    /// 게임 점수를 표시하고 관리하는 위젯입니다.
    /// </summary>
    public class ScoreWidget : MonoBehaviour {
        private Label _scoreLabel;

        private const string SCORE_PREFIX = "SCORE: ";

        public void Initialize(Label scoreLabel) {
            _scoreLabel = scoreLabel;

            if (GameManager.Instance != null) {
                UpdateScore(GameManager.Instance.Score);
            } else {
                UpdateScore(0);
            }
        }

        private void OnEnable() {
            EventBroker.Subscribe<ScoreChangedEvent>(OnScoreChanged);
        }

        private void OnDisable() {
            EventBroker.Unsubscribe<ScoreChangedEvent>(OnScoreChanged);
        }

        private void OnScoreChanged(ScoreChangedEvent evt) {
            UpdateScore(evt.CurrentScore);
        }

        private void UpdateScore(int score) {
            if (_scoreLabel != null) {
                _scoreLabel.text = $"{SCORE_PREFIX}{score}";
            }
        }
    }
}
