using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

/// <summary>
/// 전역 게임 상태(HP, 점수, 게임 오버 등)를 관리하는 싱글톤 클래스입니다.
/// </summary>
public class GameManager : MonoBehaviour {
    /// <summary>
    /// GameManager의 싱글톤 인스턴스입니다.
    /// </summary>
    public static GameManager Instance { get; private set; }

    [Header("Player Settings")]
    [SerializeField, FormerlySerializedAs("maxHP")] private int _maxHP = 100;

    [Header("Game State")]
    [SerializeField, FormerlySerializedAs("currentHP")] private int _currentHP;
    [SerializeField, FormerlySerializedAs("score")] private int _score;
    [SerializeField, FormerlySerializedAs("isGameOver")] private bool _isGameOver;

    /// <summary>
    /// 현재 플레이어의 체력을 반환합니다.
    /// </summary>
    public int CurrentHP => _currentHP;

    /// <summary>
    /// 현재 게임 점수를 반환합니다.
    /// </summary>
    public int Score => _score;

    /// <summary>
    /// 게임 오버 여부를 반환합니다.
    /// </summary>
    public bool IsGameOver => _isGameOver;

    /// <summary>
    /// 플레이어의 Transform 위치 정보를 제공합니다.
    /// (GameObject.Find("Player") 사용을 피하기 위함)
    /// </summary>
    public Transform PlayerTransform { get; set; }

    // Events
    /// <summary>
    /// HP가 변경될 때 호출되는 이벤트 (현재 HP, 최대 HP)
    /// </summary>
    public static event Action<int, int> OnHPChanged;

    /// <summary>
    /// 점수가 변경될 때 호출되는 이벤트 (점수)
    /// </summary>
    public static event Action<int> OnScoreChanged;

    /// <summary>
    /// 게임 오버 시 호출되는 이벤트
    /// </summary>
    public static event Action OnGameOver;

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
        StartGame();
    }

    /// <summary>
    /// 게임을 초기 상태로 시작합니다.
    /// </summary>
    public void StartGame() {
        _currentHP = _maxHP;
        _score = 0;
        _isGameOver = false;
        Time.timeScale = NORMAL_TIME_SCALE;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        OnHPChanged?.Invoke(_currentHP, _maxHP);
        OnScoreChanged?.Invoke(_score);
    }

    /// <summary>
    /// 플레이어에게 피해를 입힙니다.
    /// </summary>
    /// <param name="damage">입힐 피해량</param>
    public void TakeDamage(int damage) {
        if (_isGameOver) return;

        _currentHP = Mathf.Max(0, _currentHP - damage);
        OnHPChanged?.Invoke(_currentHP, _maxHP);

        if (_currentHP <= 0) {
            GameOver();
        }
    }

    /// <summary>
    /// 점수를 추가합니다.
    /// </summary>
    /// <param name="amount">추가할 점수</param>
    public void AddScore(int amount) {
        if (_isGameOver) return;

        _score += amount;
        OnScoreChanged?.Invoke(_score);
    }

    private void GameOver() {
        _isGameOver = true;
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
