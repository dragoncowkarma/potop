using System;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.Serialization;

public enum GameState { Start, Playing, GameOver }

/// <summary>
/// 전역 게임 상태(HP, 점수, 게임 오버 등)를 관리하는 싱글톤 클래스입니다.
/// </summary>
public class GameManager : MonoBehaviour {
    /// <summary>
    /// GameManager의 싱글톤 인스턴스입니다.
    /// </summary>
    public static GameManager Instance { get; private set; }

    [Header("Player Settings")]
    [SerializeField] private int _maxHealth = 100;

    [Header("Game State")]
    [SerializeField] private int _health;
    public int Health { get { return _health; } private set { _health = value; } }

    [SerializeField] private int _score;
    public int Score { get { return _score; } private set { _score = value; } }


    [SerializeField] private bool _isGameOver;

    public GameState CurrentState { get; private set; }

    /// <summary>
    /// 게임 오버 여부를 반환합니다.
    /// </summary>
    public bool IsGameOver => _isGameOver;

    /// <summary>
    /// 플레이어의 Transform 위치 정보를 제공합니다.
    /// </summary>
    public Transform Player { get; set; }

    // Events
    /// <summary>
    /// HP가 변경될 때 호출되는 이벤트 (현재 HP, 최대 HP)
    /// </summary>
    public static event Action<int, int> OnHealthChanged;

    /// <summary>
    /// 점수가 변경될 때 호출되는 이벤트 (점수)
    /// </summary>
    public static event Action<int> OnScoreChanged;

    /// <summary>
    /// 게임 오버 시 호출되는 이벤트
    /// </summary>
    public static event Action OnGameOver;

    /// <summary>
    /// 게임 상태가 변경될 때 호출되는 이벤트 (현재 상태)
    /// </summary>
    public static event Action<GameState> OnStateChanged;

    private const float GAME_OVER_TIME_SCALE = 0f;
    private const float NORMAL_TIME_SCALE = 1f;
    private const string START_SCENE_NAME = "Start";

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start() {
        ChangeState(GameState.Start);
        StartGame();
    }

    /// <summary>
    /// 게임 상태를 변경합니다.
    /// </summary>
    /// <param name="newState">새로운 게임 상태</param>
    public void ChangeState(GameState newState) {
        CurrentState = newState;
        OnStateChanged?.Invoke(CurrentState);
    }

    /// <summary>
    /// 게임을 초기 상태로 시작합니다.
    /// </summary>
    public void StartGame() {
        Health = _maxHealth;
        Score = 0;
        _isGameOver = false;
        ChangeState(GameState.Playing);
        Time.timeScale = NORMAL_TIME_SCALE;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        OnHealthChanged?.Invoke(Health, _maxHealth);
        OnScoreChanged?.Invoke(Score);
    }

    /// <summary>
    /// 플레이어에게 피해를 입힙니다.
    /// </summary>
    /// <param name="value">입힐 피해량</param>
    public void TakeDamage(int value) {
        if (_isGameOver) return;

        Health = Mathf.Max(0, Health - value);
        OnHealthChanged?.Invoke(Health, _maxHealth);

        if (Health <= 0) {
            GameOver();
        }
    }

    /// <summary>
    /// 점수를 추가합니다.
    /// </summary>
    /// <param name="value">추가할 점수</param>
    public void AddScore(int value) {
        if (_isGameOver) return;

        Score += value;
        OnScoreChanged?.Invoke(Score);
    }

    private void GameOver() {
        _isGameOver = true;
        ChangeState(GameState.GameOver);
        Time.timeScale = GAME_OVER_TIME_SCALE;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        OnGameOver?.Invoke();
    }

    /// <summary>
    /// 현재 씬을 다시 로드하여 게임을 재시작합니다.
    /// </summary>
    public void RestartGame() {
        Time.timeScale = NORMAL_TIME_SCALE;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// 시작 메뉴 씬으로 이동합니다.
    /// </summary>
    public void GoToMainMenu() {
        Time.timeScale = NORMAL_TIME_SCALE;
        SceneManager.LoadScene(START_SCENE_NAME);
    }

    private void OnDestroy() {
        if (Instance == this) {
            Instance = null;
        }
    }
}

