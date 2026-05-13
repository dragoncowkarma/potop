using UnityEngine;
using Potop.Client.Gameplay.Combat;

namespace Potop.Client.Gameplay.AI.Variants {
    /// <summary>
    /// 플레이어의 공격을 견디며 최전선에서 압박을 가하기 위해 설계된 중장갑 적 변종입니다.
    /// 높은 체력과 방어력, 넉백 저항을 가지는 대신 이동 속도가 느립니다.
    /// </summary>
    public class ArmoredEnemy : EnemyBase {
        [SerializeField] private float _knockbackResistance = 0.5f;
        [SerializeField] private float _damageReductionMultiplier = 0.5f;

        /// <summary>
        /// 장갑의 특성을 반영하여 받는 피해를 감소시키기 위해 피해 처리 로직을 재정의합니다.
        /// 단순한 높은 체력 이상의 내구성을 부여하기 위해 방어력 기반 피해 감소를 적용합니다.
        /// </summary>
        /// <param name="damageInfo">적용할 피해 정보</param>
        public override void TakeDamage(DamageInfo damageInfo) {
            int reducedDamage = Mathf.Max(1, Mathf.RoundToInt(damageInfo.Amount * _damageReductionMultiplier));
            damageInfo.Amount = reducedDamage;
            base.TakeDamage(damageInfo);
        }

        /// <summary>
        /// 중장갑으로 인한 무게감을 표현하고 진형 유지력을 높이기 위해 넉백 저항을 적용합니다.
        /// </summary>
        /// <param name="force">적용할 넉백 힘</param>
        public override void ApplyKnockback(Vector3 force) {
            Vector3 reducedForce = force * (1f - _knockbackResistance);
            // 의존성 주입된 EnemyBase 로직에 축소된 힘 전달
            base.ApplyKnockback(reducedForce);
        }
    }
}
