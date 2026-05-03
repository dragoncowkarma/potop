using UnityEngine;

namespace Potop.Client.Gameplay.Weapons.Strategies {
    /// <summary>
    /// 산탄형으로 여러 투사체를 흩뿌리며 발사하는 전략입니다.
    /// 샷건이나 분산 공격 무기에 사용됩니다.
    /// </summary>
    public class SpreadFireStrategy : IFireStrategy {
        public void ExecuteFire(WeaponBase weapon) {
            float damage = weapon.GetCalculatedDamage();
            float speed = weapon.GetCalculatedProjectileSpeed();

#if UNITY_EDITOR
            Debug.Log($"[SpreadFireStrategy] 산탄 발사! 피해량: {damage}, 속도: {speed}");
#endif
            // TODO: 무기 파츠의 스프레드 감소율을 반영하여 다수의 투사체를 각기 다른 각도로 생성 및 초기화
        }
    }
}
