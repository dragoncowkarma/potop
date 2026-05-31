using UnityEngine;

namespace Potop.Client.Gameplay.Weapons.Strategies {
    /// <summary>
    /// 포물선을 그리며 투사체를 발사하는 전략입니다.
    /// 곡사포나 유탄 발사기 형태의 무기에 사용됩니다.
    /// </summary>
    public class LobFireStrategy : IFireStrategy {
        public void ExecuteFire(WeaponBase weapon) {
            GameObject projectilePrefab = weapon.ProjectilePrefab;
            Transform firePoint = weapon.FirePoint;

            if (projectilePrefab == null || firePoint == null) {
                return;
            }

            float damage = weapon.GetCalculatedDamage();
            float speed = weapon.GetCalculatedProjectileSpeed();
            float launchAngle = weapon.WeaponData != null ? weapon.WeaponData.LaunchAngle : 45f;
            float aoeRadius = weapon.WeaponData != null ? weapon.WeaponData.AoERadius : 3f;

            GameObject projectileObj = null;
            if (Potop.Client.Core.Pooling.PoolManager.Instance != null) {
                projectileObj = Potop.Client.Core.Pooling.PoolManager.Instance.Spawn(projectilePrefab, firePoint.position, firePoint.rotation);
            } else {
                projectileObj = Object.Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            }

            if (projectileObj == null) {
                return;
            }

            if (!projectileObj.TryGetComponent<Rigidbody>(out var rb)) {
                rb = projectileObj.AddComponent<Rigidbody>();
            }
            rb.useGravity = true;
            rb.isKinematic = false;

            Vector3 launchDirection = Quaternion.AngleAxis(-launchAngle, firePoint.right) * firePoint.forward;
            rb.linearVelocity = launchDirection * speed;

            if (projectileObj.TryGetComponent<Projectile>(out var projectile)) {
                projectile.Initialize(speed, Mathf.RoundToInt(damage), aoeRadius, 0, 5f);
            }
        }
    }
}
