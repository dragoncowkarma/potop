using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Potop.Client.Core.Events;
using Potop.Client.Gameplay.Meta;

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

        [Header("Game State")]
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
            if (MetaUpgradeManager.Instance == null) {
                Debug.LogWarning("MetaUpgradeManager.Instance is null during StartGame.");
            }
            if (GemWallet.Instance == null) {
                Debug.LogWarning("GemWallet.Instance is null during StartGame.");
            }

            Score = 0;
            _isGameOver = false;
            ChangeState(GameState.Playing);
            Time.timeScale = NORMAL_TIME_SCALE;

            if (PlayerHealthController.Instance != null) {
                PlayerHealthController.Instance.InitializeHealth();
            }

            EventBroker.Publish(new ScoreChangedEvent { CurrentScore = Score });
        }

        /// <summary>
        /// 플레이어에게 피해를 입힙니다.
        /// 하위 호환성을 위한 래퍼 메서드입니다.
        /// </summary>
        /// <param name="value">입힐 피해량</param>
        public void TakeDamage(int value) {
            if (_isGameOver) return;
            if (PlayerHealthController.Instance != null) {
                PlayerHealthController.Instance.TakeDamage(value);
            }
        }

        /// <summary>
        /// 플레이어의 체력을 회복합니다.
        /// 하위 호환성을 위한 래퍼 메서드입니다.
        /// </summary>
        /// <param name="amount">회복할 체력량</param>
        public void Heal(int amount) {
            if (_isGameOver) return;
            if (PlayerHealthController.Instance != null) {
                PlayerHealthController.Instance.Heal(amount);
            }
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

        public void TriggerGameOver() {
            GameOver();
        }

        private void GameOver() {
            if (_isGameOver) return;
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
            if (Instance == this) {
                Instance = null;
            }
        }
    }
}
