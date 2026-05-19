using UnityEngine;
using Potop.Client.Gameplay.Weapons.Strategies;

namespace Potop.Client.Gameplay.Weapons.Overdrive
{
    /// <summary>
    /// 프리즘 체인 발사 전략입니다.
    /// 다연발 투사체를 방사형으로 발사하며, 각 투사체는 높은 관통/도탄 횟수를 가집니다.
    /// (실제 도탄 로직은 BounceModifier 등 별도 모디파이어 컴포넌트에 의존하거나 투사체 내부에 존재함을 가정)
    /// </summary>
    public class PrismChainFireStrategy : IFireStrategy
    {
        private const int PROJECTILE_COUNT = 5;
        private const float SPREAD_ANGLE = 45f;
        private const int PIERCE_COUNT = 3;

        public void ExecuteFire(WeaponBase weapon)
        {
            GameObject projectilePrefab = weapon.ProjectilePrefab;
            Transform firePoint = weapon.FirePoint;

            if (projectilePrefab == null || firePoint == null) return;

            float damage = weapon.GetCalculatedDamage();
            float speed = weapon.GetCalculatedProjectileSpeed();

            float angleStep = SPREAD_ANGLE / (PROJECTILE_COUNT - 1);
            float startAngle = -SPREAD_ANGLE / 2f;

            for (int i = 0; i < PROJECTILE_COUNT; i++)
            {
                float currentAngle = startAngle + (angleStep * i);
                Quaternion rotation = firePoint.rotation * Quaternion.Euler(0, currentAngle, 0);

                GameObject projectileObj = Potop.Client.Core.Pooling.PoolManager.Instance.Spawn(projectilePrefab, firePoint.position, rotation);

                // 궁극 무기 전용 모디파이어 추가
                if (!projectileObj.GetComponent<BounceModifier>())
                {
                    var bounce = projectileObj.AddComponent<BounceModifier>();
                    bounce.enabled = true; // 풀링 호환성을 위해 OnEnable/OnDisable 사용 고려
                }

                if (!projectileObj.GetComponent<HomingModifier>())
                {
                    var homing = projectileObj.AddComponent<HomingModifier>();
                    homing.enabled = true;
                }

                if (projectileObj.TryGetComponent<Projectile>(out var projectile))
                {
                    projectile.Initialize(speed, Mathf.RoundToInt(damage), 0f, PIERCE_COUNT, 0f);
                }
            }
        }
    }
}
