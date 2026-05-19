using UnityEngine;
using Potop.Client.Gameplay.Weapons.Strategies;

namespace Potop.Client.Gameplay.Weapons.Overdrive
{
    /// <summary>
    /// 개틀링 레일건 발사 전략입니다.
    /// 초고속 무한 관통 투사체를 발사합니다.
    /// </summary>
    public class GatlingRailgunFireStrategy : IFireStrategy
    {
        private const int INFINITE_PIERCE = 9999;
        private const float SPEED_MULTIPLIER = 5f;

        public void ExecuteFire(WeaponBase weapon)
        {
            GameObject projectilePrefab = weapon.ProjectilePrefab;
            Transform firePoint = weapon.FirePoint;

            if (projectilePrefab == null || firePoint == null) return;

            float damage = weapon.GetCalculatedDamage();
            float speed = weapon.GetCalculatedProjectileSpeed() * SPEED_MULTIPLIER;

            GameObject projectileObj = Potop.Client.Core.Pooling.PoolManager.Instance.Spawn(projectilePrefab, firePoint.position, firePoint.rotation);

            if (!projectileObj.GetComponent<PierceModifier>())
            {
                var pierce = projectileObj.AddComponent<PierceModifier>();
                pierce.enabled = true;
            }

            if (projectileObj.TryGetComponent<Projectile>(out var projectile))
            {
                projectile.Initialize(speed, Mathf.RoundToInt(damage), 0f, INFINITE_PIERCE, 0f);
            }
        }
    }
}
