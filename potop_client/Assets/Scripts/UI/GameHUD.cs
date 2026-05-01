using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 실시간 게임플레이 피드백(HP, 점수, 게임오버 등)을 표시하는 UI 컨트롤러입니다.
/// </summary>
public class GameHUD : MonoBehaviour {
    [Header("HUD Elements")]
    [SerializeField] private Slider _hpBar;
    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private RectTransform _crosshair;

    [Header("Game Over Panel")]
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TextMeshProUGUI _finalScoreText;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _mainMenuButton;

    private const string SCORE_PREFIX = "SCORE: ";
    private const string FINAL_SCORE_PREFIX = "FINAL SCORE\n";
    private const string HP_SEPARATOR = " / ";

    private void OnEnable() {
        GameManager.OnHPChanged += UpdateHP;
        GameManager.OnScoreChanged += UpdateScore;
        GameManager.OnGameOver += ShowGameOver;
    }

    private void OnDisable() {
        GameManager.OnHPChanged -= UpdateHP;
        GameManager.OnScoreChanged -= UpdateScore;
        GameManager.OnGameOver -= ShowGameOver;
    }

    private void Start() {
        if (_gameOverPanel != null) {
            _gameOverPanel.SetActive(false);
        }

        if (_restartButton != null) {
            _restartButton.onClick.AddListener(OnRestartClicked);
        }

        if (_mainMenuButton != null) {
            _mainMenuButton.onClick.AddListener(OnMainMenuClicked);
        }
    }

    private void Update() {
        if (GameManager.Instance != null && _scoreText != null) {
            _scoreText.text = $"{SCORE_PREFIX}{GameManager.Instance.Score}";
        }
    }

    private void UpdateHP(int current, int max) {
        if (_hpBar != null) {
            _hpBar.maxValue = max;
            _hpBar.value = current;
        }

        if (_hpText != null) {
            _hpText.text = $"{current}{HP_SEPARATOR}{max}";
        }
    }

    private void UpdateScore(int score) {
        if (_scoreText != null) {
            _scoreText.text = $"{SCORE_PREFIX}{score}";
        }
    }

    private void ShowGameOver() {
        if (_gameOverPanel != null) {
            _gameOverPanel.SetActive(true);
        }

        if (_finalScoreText != null && GameManager.Instance != null) {
            _finalScoreText.text = $"{FINAL_SCORE_PREFIX}{GameManager.Instance.Score}";
        }
    }

    private void OnRestartClicked() {
        if (GameManager.Instance != null) {
            GameManager.Instance.RestartGame();
        }
    }

    private void OnMainMenuClicked() {
        if (GameManager.Instance != null) {
            GameManager.Instance.GoToMainMenu();
        }
    }
}
