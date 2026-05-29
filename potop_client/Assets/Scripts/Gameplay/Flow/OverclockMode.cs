using UnityEngine;
using Potop.Client.Core.Events;
using Potop.Client.Data;
using Potop.Client.Gameplay.Wave;

namespace Potop.Client.Gameplay.Flow {
    /// <summary>
    /// 무한 적 스케일링을 관리하는 오버클럭 모드 시스템입니다.
    /// </summary>
    public class OverclockMode : MonoBehaviour {
        public static OverclockMode Instance { get; private set; }

        [SerializeField] private OverclockData _overclockData;
        [SerializeField] private WaveManager _waveManager;

        private bool _isActive = false;
        private float _timer = 0f;
        private int _scaleCount = 0;

        private float _currentHpMultiplier = 1f;
        private float _currentSpeedMultiplier = 1f;
        private float _currentDamageMultiplier = 1f;

        public bool IsActive => _isActive;
        public float CurrentHpMultiplier => _currentHpMultiplier;
        public float CurrentSpeedMultiplier => _currentSpeedMultiplier;
        public float CurrentDamageMultiplier => _currentDamageMultiplier;

        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void OnEnable() {
            EventBroker.Subscribe<BossDefeatedEvent>(OnBossDefeated);
        }

        private void OnDisable() {
            EventBroker.Unsubscribe<BossDefeatedEvent>(OnBossDefeated);
        }

        private void Start() {
            if (_waveManager == null) {
                _waveManager = FindFirstObjectByType<WaveManager>();
            }

            if (_overclockData != null) {
                _currentHpMultiplier = _overclockData.BaseHpMultiplier;
                _currentSpeedMultiplier = _overclockData.BaseSpeedMultiplier;
                _currentDamageMultiplier = _overclockData.BaseDamageMultiplier;
            }
        }

        private void Update() {
            if (!_isActive) return;
            if (_overclockData == null) return;

            _timer += Time.deltaTime;
            if (_timer >= _overclockData.ScalingInterval) {
                _timer -= _overclockData.ScalingInterval;
                ScaleStats();
            }
        }

        private void OnBossDefeated(BossDefeatedEvent evt) {
            StartOverclock();
        }

        public void StartOverclock() {
            if (_isActive) return;

            _isActive = true;
            _timer = 0f;
            _scaleCount = 0;

            if (_overclockData != null) {
                _currentHpMultiplier = _overclockData.BaseHpMultiplier;
                _currentSpeedMultiplier = _overclockData.BaseSpeedMultiplier;
                _currentDamageMultiplier = _overclockData.BaseDamageMultiplier;

                if (_waveManager != null) {
                    _waveManager.StartOverclockMode(_overclockData.OverclockSpawnInterval);
                }
            } else {
                if (_waveManager != null) {
                    _waveManager.StartOverclockMode(1f);
                }
            }
        }

        private void ScaleStats() {
            if (_overclockData == null) return;

            _scaleCount++;

            float rawHpMult = Mathf.Pow(_overclockData.HpScalingFactor, _scaleCount) * _overclockData.BaseHpMultiplier;
            float rawSpeedMult = Mathf.Pow(_overclockData.SpeedScalingFactor, _scaleCount) * _overclockData.BaseSpeedMultiplier;
            float rawDamageMult = Mathf.Pow(_overclockData.DamageScalingFactor, _scaleCount) * _overclockData.BaseDamageMultiplier;

            const float MAX_MULTIPLIER_LIMIT = 1e9f;

            _currentHpMultiplier = Mathf.Min(rawHpMult, MAX_MULTIPLIER_LIMIT);
            _currentSpeedMultiplier = Mathf.Min(rawSpeedMult, MAX_MULTIPLIER_LIMIT);
            _currentDamageMultiplier = Mathf.Min(rawDamageMult, MAX_MULTIPLIER_LIMIT);
        }
    }
}
