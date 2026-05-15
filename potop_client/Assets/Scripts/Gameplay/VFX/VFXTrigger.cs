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

        /// <summary>
        /// VFX를 스폰하고 지정된 시간 후에 풀로 반환합니다.
        /// </summary>
        /// <param name="prefab">스폰할 프리팹</param>
        /// <param name="position">스폰 위치</param>
        /// <param name="normal">표면 법선</param>
        private async void SpawnVFX(GameObject prefab, Vector3 position, Vector3 normal) {
            if (prefab == null) return;

            Quaternion rotation = Quaternion.LookRotation(normal);
            GameObject instance = PoolManager.Instance != null
                ? PoolManager.Instance.Spawn(prefab, position, rotation)
                : Instantiate(prefab, position, rotation);

            if (instance != null && PoolManager.Instance != null) {
                await DespawnAfterDelay(instance, _vfxLifeTime);
            }
        }

        /// <summary>
        /// 지정된 지연 시간 후에 VFX 인스턴스를 풀로 반환합니다.
        /// </summary>
        /// <param name="instance">반환할 인스턴스</param>
        /// <param name="delay">지연 시간(초)</param>
        private async Awaitable DespawnAfterDelay(GameObject instance, float delay) {
            // Unity 6 Awaitable을 사용하여 코루틴 없이 비동기 대기
            await Awaitable.WaitForSecondsAsync(delay);

            if (instance != null && instance.activeInHierarchy) {
                // 풀에 반환하기 전에 파티클 상태를 초기화하여 잔상 문제를 방지
                ResetVFX(instance);
                PoolManager.Instance?.Despawn(instance);
            }
        }

        /// <summary>
        /// VFX 인스턴스의 파티클 시스템을 정지하고 초기화합니다.
        /// </summary>
        /// <param name="instance">초기화할 VFX 오브젝트</param>
        private void ResetVFX(GameObject instance) {
            ParticleSystem[] particleSystems = instance.GetComponentsInChildren<ParticleSystem>();
            foreach (var ps in particleSystems) {
                ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            }
        }
    }
}

