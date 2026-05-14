using System.Collections;
using UnityEngine;
using Potop.Client.Gameplay.Combat;

namespace Potop.Client.Gameplay.Hazards {
    /// <summary>
    /// 전술적 이점을 제공하는 상호작용 가능한 환경 요소의 기본 클래스입니다.
    /// 플레이어의 공격에 의해 활성화되어 적에게 피해를 주거나 특수한 효과를 발생시킵니다.
    /// </summary>
    public abstract class EnvironmentalHazard : MonoBehaviour, IDamageable {
        [SerializeField] protected float _activationDelay = 0.1f;
        protected bool _isActivated = false;

        public virtual void TakeDamage(DamageInfo info) {
            if (_isActivated) return;

            _isActivated = true;
            if (_activationDelay > 0) {
                StartCoroutine(DelayedActivation(info));
            } else {
                ActivateHazard(info);
            }
        }

        private IEnumerator DelayedActivation(DamageInfo info) {
            yield return new WaitForSeconds(_activationDelay);
            ActivateHazard(info);
        }

        /// <summary>
        /// 환경 요소를 초기 상태로 되돌립니다.
        /// 오브젝트 풀링을 사용할 때 재사용하기 위해 호출됩니다.
        /// </summary>
        protected virtual void OnEnable() {
            _isActivated = false;
        }

        /// <summary>
        /// 구체적인 환경 요소가 활성화되었을 때의 동작을 정의해야 하는 추상 메서드입니다.
        /// </summary>
        /// <param name="info">활성화의 원인이 된 피해 정보</param>
        protected abstract void ActivateHazard(DamageInfo info);
    }
}
