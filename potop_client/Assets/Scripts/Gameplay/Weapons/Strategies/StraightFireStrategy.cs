using UnityEngine;

namespace Potop.Client.Gameplay.Weapons.Strategies {
    /// <summary>
    /// 직선으로 투사체를 발사하는 전략입니다.
    /// 기본적이고 직관적인 사격 방식에 사용됩니다.
    /// </summary>
    public class StraightFireStrategy : IFireStrategy {
        public void ExecuteFire(WeaponBase weapon) {
            GameObject projectilePrefab = weapon.ProjectilePrefab;
            Transform firePoint = weapon.FirePoint;

            if (projectilePrefab == null || firePoint == null) {
#if UNITY_EDITOR
                if (projectilePrefab == null) Debug.LogWarning("[StraightFireStrategy] ProjectilePrefab이 설정되지 않았습니다.");
                if (firePoint == null) Debug.LogWarning("[StraightFireStrategy] FirePoint가 설정되지 않았습니다.");
#endif
                return;
            }

            float damage = weapon.GetCalculatedDamage();
            float speed = weapon.GetCalculatedProjectileSpeed();

#if UNITY_EDITOR
            Debug.Log($"[StraightFireStrategy] 직선 발사! 피해량: {damage}, 속도: {speed}");
#endif
            // Object Pooling을 이용한 투사체 생성
            GameObject projectileObj = Potop.Client.Core.Pooling.PoolManager.Instance.Spawn(projectilePrefab, firePoint.position, firePoint.rotation);

            // 투사체 초기화
            if (projectileObj.TryGetComponent<Projectile>(out var projectile)) {
                projectile.Initialize(speed, Mathf.RoundToInt(damage));
            }
        }
    }
}
