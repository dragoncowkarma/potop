using UnityEngine;

namespace Potop.Client.Gameplay.Weapons.Strategies {
    /// <summary>
    /// 직선으로 투사체를 발사하는 전략입니다.
    /// 기본적이고 직관적인 사격 방식에 사용됩니다.
    /// </summary>
    public class StraightFireStrategy : IFireStrategy {
        public void ExecuteFire(WeaponBase weapon) {
            float damage = weapon.GetCalculatedDamage();
            float speed = weapon.GetCalculatedProjectileSpeed();

#if UNITY_EDITOR
            Debug.Log($"[StraightFireStrategy] 직선 발사! 피해량: {damage}, 속도: {speed}");
#endif
            // TODO: Object Pooling을 이용한 실제 투사체 생성 및 초기화 로직 구현
        }
    }
}
