using UnityEngine;
using Unity.Cinemachine;
using Potop.Client.Core.Events;

namespace Potop.Client.Core.Camera {
    /// <summary>
    /// Cinemachine Multi-Channel Perlin을 사용하여 프레임 레이트에 독립적인 카메라 흔들림 효과를 제어하는 컨트롤러입니다.
    /// 전투 이벤트를 구독하여 타격 강도와 유형에 따라 흔들림과 히트스톱을 발생시킵니다.
    /// </summary>
    public class CameraShakeController : MonoBehaviour {
        [System.Serializable]
        public struct ShakePreset {
            public float HitStopDuration;
            public float HitStopScale;
            public float ShakeAmplitude;
            public float ShakeFrequency;
            public float ShakeDuration;
        }

        [Header("Cinemachine Integration")]
        [SerializeField] private CinemachineCamera _cinemachineCamera;

        [Header("Cooldown (Coalescence)")]
        [SerializeField] private float _cooldownWindow = 0.05f;

        [Header("Combat Juice Presets")]
        [SerializeField] private ShakePreset _normalHitPreset = new ShakePreset {
            HitStopDuration = 0.08f,
            HitStopScale = 0.1f,
            ShakeAmplitude = 1.0f,
            ShakeFrequency = 1.5f,
            ShakeDuration = 0.2f
        };

        [SerializeField] private ShakePreset _heavyHitPreset = new ShakePreset {
            HitStopDuration = 0.18f,
            HitStopScale = 0.05f,
            ShakeAmplitude = 2.5f,
            ShakeFrequency = 2.5f,
            ShakeDuration = 0.4f
        };

        private CinemachineBasicMultiChannelPerlin _multiChannelPerlin;
        private float _lastEventTime = -999f;

        private float _shakeTimer = 0f;
        private float _shakeDuration = 0f;
        private float _shakeAmplitude = 0f;
        private float _shakeFrequency = 0f;

        private void Awake() {
            // Find references
            if (_cinemachineCamera == null) {
                _cinemachineCamera = GetComponent<CinemachineCamera>();
            }
        }

        private void Start() {
            if (_cinemachineCamera == null) {
                _cinemachineCamera = FindFirstObjectByType<CinemachineCamera>();
            }
            if (_cinemachineCamera != null) {
                _multiChannelPerlin = _cinemachineCamera.GetCinemachineComponent(CinemachineCore.Stage.Noise) as CinemachineBasicMultiChannelPerlin;
            }
        }

        private void OnEnable() {
            EventBroker.Subscribe<CombatImpactEvent>(HandleCombatImpact);
        }

        private void OnDisable() {
            EventBroker.Unsubscribe<CombatImpactEvent>(HandleCombatImpact);
            SetNoiseGain(0f, 0f);
        }

        private void Update() {
            if (_shakeTimer > 0f) {
                _shakeTimer -= Time.unscaledDeltaTime;
                if (_shakeTimer <= 0f) {
                    _shakeTimer = 0f;
                    SetNoiseGain(0f, 0f);
                } else {
                    // unscaled time 기반 선형 감쇠 계산
                    float progress = _shakeTimer / _shakeDuration;
                    float currentAmplitude = _shakeAmplitude * progress;
                    SetNoiseGain(currentAmplitude, _shakeFrequency);
                }
            }
        }

        private void SetNoiseGain(float amplitude, float frequency) {
            if (_multiChannelPerlin != null) {
                _multiChannelPerlin.AmplitudeGain = amplitude;
                _multiChannelPerlin.FrequencyGain = frequency;
            }
        }

        /// <summary>
        /// 전투 타격 이벤트를 처리하여 히트스톱과 흔들림을 유발합니다.
        /// </summary>
        /// <param name="evt">타격 정보 이벤트 데이터</param>
        private void HandleCombatImpact(CombatImpactEvent evt) {
            // 짧은 쿨다운 윈도우 내의 연속 이벤트를 병합(생략)하여 스토로브 현상을 방지
            if (Time.unscaledTime - _lastEventTime < _cooldownWindow) {
                return;
            }
            _lastEventTime = Time.unscaledTime;

            ShakePreset preset = evt.IsHeavy ? _heavyHitPreset : _normalHitPreset;

            // 중앙 집중식 타임 컨트롤러에 hitstop 요청
            TimeController.TriggerHitStop(preset.HitStopDuration, preset.HitStopScale);

            // 프레임 레이트 독립적인 흔들림 활성화
            TriggerShake(preset.ShakeAmplitude, preset.ShakeFrequency, preset.ShakeDuration);
        }

        /// <summary>
        /// 직접 진폭, 주파수, 지속 시간을 받아 카메라 흔들림을 발생시킵니다.
        /// </summary>
        public void TriggerShake(float amplitude, float frequency, float duration) {
            _shakeAmplitude = amplitude;
            _shakeFrequency = frequency;
            _shakeDuration = duration;
            _shakeTimer = duration;

            SetNoiseGain(amplitude, frequency);
        }

        /// <summary>
        /// 지정된 강도로 카메라 흔들림을 트리거합니다. (하위 호환용)
        /// </summary>
        /// <param name="intensity">흔들림 강도</param>
        public void TriggerShake(float intensity = 1f) {
            TriggerShake(intensity * _normalHitPreset.ShakeAmplitude, _normalHitPreset.ShakeFrequency, _normalHitPreset.ShakeDuration);
        }
    }
}


