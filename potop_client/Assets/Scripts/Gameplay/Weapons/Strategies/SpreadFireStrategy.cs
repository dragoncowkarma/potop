using UnityEngine;

namespace Potop.Client.Gameplay.Weapons.Strategies {
    /// <summary>
    /// 산탄형으로 여러 투사체를 흩뿌리며 발사하는 전략입니다.
    /// 샷건이나 분산 공격 무기에 사용됩니다.
    /// </summary>
    public class SpreadFireStrategy : IFireStrategy {
        public void ExecuteFire(WeaponBase weapon) {
            GameObject projectilePrefab = weapon.ProjectilePrefab;
            Transform firePoint = weapon.FirePoint;

            if (projectilePrefab == null || firePoint == null) {
                return;
            }

            float damage = weapon.GetCalculatedDamage();
            float speed = weapon.GetCalculatedProjectileSpeed();

            int count = weapon.WeaponData != null ? weapon.WeaponData.SpreadProjectileCount : 3;
            float spreadAngle = weapon.WeaponData != null ? weapon.WeaponData.SpreadAngle : 10f;

            for (int i = 0; i < count; i++) {
                float angle = (i - (count - 1) / 2f) * spreadAngle;
                Quaternion projectileRotation = firePoint.rotation * Quaternion.Euler(0f, angle, 0f);

                GameObject projectileObj = null;
                if (Potop.Client.Core.Pooling.PoolManager.Instance != null) {
                    projectileObj = Potop.Client.Core.Pooling.PoolManager.Instance.Spawn(projectilePrefab, firePoint.position, projectileRotation);
                } else {
                    projectileObj = Object.Instantiate(projectilePrefab, firePoint.position, projectileRotation);
                }

                if (projectileObj != null && projectileObj.TryGetComponent<Projectile>(out var projectile)) {
                    projectile.Initialize(speed, Mathf.RoundToInt(damage));
                }
            }
        }
    }
}
