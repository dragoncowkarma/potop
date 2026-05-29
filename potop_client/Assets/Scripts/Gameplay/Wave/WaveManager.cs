using System.Collections.Generic;
using UnityEngine;
using Potop.Client.Core.Events;

namespace Potop.Client.Gameplay.Wave {
    /// <summary>
    /// 웨이브가 시작되었을 때 발생하는 이벤트입니다.
    /// UI 갱신 및 스포너 시스템에 웨이브 시작을 알리기 위해 사용됩니다.
    /// </summary>
    public struct WaveStartedEvent {
        /// <summary>
        /// 시작된 웨이브의 인덱스입니다.
        /// </summary>
        public int WaveIndex;
    }

    /// <summary>
    /// 웨이브가 완료되었을 때 발생하는 이벤트입니다.
    /// 게임 진행 논리(대기 시간 시작, 보상 지급 등)를 구동하기 위해 사용됩니다.
    /// </summary>
    public struct WaveCompletedEvent {
        /// <summary>
        /// 완료된 웨이브의 인덱스입니다.
        /// </summary>
        public int WaveIndex;
    }

    /// <summary>
    /// 게임 내 웨이브 진행을 관리하는 클래스입니다.
    /// WaveData 리스트를 순차적으로 진행하며 타이머 기반으로 웨이브 시작 및 완료 이벤트를 발생시킵니다.
    /// </summary>
    public class WaveManager : MonoBehaviour {
        [SerializeField] private List<WaveData> _waves = new List<WaveData>();

        /// <summary>
        /// 웨이브 간 기본 대기 시간(초)입니다.
        /// 매직 넘버 사용을 피하기 위해 인스펙터에서 설정 가능한 변수로 노출합니다.
        /// </summary>
        [SerializeField] private float _defaultWaveDelay = 5f;

        private int _currentWaveIndex = 0;
        private float _waveTimer = 0f;
        private float _delayTimer = 0f;
        private bool _isWaveActive = false;
        private bool _isGameComplete = false;
        private bool _isOverclockActive = false;
        private float _overclockSpawnInterval = 1f;

        public bool IsOverclockActive => _isOverclockActive;
        public float OverclockSpawnInterval => _overclockSpawnInterval;

        public float CurrentWaveProgress => _isWaveActive && _currentWaveIndex < _waves.Count 
            ? 1f - (_waveTimer / _waves[_currentWaveIndex].Duration) 
            : 0f;
        
        public WaveData CurrentWaveData => _currentWaveIndex < _waves.Count ? _waves[_currentWaveIndex] : null;
        public bool IsWaveActive => _isWaveActive;

        private void Start() {
            if (_waves == null || _waves.Count == 0) {
#if UNITY_EDITOR
                Debug.LogWarning("WaveManager: No waves defined in the inspector!");
#endif
                _isGameComplete = true;
                return;
            }

            // 초기 대기 시간을 설정하여 첫 웨이브 시작을 준비합니다.
            _delayTimer = _defaultWaveDelay;
        }

        private void Update() {
            if (_isOverclockActive) {
                _isWaveActive = true;
                _isGameComplete = false;
                return;
            }

            if (_isGameComplete) {
                return;
            }

            if (_isWaveActive) {
                UpdateWaveTimer();
            } else {
                UpdateDelayTimer();
            }
        }

        /// <summary>
        /// 웨이브 진행 시간을 계산하고 완료 조건에 도달했는지 확인합니다.
        /// </summary>
        private void UpdateWaveTimer() {
            _waveTimer -= Time.deltaTime;

            if (_waveTimer <= 0f) {
                CompleteCurrentWave();
            }
        }

        /// <summary>
        /// 다음 웨이브 시작 전 대기 시간을 계산하고 웨이브를 시작합니다.
        /// </summary>
        private void UpdateDelayTimer() {
            _delayTimer -= Time.deltaTime;

            if (_delayTimer <= 0f) {
                StartNextWave();
            }
        }

        /// <summary>
        /// 다음 웨이브를 시작하고 관련 시스템에 이벤트를 전송합니다.
        /// </summary>
        private void StartNextWave() {
            if (_currentWaveIndex >= _waves.Count) {
                _isGameComplete = true;
                return;
            }

            var currentWave = _waves[_currentWaveIndex];
            if (currentWave == null) {
#if UNITY_EDITOR
                Debug.LogError($"WaveData at index {_currentWaveIndex} is missing!");
#endif
                _isGameComplete = true;
                return;
            }

            _isWaveActive = true;
            _waveTimer = currentWave.Duration;

            EventBroker.Publish(new WaveStartedEvent { WaveIndex = _currentWaveIndex });
        }

        /// <summary>
        /// 현재 웨이브를 종료하고 대기 상태로 전환하며 이벤트를 전송합니다.
        /// </summary>
        private void CompleteCurrentWave() {
            _isWaveActive = false;

            EventBroker.Publish(new WaveCompletedEvent { WaveIndex = _currentWaveIndex });

            _currentWaveIndex++;

            if (_currentWaveIndex >= _waves.Count) {
                _isGameComplete = true;
            } else {
                _delayTimer = _defaultWaveDelay;
            }
        }

        /// <summary>
        /// 오버클럭 모드를 시작하고 무한 스폰 모드로 전환합니다.
        /// </summary>
        /// <param name="spawnInterval">스폰 간격</param>
        public void StartOverclockMode(float spawnInterval) {
            _isOverclockActive = true;
            _isGameComplete = false;
            _isWaveActive = true;
            _overclockSpawnInterval = spawnInterval;
            if (_currentWaveIndex >= _waves.Count) {
                _currentWaveIndex = Mathf.Max(0, _waves.Count - 1);
            }
        }
    }
}
