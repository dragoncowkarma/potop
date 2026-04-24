using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Player Settings")]
    public int maxHP = 100;

    [Header("Game State")]
    public int currentHP;
    public int score;
    public bool isGameOver;

    // Events
    public static event Action<int, int> OnHPChanged;       // currentHP, maxHP
    public static event Action<int> OnScoreChanged;          // score
    public static event Action OnGameOver;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        currentHP = maxHP;
        score = 0;
        isGameOver = false;
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        OnHPChanged?.Invoke(currentHP, maxHP);
        OnScoreChanged?.Invoke(score);
    }

    public void TakeDamage(int damage)
    {
        if (isGameOver) return;

        currentHP = Mathf.Max(0, currentHP - damage);
        OnHPChanged?.Invoke(currentHP, maxHP);

        if (currentHP <= 0)
        {
            GameOver();
        }
    }

    public void AddScore(int amount)
    {
        if (isGameOver) return;

        score += amount;
        OnScoreChanged?.Invoke(score);
    }

    private void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        OnGameOver?.Invoke();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Start");
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
