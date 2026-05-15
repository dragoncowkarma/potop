using UnityEngine;
using Potop.Client.Gameplay.Weapons.Strategies;

namespace Potop.Client.Gameplay {
    /// <summary>
    /// 철갑탄 포탑입니다.
    /// 강력한 피해, 관통, 그리고 넉백을 제공합니다.
    /// </summary>
    public class JuggernautWeapon : Weapons.WeaponBase {
        // 저거너트 고유 스탯: 관통 횟수와 넉백 수치
        private const int PIERCE_COUNT = 1;
        private const float KNOCKBACK_FORCE = 5.0f;

        protected override void Start() {
            base.Start();
            // 저거너트 전용 발사 전략 설정
            SetFireStrategy(new JuggernautFireStrategy(PIERCE_COUNT, KNOCKBACK_FORCE));
        }

        /// <summary>
        /// 저거너트 전용 관통 및 넉백 발사 전략입니다.
        /// </summary>
        private class JuggernautFireStrategy : IFireStrategy {
            private readonly int _pierceCount;
            private readonly float _knockbackForce;

            public JuggernautFireStrategy(int pierceCount, float knockbackForce) {
                _pierceCount = pierceCount;
                _knockbackForce = knockbackForce;
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

                    // TODO: Projectile 시스템 확장 시 관통력 및 넉백 전달
                    // 현재 Projectile에는 Pierce/Knockback 설정이 없으므로, 추후 반영을 위해 구조만 둡니다.
                }
            }
        }
    }
}
