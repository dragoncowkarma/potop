using UnityEngine;
using Potop.Client.Gameplay.Weapons.Strategies;

namespace Potop.Client.Gameplay {
    /// <summary>
    /// 에너지 구체 포탑입니다.
    /// 적중 시 범위 폭발을 일으킵니다. 히트 수는 무제한입니다.
    /// </summary>
    public class NovaWeapon : Weapons.WeaponBase {
        // 노바 고유 스탯: 폭발 반경
        private const float EXPLOSION_RADIUS = 1.5f;

        protected override void Start() {
            base.Start();
            // 노바 전용 범위 공격 발사 전략 설정
            SetFireStrategy(new NovaFireStrategy(EXPLOSION_RADIUS));
        }

        /// <summary>
        /// 노바 전용 폭발 발사 전략입니다.
        /// </summary>
        private class NovaFireStrategy : IFireStrategy {
            private readonly float _explosionRadius;

            public NovaFireStrategy(float explosionRadius) {
                _explosionRadius = explosionRadius;
            }

            public void ExecuteFire(Weapons.WeaponBase weapon) {
                GameObject projectilePrefab = weapon.ProjectilePrefab;
                Transform firePoint = weapon.FirePoint;

                if (projectilePrefab == null || firePoint == null) return;

                float damage = weapon.GetCalculatedDamage();
                float speed = weapon.GetCalculatedProjectileSpeed();

                GameObject projectileObj = Core.Pooling.PoolManager.Instance.Spawn(projectilePrefab, firePoint.position, firePoint.rotation);

                if (projectileObj.TryGetComponent<Projectile>(out var projectile)) {
                    projectile.Initialize(speed, Mathf.RoundToInt(damage));

                    // TODO: Projectile 시스템 확장 시 폭발 반경 전달
                    // 현재 Projectile에는 폭발 범위(AoE) 설정이 없으므로, 추후 반영을 위해 구조만 둡니다.
                }
            }
        }
    }
}
