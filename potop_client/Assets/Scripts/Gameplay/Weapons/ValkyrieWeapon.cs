using UnityEngine;
using Potop.Client.Gameplay.Weapons.Strategies;

namespace Potop.Client.Gameplay {
    /// <summary>
    /// 고속 기관총 포탑입니다.
    /// 매우 빠른 공속과 탄퍼짐(Spread)을 가집니다.
    /// </summary>
    public class ValkyrieWeapon : Weapons.WeaponBase {
        // 탄퍼짐 설정은 발키리 고유의 특징이므로 상수로 정의합니다.
        private const float SPREAD_ANGLE = 15f;

        protected override void Start() {
            base.Start();
            // 스프레드 발사 전략을 설정합니다.
            SetFireStrategy(new ValkyrieSpreadStrategy(SPREAD_ANGLE));
        }

        /// <summary>
        /// 발키리 전용 탄퍼짐 발사 전략입니다.
        /// </summary>
        private class ValkyrieSpreadStrategy : IFireStrategy {
            private readonly float _spreadAngle;

            public ValkyrieSpreadStrategy(float spreadAngle) {
                _spreadAngle = spreadAngle;
            }

            public void ExecuteFire(Weapons.WeaponBase weapon) {
                GameObject projectilePrefab = weapon.ProjectilePrefab;
                Transform firePoint = weapon.FirePoint;

                if (projectilePrefab == null || firePoint == null) return;

                float damage = weapon.GetCalculatedDamage();
                float speed = weapon.GetCalculatedProjectileSpeed();

                // 탄퍼짐 계산: y축(좌우)을 기준으로 랜덤 각도 적용
                float randomAngle = Random.Range(-_spreadAngle / 2f, _spreadAngle / 2f);
                Quaternion spreadRotation = firePoint.rotation * Quaternion.Euler(0, randomAngle, 0);

                GameObject projectileObj = Core.Pooling.PoolManager.Instance.Spawn(projectilePrefab, firePoint.position, spreadRotation);

                if (projectileObj.TryGetComponent<Projectile>(out var projectile)) {
                    projectile.Initialize(speed, Mathf.RoundToInt(damage));
                }
            }
        }
    }
}
