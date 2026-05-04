using UnityEngine;
using Potop.Client.Core.Pooling;
using Potop.Client.Gameplay.Combat;

namespace Potop.Client.Gameplay.VFX {
    /// <summary>
    /// 피격/사망 이벤트에 반응하여 파티클 VFX를 피격 위치에 스폰하는 컴포넌트입니다.
    /// Health 컴포넌트와 동일한 GameObject에 부착하여 사용합니다.
    /// </summary>
    [RequireComponent(typeof(Health))]
    public class VFXTrigger : MonoBehaviour {
        [Header("VFX Prefabs")]
        [SerializeField] private GameObject _hitVFXPrefab;
        [SerializeField] private GameObject _deathVFXPrefab;

        [Header("Settings")]
        [SerializeField] private float _vfxLifeTime = 2f;

        private Health _health;

        private void Awake() {
            _health = GetComponent<Health>();
        }

        private void OnEnable() {
            _health.OnDamaged += HandleDamaged;
            _health.OnDeath += HandleDeath;
        }

        private void OnDisable() {
            _health.OnDamaged -= HandleDamaged;
            _health.OnDeath -= HandleDeath;
        }

        private void HandleDamaged(DamageInfo info) {
            SpawnVFX(_hitVFXPrefab, info.HitPoint, info.HitNormal);
        }

        private void HandleDeath() {
            SpawnVFX(_deathVFXPrefab, transform.position, Vector3.up);
        }

        private void SpawnVFX(GameObject prefab, Vector3 position, Vector3 normal) {
            if (prefab == null) return;

            Quaternion rotation = Quaternion.LookRotation(normal);
            GameObject instance = PoolManager.Instance != null
                ? PoolManager.Instance.Spawn(prefab, position, rotation)
                : Instantiate(prefab, position, rotation);

            // Pool will handle despawn via prefab's own auto-return, or schedule manual despawn
            if (instance != null && PoolManager.Instance != null) {
                StartCoroutine(DespawnAfterDelay(instance, _vfxLifeTime));
            }
        }

        private System.Collections.IEnumerator DespawnAfterDelay(GameObject instance, float delay) {
            yield return new WaitForSeconds(delay);
            if (instance != null && instance.activeInHierarchy) {
                PoolManager.Instance?.Despawn(instance);
            }
        }
    }
}
