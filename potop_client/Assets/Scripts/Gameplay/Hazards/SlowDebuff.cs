using System.Collections;
using UnityEngine;
using Potop.Client.Gameplay;

namespace Potop.Client.Gameplay.Hazards {
    /// <summary>
    /// 적에게 부착되어 이동 속도를 느리게 만드는 디버프 컴포넌트입니다.
    /// EnemyBot의 MoveSpeed가 읽기 전용이므로, 물리적인 반대 방향 이동을 가해 속도를 상쇄합니다.
    /// </summary>
    public class SlowDebuff : MonoBehaviour {
        private EnemyBot _targetBot;
        private float _slowFactor;
        private Coroutine _slowRoutine;

        /// <summary>
        /// 적에게 슬로우 디버프를 적용합니다.
        /// </summary>
        /// <param name="duration">슬로우 유지 시간</param>
        /// <param name="slowFactor">감소시킬 속도 비율 (예: 0.5면 50% 느려짐)</param>
        public void ApplySlow(float duration, float slowFactor) {
            _targetBot = GetComponent<EnemyBot>();
            if (_targetBot == null) {
                Destroy(this);
                return;
            }

            _slowFactor = Mathf.Clamp01(slowFactor);

            if (_slowRoutine != null) {
                StopCoroutine(_slowRoutine);
            }
            _slowRoutine = StartCoroutine(SlowRoutine(duration));
        }

        private void Update() {
            // EnemyBot이 매 프레임 forward로 이동하므로, 그 반대 방향으로 이동시켜 속도를 상쇄합니다.
            if (_targetBot != null && _slowFactor > 0) {
                float counterSpeed = _targetBot.MoveSpeed * _slowFactor;
                transform.Translate(-Vector3.forward * counterSpeed * Time.deltaTime, Space.Self);
            }
        }

        private IEnumerator SlowRoutine(float duration) {
            yield return new WaitForSeconds(duration);
            RemoveDebuff();
        }

        private void RemoveDebuff() {
            _slowFactor = 0f;
            Destroy(this);
        }
    }
}
