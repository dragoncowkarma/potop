using UnityEngine;
using Potop.Client.Gameplay.Weapons.Strategies;

namespace Potop.Client.Gameplay.Weapons.Overdrive
{
    /// <summary>
    /// 오비탈 스트라이크 발사 전략입니다.
    /// 거대한 폭발을 동반하는 궤도 폭격을 수행합니다.
    /// </summary>
    public class OrbitalStrikeFireStrategy : IFireStrategy
    {
        private const float EXPLOSION_RADIUS = 10f;
        private const float SCALE_MULTIPLIER = 3f;

        public void ExecuteFire(WeaponBase weapon)
        {
            GameObject projectilePrefab = weapon.ProjectilePrefab;
            Transform firePoint = weapon.FirePoint;

            if (projectilePrefab == null || firePoint == null) return;

            float damage = weapon.GetCalculatedDamage();
            float speed = weapon.GetCalculatedProjectileSpeed();

            // 목표 지점은 현재는 전방으로 설정
            Vector3 targetPosition = firePoint.position + firePoint.forward * 20f;

            // 위에서 아래로 떨어지도록 위치 조정 (궤도 폭격 느낌)
            Vector3 spawnPosition = targetPosition + Vector3.up * 30f;
            Quaternion spawnRotation = Quaternion.LookRotation(Vector3.down);

            GameObject projectileObj = Potop.Client.Core.Pooling.PoolManager.Instance.Spawn(projectilePrefab, spawnPosition, spawnRotation);

            projectileObj.transform.localScale = Vector3.one * SCALE_MULTIPLIER;

            if (projectileObj.TryGetComponent<Projectile>(out var projectile))
            {
                // 매우 높은 피해량, 넓은 범위, 넉백 추가
                projectile.Initialize(speed * 2f, Mathf.RoundToInt(damage * 5f), EXPLOSION_RADIUS, 0, 10f);
            }
        }
    }
}
