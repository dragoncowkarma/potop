using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// 실시간 게임플레이 피드백(HP, 점수, 게임오버 등)을 표시하는 UI 컨트롤러입니다.
/// </summary>
public class GameHUD : MonoBehaviour {
    [Header("UI Document")]
    [SerializeField] private UIDocument _uiDocument;

    private Label _scoreLabel;
    private Label _healthLabel;

    private const string SCORE_PREFIX = "SCORE: ";
    private const string HP_SEPARATOR = " / ";

    private void OnEnable() {
        GameManager.OnHealthChanged += UpdateHP;
        GameManager.OnScoreChanged += UpdateScore;
    }

    private void OnDisable() {
        GameManager.OnHealthChanged -= UpdateHP;
        GameManager.OnScoreChanged -= UpdateScore;
    }

    private void Start() {
        if (_uiDocument != null && _uiDocument.rootVisualElement != null) {
            _scoreLabel = _uiDocument.rootVisualElement.Q<Label>("score-label");
            _healthLabel = _uiDocument.rootVisualElement.Q<Label>("health-label");
        }

        if (GameManager.Instance != null) {
            UpdateScore(GameManager.Instance.Score);
            // Health initialization is handled by GameManager's event in StartGame() which might fire before this Start(),
            // but we can query it directly just in case.
            UpdateHP(GameManager.Instance.Health, 100); // Assuming 100 max health for initial UI set if event missed
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
}
