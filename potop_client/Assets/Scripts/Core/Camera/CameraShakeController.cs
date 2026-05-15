using UnityEngine;
using Unity.Cinemachine;
using Potop.Client.Core.Events;

namespace Potop.Client.Core.Camera {
    /// <summary>
    /// Cinemachine Impulse를 사용하여 카메라 흔들림 효과를 제어하는 컨트롤러입니다.
    /// 전투 이벤트를 구독하여 타격 강도에 따라 흔들림을 발생시킵니다.
    /// </summary>
    [RequireComponent(typeof(CinemachineImpulseSource))]
    public class CameraShakeController : MonoBehaviour {
        private CinemachineImpulseSource _impulseSource;

        private void Awake() {
            _impulseSource = GetComponent<CinemachineImpulseSource>();
        }

        private void OnEnable() {
            EventBroker.Subscribe<CombatImpactEvent>(HandleCombatImpact);
        }

        private void OnDisable() {
            EventBroker.Unsubscribe<CombatImpactEvent>(HandleCombatImpact);
        }

        private const float SHAKE_INTENSITY_THRESHOLD = 0.5f;

        /// <summary>
        /// 전투 타격 이벤트를 처리하여 카메라 흔들림을 유발합니다.
        /// </summary>
        /// <param name="evt">타격 정보 이벤트 데이터</param>
        private void HandleCombatImpact(CombatImpactEvent evt) {
            // 중량 타격이거나 강도가 일정 수준 이상일 때 흔들림 발생
            if (evt.IsHeavy || evt.Intensity > SHAKE_INTENSITY_THRESHOLD) {
                TriggerShake(evt.Intensity);
            }
        }

        /// <summary>
        /// 지정된 강도로 카메라 흔들림을 트리거합니다.
        /// </summary>
        /// <param name="intensity">흔들림 강도</param>
        public void TriggerShake(float intensity = 1f) {
            if (_impulseSource != null) {
                _impulseSource.GenerateImpulseWithForce(intensity);
            }
        }
    }
}

