using System;
using UnityEngine;
using Potop.Client.Core;
using Potop.Client.Core.Events;
using Potop.Client.Gameplay.Meta;

namespace Potop.Client.Gameplay.Flow {
    /// <summary>
    /// 단일 씬 내에서 게임의 로비, 캐릭터 선택, 인게임, 보스전, 오버클럭, 결과 화면 상태 전환을 관리하고,
    /// 게임 결과 메트릭(처치 수, 최대 웨이브, 획득 보석, 생존 시간)을 집계하는 컨트롤러입니다.
    /// </summary>
    public class GameFlowController : MonoBehaviour {
        public static GameFlowController Instance { get; private set; }

        private GameFlowState _currentState = GameFlowState.Lobby;
        public GameFlowState CurrentState => _currentState;

        // 게임 메트릭
        private int _kills;
        private int _maxWaves;
        private int _gemsEarned;
        private float _survivalTime;
        private SettlementData _settlementData;

        // 동적 UI 결과 인스턴스
        private GameObject _resultUiInstance;
        private bool _isSubscribedToGameplay = false;

        // 테스트 용이성을 위한 dynamic instantiator/destroyer 델리게이트
        public Func<string, GameObject> UIInstantiator { get; set; } = (path) => {
            var prefab = Resources.Load<GameObject>(path);
            return prefab != null ? Instantiate(prefab) : null;
        };

        public Action<GameObject> UIDestroyer { get; set; } = (obj) => {
            Destroy(obj);
        };

        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void OnDisable() {
            UnsubscribeGameplayEvents();
            CleanupUI();
        }

        private void Update() {
            if (_currentState == GameFlowState.InGame || 
                _currentState == GameFlowState.BossBattle || 
                _currentState == GameFlowState.Overclock) {
                _survivalTime += Time.deltaTime;
            }
        }

        /// <summary>
        /// 새로운 상태로 게임 흐름을 전환합니다.
        /// </summary>
        public void TransitionTo(GameFlowState newState) {
            if (_currentState == newState) return;

            GameFlowState previousState = _currentState;
            _currentState = newState;

            HandleExitState(previousState);
            HandleEnterState(newState);

            EventBroker.Publish(new GameFlowStateChangedEvent {
                PreviousState = previousState,
                NewState = newState
            });
        }

        private void HandleExitState(GameFlowState state) {
            switch (state) {
                case GameFlowState.Lobby:
                    _kills = 0;
                    _maxWaves = 0;
                    _gemsEarned = 0;
                    _survivalTime = 0f;
                    _settlementData = null;
                    break;
            }
        }

        private void HandleEnterState(GameFlowState state) {
            switch (state) {
                case GameFlowState.Lobby:
                    CleanupUI();
                    UnsubscribeGameplayEvents();
                    break;
                case GameFlowState.SelectTurret:
                case GameFlowState.InGame:
                case GameFlowState.BossBattle:
                case GameFlowState.Overclock:
                    SubscribeGameplayEvents();
                    break;
                case GameFlowState.Result:
                    UnsubscribeGameplayEvents();
                    ShowResult();
                    break;
            }
        }

        private void SubscribeGameplayEvents() {
            if (_isSubscribedToGameplay) return;
            _isSubscribedToGameplay = true;

            EventBroker.Subscribe<EnemyDiedEvent>(OnEnemyDied);
            EventBroker.Subscribe<Potop.Client.Gameplay.Wave.WaveStartedEvent>(OnWaveStarted);
            EventBroker.Subscribe<Potop.Client.Gameplay.Progression.EXPCollectedEvent>(OnEXPCollected);
            EventBroker.Subscribe<BossDefeatedEvent>(OnBossDefeated);

            if (GameManager.Instance != null) {
                GameManager.OnGameOver += OnGameOver;
            }
        }

        private void UnsubscribeGameplayEvents() {
            if (!_isSubscribedToGameplay) return;
            _isSubscribedToGameplay = false;

            EventBroker.Unsubscribe<EnemyDiedEvent>(OnEnemyDied);
            EventBroker.Unsubscribe<Potop.Client.Gameplay.Wave.WaveStartedEvent>(OnWaveStarted);
            EventBroker.Unsubscribe<Potop.Client.Gameplay.Progression.EXPCollectedEvent>(OnEXPCollected);
            EventBroker.Unsubscribe<BossDefeatedEvent>(OnBossDefeated);

            if (GameManager.Instance != null) {
                GameManager.OnGameOver -= OnGameOver;
            }
        }

        private void OnEnemyDied(EnemyDiedEvent e) {
            _kills++;
        }

        private void OnWaveStarted(Potop.Client.Gameplay.Wave.WaveStartedEvent e) {
            _maxWaves = Mathf.Max(_maxWaves, e.WaveIndex);
        }

        private void OnEXPCollected(Potop.Client.Gameplay.Progression.EXPCollectedEvent e) {
            _gemsEarned += 1;
        }

        private void OnBossDefeated(BossDefeatedEvent e) {
            TransitionTo(GameFlowState.Overclock);
        }

        private void OnGameOver() {
            TransitionTo(GameFlowState.Result);
        }

        private void ShowResult() {
            _settlementData = new SettlementData {
                Kills = _kills,
                MaxWaves = _maxWaves,
                GemsEarned = _gemsEarned,
                SurvivalTime = _survivalTime
            };

            if (GemWallet.Instance != null) {
                GemWallet.Instance.Earn(_settlementData.GemsEarned);
            }

            var uiObj = UIInstantiator("Prefabs/UI/ResultUI");
            if (uiObj != null) {
                _resultUiInstance = uiObj;
                var uiController = uiObj.GetComponent<Potop.Client.UI.ResultUIController>();
                if (uiController != null) {
                    uiController.Setup(_settlementData, () => TransitionTo(GameFlowState.Lobby));
                }
            }
        }

        private void CleanupUI() {
            if (_resultUiInstance != null) {
                UIDestroyer(_resultUiInstance);
                _resultUiInstance = null;
            }
        }
    }
}
