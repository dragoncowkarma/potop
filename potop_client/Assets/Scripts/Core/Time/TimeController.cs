using UnityEngine;

namespace Potop.Client.Core {
    /// <summary>
    /// 타임스케일(시간 속도) 변경을 중앙 집중식으로 관리하는 컨트롤러입니다.
    /// Hitstop, Slow-motion, Pause 등의 여러 타임스케일 효과가 중첩(Nested)될 때 
    /// 이전 상태를 잃지 않고 올바른 타임스케일로 복원하는 역할을 합니다.
    /// </summary>
    public class TimeController : MonoBehaviour {
        public static TimeController Instance { get; private set; }

        private float _baseTimeScale = 1f;
        private int _pauseCount = 0;
        
        private float _hitStopTimer = 0f;
        private float _hitStopScale = 1f;

        private float _slowMotionTimer = 0f;
        private float _slowMotionScale = 1f;

        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void OnDestroy() {
            if (Instance == this) {
                Instance = null;
            }
        }

        private void Update() {
            float dt = Time.unscaledDeltaTime;

            if (_hitStopTimer > 0f) {
                _hitStopTimer -= dt;
                if (_hitStopTimer <= 0f) {
                    _hitStopTimer = 0f;
                }
            }

            if (_slowMotionTimer > 0f) {
                _slowMotionTimer -= dt;
                if (_slowMotionTimer <= 0f) {
                    _slowMotionTimer = 0f;
                }
            }

            UpdateTimeScale();
        }

        /// <summary>
        /// 현재 활성화된 효과들을 평가하여 최종 타임스케일을 적용합니다.
        /// </summary>
        public void UpdateTimeScale() {
            float targetScale = _baseTimeScale;

            if (_pauseCount > 0) {
                targetScale = 0f;
            } else if (_hitStopTimer > 0f) {
                targetScale = _hitStopScale;
            } else if (_slowMotionTimer > 0f) {
                targetScale = _slowMotionScale;
            }

            Time.timeScale = targetScale;
        }

        // --- 인스턴스 메서드 ---

        public void SetBaseTimeScaleInstance(float scale) {
            _baseTimeScale = scale;
            UpdateTimeScale();
        }

        public void RequestPauseInstance() {
            _pauseCount++;
            UpdateTimeScale();
        }

        public void RemovePauseInstance() {
            _pauseCount = Mathf.Max(0, _pauseCount - 1);
            UpdateTimeScale();
        }

        public void TriggerHitStopInstance(float duration, float scale) {
            // 더 긴 지속 시간을 우선 적용하고, 지속 시간이 같으면 더 강한(더 낮은) 타임스케일을 적용합니다.
            if (duration > _hitStopTimer) {
                _hitStopTimer = duration;
                _hitStopScale = scale;
            } else if (Mathf.Approximately(duration, _hitStopTimer)) {
                _hitStopScale = Mathf.Min(_hitStopScale, scale);
            }
            UpdateTimeScale();
        }

        public void TriggerSlowMotionInstance(float duration, float scale) {
            _slowMotionTimer = duration;
            _slowMotionScale = scale;
            UpdateTimeScale();
        }

        public void ClearSlowMotionInstance() {
            _slowMotionTimer = 0f;
            UpdateTimeScale();
        }

        public void ResetTimeEffectsInstance() {
            _baseTimeScale = 1f;
            _pauseCount = 0;
            _hitStopTimer = 0f;
            _hitStopScale = 1f;
            _slowMotionTimer = 0f;
            _slowMotionScale = 1f;
            UpdateTimeScale();
        }

        // --- 정적 래퍼 메서드 (단위 테스트 및 정적 접근용 Fallback 포함) ---

        public static void SetBaseTimeScale(float scale) {
            if (Instance != null) {
                Instance.SetBaseTimeScaleInstance(scale);
            } else {
                Time.timeScale = scale;
            }
        }

        public static void RequestPause() {
            if (Instance != null) {
                Instance.RequestPauseInstance();
            } else {
                Time.timeScale = 0f;
            }
        }

        public static void RemovePause() {
            if (Instance != null) {
                Instance.RemovePauseInstance();
            } else {
                Time.timeScale = 1f;
            }
        }

        public static void TriggerHitStop(float duration, float scale = 0.05f) {
            if (Instance != null) {
                Instance.TriggerHitStopInstance(duration, scale);
            } else {
                Time.timeScale = scale;
            }
        }

        public static void TriggerSlowMotion(float duration, float scale = 0.1f) {
            if (Instance != null) {
                Instance.TriggerSlowMotionInstance(duration, scale);
            } else {
                Time.timeScale = scale;
            }
        }

        public static void ClearSlowMotion() {
            if (Instance != null) {
                Instance.ClearSlowMotionInstance();
            } else {
                Time.timeScale = 1f;
            }
        }

        public static void ResetTimeEffects() {
            if (Instance != null) {
                Instance.ResetTimeEffectsInstance();
            } else {
                Time.timeScale = 1f;
            }
        }
    }
}
