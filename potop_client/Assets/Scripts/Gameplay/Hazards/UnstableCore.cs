using System.Collections;
using UnityEngine;
using Potop.Client.Gameplay.Combat;
using Potop.Client.Core.Pooling;

namespace Potop.Client.Gameplay.Hazards {
    /// <summary>
    /// 공격받으면 폭발하여 주변 적에게 광역 피해를 입히고 슬로우 효과를 부여하는 불안정한 코어 환경 요소입니다.
    /// </summary>
    public class UnstableCore : EnvironmentalHazard {
        [Header("Explosion Settings")]
        [SerializeField] private float _explosionRadius = 5f;
        [SerializeField] private int _explosionDamage = 50;
        [SerializeField] private LayerMask _enemyLayer;

        [Header("Slow Effect Settings")]
        [SerializeField] private float _slowDuration = 3f;
        [SerializeField] private float _slowFactor = 0.5f;

        [Header("VFX")]
        [SerializeField] private GameObject _explosionVfxPrefab;

        // 성능 최적화를 위한 물리 쿼리 결과 캐싱 (최대 30개)
        private Collider[] _colliders = new Collider[30];

        /// <summary>
        /// 피해를 입었을 때 폭발 로직을 실행합니다.
        /// </summary>
        /// <param name="info">가해진 피해 정보</param>
        protected override void ActivateHazard(DamageInfo info) {
            Explode();
        }

        private void Explode() {
            // 시각 효과 생성
            if (_explosionVfxPrefab != null && PoolManager.Instance != null) {
                PoolManager.Instance.Spawn(_explosionVfxPrefab, transform.position, Quaternion.identity);
            }

            // 반경 내 적 탐색 및 피해, 디버프 적용 (가비지 할당 방지)
            int count = Physics.OverlapSphereNonAlloc(transform.position, _explosionRadius, _colliders, _enemyLayer);
            for (int i = 0; i < count; i++) {
                Collider col = _colliders[i];

                if (col.TryGetComponent<IDamageable>(out var damageable)) {
                    DamageInfo explosionInfo = new DamageInfo {
                        Amount = _explosionDamage,
                        HitPoint = col.ClosestPoint(transform.position),
                        HitNormal = (col.transform.position - transform.position).normalized,
                        Instigator = gameObject,
                        Type = DamageType.Explosive
                    };
                    damageable.TakeDamage(explosionInfo);
                }

                // 슬로우 디버프 적용 (성능을 위해 GetComponent 최적화)
                if (col.CompareTag("Enemy")) {
                    if (!col.TryGetComponent<SlowDebuff>(out var slowDebuff)) {
                        slowDebuff = col.gameObject.AddComponent<SlowDebuff>();
                    }
                    slowDebuff.ApplySlow(_slowDuration, _slowFactor);
                }
            }

            // 폭발 후 오브젝트 제거(풀 반환)
            if (PoolManager.Instance != null) {
                PoolManager.Instance.Despawn(gameObject);
            } else {
                Destroy(gameObject);
            }
        }
    }
}
