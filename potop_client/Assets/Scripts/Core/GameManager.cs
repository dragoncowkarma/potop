using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Potop.Client.Core.Events;

namespace Potop.Client.Core {
    /// <summary>
    /// 게임의 전반적인 상태를 나타내는 열거형입니다.
    /// </summary>
    public enum GameState {
        /// <summary>시작 화면 상태입니다.</summary>
        Start,
        /// <summary>게임 플레이 중 상태입니다.</summary>
        Playing,
        /// <summary>게임 오버 상태입니다.</summary>
        GameOver
    }

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

        /// <summary>
        /// 플레이어의 최대 HP입니다.
        /// </summary>
        public int MaxHealth => _maxHealth;

        [Header("Game State")]
        [SerializeField] private int _health;

        /// <summary>
        /// 플레이어의 현재 HP입니다.
        /// </summary>
        public int Health { get { return _health; } private set { _health = value; } }

        [SerializeField] private int _score;

        /// <summary>
        /// 현재 게임 점수입니다.
        /// </summary>
        public int Score { get { return _score; } private set { _score = value; } }

        [SerializeField] private bool _isGameOver;

        /// <summary>
        /// 현재 게임 상태입니다.
        /// </summary>
        public GameState CurrentState { get; private set; }

        /// <summary>
        /// 게임 오버 여부를 반환합니다.
        /// </summary>
        public bool IsGameOver => _isGameOver;

        /// <summary>
        /// 플레이어의 Transform 위치 정보를 제공합니다.
        /// </summary>
        public Transform PlayerTransform { get; set; }

        // Events
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
            EventBroker.Subscribe<PlayerTakeDamageEvent>(OnPlayerTakeDamage);
            ChangeState(GameState.Start);
            StartGame();
        }

        /// <summary>
        /// 게임 상태를 변경합니다.
        /// </summary>
        /// <param name="newState">새로운 게임 상태</param>
        public void ChangeState(GameState newState) {
            CurrentState = newState;

            if (CurrentState == GameState.Playing) {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            } else {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

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

            EventBroker.Publish(new HealthChangedEvent { CurrentHealth = Health, MaxHealth = _maxHealth });
            EventBroker.Publish(new ScoreChangedEvent { CurrentScore = Score });
        }

        /// <summary>
        /// 플레이어에게 피해를 입힙니다.
        /// </summary>
        /// <param name="value">입힐 피해량</param>
        public void TakeDamage(int value) {
            if (_isGameOver) return;

            Health = Mathf.Max(0, Health - value);
            EventBroker.Publish(new HealthChangedEvent { CurrentHealth = Health, MaxHealth = _maxHealth });

            if (Health <= 0) {
                GameOver();
            }
        }

        /// <summary>
        /// 플레이어의 체력을 회복합니다.
        /// </summary>
        /// <param name="amount">회복할 체력량</param>
        public void Heal(int amount) {
            if (_isGameOver || amount <= 0) return;

            Health = Mathf.Min(_maxHealth, Health + amount);
            EventBroker.Publish(new HealthChangedEvent { CurrentHealth = Health, MaxHealth = _maxHealth });
        }

        private void OnPlayerTakeDamage(PlayerTakeDamageEvent e) {
            TakeDamage(e.Damage);
        }

        /// <summary>
        /// 점수를 추가합니다.
        /// </summary>
        /// <param name="value">추가할 점수</param>
        public void AddScore(int value) {
            if (_isGameOver) return;

            Score += value;
            EventBroker.Publish(new ScoreChangedEvent { CurrentScore = Score });
        }

        private void GameOver() {
            _isGameOver = true;
            ChangeState(GameState.GameOver);
            Time.timeScale = GAME_OVER_TIME_SCALE;

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
            EventBroker.Unsubscribe<PlayerTakeDamageEvent>(OnPlayerTakeDamage);
            if (Instance == this) {
                Instance = null;
            }
        }
    }
}

