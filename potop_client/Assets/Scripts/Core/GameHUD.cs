using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameHUD : MonoBehaviour
{
    [Header("HUD Elements")]
    public Slider hpBar;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI scoreText;
    public RectTransform crosshair;

    [Header("Game Over Panel")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText;
    public Button restartButton;
    public Button mainMenuButton;

    private void OnEnable()
    {
        GameManager.OnHPChanged += UpdateHP;
        GameManager.OnScoreChanged += UpdateScore;
        GameManager.OnGameOver += ShowGameOver;
    }

    private void OnDisable()
    {
        GameManager.OnHPChanged -= UpdateHP;
        GameManager.OnScoreChanged -= UpdateScore;
        GameManager.OnGameOver -= ShowGameOver;
    }

    private void Start()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (restartButton != null)
            restartButton.onClick.AddListener(OnRestartClicked);

        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(OnMainMenuClicked);
    }

    private void Update()
    {
        // Poll score for modes that set score directly (e.g. Stage survival time)
        if (GameManager.Instance != null && scoreText != null)
        {
            scoreText.text = $"SCORE: {GameManager.Instance.score}";
        }
    }

    private void UpdateHP(int current, int max)
    {
        if (hpBar != null)
        {
            hpBar.maxValue = max;
            hpBar.value = current;
        }

        if (hpText != null)
        {
            hpText.text = $"{current} / {max}";
        }
    }

    private void UpdateScore(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = $"SCORE: {score}";
        }
    }

    private void ShowGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        if (finalScoreText != null && GameManager.Instance != null)
        {
            finalScoreText.text = $"FINAL SCORE\n{GameManager.Instance.score}";
        }
    }

    private void OnRestartClicked()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.RestartGame();
    }

    private void OnMainMenuClicked()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.GoToMainMenu();
    }
}
