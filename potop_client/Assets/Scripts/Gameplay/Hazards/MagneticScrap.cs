using System.Collections;
using UnityEngine;
using Potop.Client.Gameplay.Combat;
using Potop.Client.Gameplay.Items;

namespace Potop.Client.Gameplay.Hazards {
    /// <summary>
    /// 플레이어의 공격으로 활성화되면 일정 범위 내의 Gem(재화)을 끌어당기는 자기장 환경 요소입니다.
    /// </summary>
    public class MagneticScrap : EnvironmentalHazard {
        [Header("Magnet Settings")]
        [SerializeField] private float _pullRadius = 15f;
        [SerializeField] private float _pullForce = 10f;
        [SerializeField] private float _activeDuration = 5f;
        [SerializeField] private LayerMask _itemLayer;

        [Header("VFX")]
        [SerializeField] private GameObject _magnetVfxPrefab;
        private GameObject _activeVfxInstance;

        private Coroutine _pullRoutine;

        // 성능 최적화를 위한 물리 쿼리 결과 캐싱 (최대 30개)
        private Collider[] _colliders = new Collider[30];

        /// <summary>
        /// 피해를 입었을 때 자기장 활성화 로직을 실행합니다.
        /// </summary>
        /// <param name="info">가해진 피해 정보</param>
        protected override void ActivateHazard(DamageInfo info) {
            if (_pullRoutine != null) {
                StopCoroutine(_pullRoutine);
            }
            _pullRoutine = StartCoroutine(MagnetRoutine());
        }

        private void FixedUpdate() {
            if (_pullRoutine != null) {
                PullGems();
            }
        }

        private IEnumerator MagnetRoutine() {
            // VFX 생성 및 활성화
            if (_magnetVfxPrefab != null && Potop.Client.Core.Pooling.PoolManager.Instance != null) {
                _activeVfxInstance = Potop.Client.Core.Pooling.PoolManager.Instance.Spawn(_magnetVfxPrefab, transform.position, Quaternion.identity);
                if (_activeVfxInstance != null) {
                    _activeVfxInstance.transform.SetParent(transform);
                }
            }

            yield return new WaitForSeconds(_activeDuration);

            _pullRoutine = null;

            // 지속 시간 종료 후 정리 및 풀 반환
            if (_activeVfxInstance != null && Potop.Client.Core.Pooling.PoolManager.Instance != null) {
                Potop.Client.Core.Pooling.PoolManager.Instance.Despawn(_activeVfxInstance);
                _activeVfxInstance = null;
            }

            if (Potop.Client.Core.Pooling.PoolManager.Instance != null) {
                Potop.Client.Core.Pooling.PoolManager.Instance.Despawn(gameObject);
            } else {
                Destroy(gameObject);
            }
        }

        private void PullGems() {
            int count = Physics.OverlapSphereNonAlloc(transform.position, _pullRadius, _colliders, _itemLayer);
            for (int i = 0; i < count; i++) {
                Collider col = _colliders[i];
                Gem gem = col.GetComponent<Gem>();
                if (gem != null) {
                    Rigidbody rb = col.attachedRigidbody;
                    if (rb != null) {
                        Vector3 pullDirection = (transform.position - gem.transform.position).normalized;
                        // 거리에 반비례하는 힘을 가해 가까울수록 더 강하게 당깁니다. (0으로 나누는 것 방지)
                        float distance = Mathf.Max(1f, Vector3.Distance(transform.position, gem.transform.position));
                        float forceMagnitude = _pullForce / distance;

                        // ForceMode.Force를 사용하여 일관된 물리적 당김 효과를 제공합니다.
                        rb.AddForce(pullDirection * forceMagnitude, ForceMode.Force);
                    } else {
                        // Rigidbody가 없는 경우 Transform 기반으로 끌어당김
                        gem.transform.position = Vector3.MoveTowards(gem.transform.position, transform.position, _pullForce * Time.deltaTime);
                    }
                }
            }
        }

        protected override void OnEnable() {
            base.OnEnable();
            if (_pullRoutine != null) {
                StopCoroutine(_pullRoutine);
                _pullRoutine = null;
            }
            if (_activeVfxInstance != null && Potop.Client.Core.Pooling.PoolManager.Instance != null) {
                Potop.Client.Core.Pooling.PoolManager.Instance.Despawn(_activeVfxInstance);
                _activeVfxInstance = null;
            }
        }
    }
}
