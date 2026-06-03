using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Potop.Client.Core.Events;
using Potop.Client.Gameplay.Flow;


namespace Potop.Client.Core {
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
        /// 게임 오버 여부를 반환합니다.
        /// </summary>
        public bool IsGameOver => _isGameOver;

        /// <summary>
        /// 게임이 현재 플레이 중인지 여부를 반환합니다.
        /// </summary>
        public bool IsPlaying {
            get {
                if (CoreFlowBridge.GetCurrentState == null) return false;
                var state = CoreFlowBridge.GetCurrentState();
                return state == GameFlowState.InGame || 
                       state == GameFlowState.BossBattle || 
                       state == GameFlowState.Overclock;
            }
        }

        /// <summary>
        /// 플레이어의 Transform 위치 정보를 제공합니다.
        /// </summary>
        public Transform PlayerTransform { get; set; }

        // Events
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
            ChangeState(GameFlowState.Lobby);
            StartGame();
        }

        /// <summary>
        /// 게임 상태를 변경합니다.
        /// </summary>
        /// <param name="newState">새로운 게임 상태</param>
        public void ChangeState(GameFlowState newState) {
            if (CoreFlowBridge.TransitionTo != null) {
                CoreFlowBridge.TransitionTo(newState);
            }

            bool isPlayingState = newState == GameFlowState.InGame || 
                                 newState == GameFlowState.BossBattle || 
                                 newState == GameFlowState.Overclock;

            if (isPlayingState) {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            } else {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        /// <summary>
        /// 게임을 초기 상태로 시작합니다.
        /// </summary>
        public void StartGame() {
            if (CoreMetaBridge.IsMetaUpgradeManagerActive == null || !CoreMetaBridge.IsMetaUpgradeManagerActive()) {
                Debug.LogWarning("MetaUpgradeManager is null during StartGame.");
            }
            if (CoreMetaBridge.IsGemWalletActive == null || !CoreMetaBridge.IsGemWalletActive()) {
                Debug.LogWarning("GemWallet is null during StartGame.");
            }

            Score = 0;
            _isGameOver = false;
            ChangeState(GameFlowState.InGame);
            TimeController.ResetTimeEffects();

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
            ChangeState(GameFlowState.Result);
            TimeController.SetBaseTimeScale(GAME_OVER_TIME_SCALE);

            OnGameOver?.Invoke();
        }

        /// <summary>
        /// 현재 씬을 다시 로드하여 게임을 재시작합니다.
        /// </summary>
        public void RestartGame() {
            TimeController.ResetTimeEffects();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        /// <summary>
        /// 시작 메뉴 씬으로 이동합니다.
        /// </summary>
        public void GoToMainMenu() {
            TimeController.ResetTimeEffects();
            SceneManager.LoadScene(START_SCENE_NAME);
        }

        private void OnDestroy() {
            if (Instance == this) {
                Instance = null;
            }
        }
    }
}
