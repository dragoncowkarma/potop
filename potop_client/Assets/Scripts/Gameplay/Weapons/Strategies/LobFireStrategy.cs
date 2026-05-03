using UnityEngine;

namespace Potop.Client.Gameplay.Weapons.Strategies {
    /// <summary>
    /// 포물선을 그리며 투사체를 발사하는 전략입니다.
    /// 곡사포나 유탄 발사기 형태의 무기에 사용됩니다.
    /// </summary>
    public class LobFireStrategy : IFireStrategy {
        public void ExecuteFire(WeaponBase weapon) {
            float damage = weapon.GetCalculatedDamage();
            float speed = weapon.GetCalculatedProjectileSpeed();

#if UNITY_EDITOR
            Debug.Log($"[LobFireStrategy] 곡사 발사! 피해량: {damage}, 속도: {speed}");
#endif
            // TODO: 투사체에 중력의 영향을 받도록 물리 기반 초기화를 수행하여 포물선 궤도 구현
        }
    }
}
