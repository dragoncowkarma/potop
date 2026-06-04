using UnityEngine;
using Potop.Client.Core.Pooling;
using Potop.Client.Gameplay.Combat;
using Potop.Client.Core.Events;
using Potop.Client.Gameplay.Flow;

namespace Potop.Client.Gameplay.VFX {
    /// <summary>
    /// 피격/사망 및 보스 페이즈/UI 전환 이벤트에 반응하여 파티클 VFX를 스폰하는 컴포넌트입니다.
    /// Health 컴포넌트와 연동하거나 전역 이벤트를 수신하여 동작할 수 있습니다.
    /// </summary>
    public class VFXTrigger : MonoBehaviour {
        [Header("VFX Prefabs")]
        [SerializeField] private GameObject _hitVFXPrefab;
        [SerializeField] private GameObject _deathVFXPrefab;

        [Header("Global Settings")]
        [SerializeField] private bool _listenToGlobalEvents = false;
        [SerializeField] private GameObject[] _bossPhaseVFXPrefabs;
        [SerializeField] private GameObject _uiTransitionVFXPrefab;

        [Header("Settings")]
        [SerializeField] private float _vfxLifeTime = 2f;

        private Health _health;

        private void Awake() {
            _health = GetComponent<Health>();
        }

        private void OnEnable() {
            if (_health != null) {
                _health.OnDamaged += HandleDamaged;
                _health.OnDeath += HandleDeath;
            }

            if (_listenToGlobalEvents) {
                EventBroker.Subscribe<BossPhaseChangedEvent>(HandleBossPhaseChanged);
                EventBroker.Subscribe<GameFlowStateChangedEvent>(HandleGameFlowStateChanged);
            }
        }

        private void OnDisable() {
            if (_health != null) {
                _health.OnDamaged -= HandleDamaged;
                _health.OnDeath -= HandleDeath;
            }

            if (_listenToGlobalEvents) {
                EventBroker.Unsubscribe<BossPhaseChangedEvent>(HandleBossPhaseChanged);
                EventBroker.Unsubscribe<GameFlowStateChangedEvent>(HandleGameFlowStateChanged);
            }
        }

        private void HandleDamaged(DamageInfo info) {
            SpawnVFX(_hitVFXPrefab, info.HitPoint, info.HitNormal);
        }

        private void HandleDeath() {
            SpawnVFX(_deathVFXPrefab, transform.position, Vector3.up);
        }

        private void HandleBossPhaseChanged(BossPhaseChangedEvent e) {
            int index = e.Phase - 1;
            if (_bossPhaseVFXPrefabs != null && index >= 0 && index < _bossPhaseVFXPrefabs.Length) {
                GameObject prefab = _bossPhaseVFXPrefabs[index];
                if (prefab != null) {
                    SpawnVFX(prefab, transform.position, Vector3.up);
                }
            }
        }

        private void HandleGameFlowStateChanged(GameFlowStateChangedEvent e) {
            if (_uiTransitionVFXPrefab != null) {
                SpawnVFX(_uiTransitionVFXPrefab, Vector3.zero, Vector3.up);
            }
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

            if (instance != null) {
                // 스폰된 VFX가 예산/품질 관리를 받도록 ExplosionEffect 컴포넌트 강제 추가
                var effect = instance.GetComponent<ExplosionEffect>();
                if (effect == null) {
                    effect = instance.AddComponent<ExplosionEffect>();
                }

                if (PoolManager.Instance != null) {
                    await DespawnAfterDelay(instance, _vfxLifeTime);
                }
            }
        }

        /// <summary>
        /// 지정된 지연 시간 후에 VFX 인스턴스를 풀로 반환합니다.
        /// </summary>
        /// <param name="instance">반환할 인스턴스</param>
        /// <param name="delay">지연 시간(초)</param>
        private async Awaitable DespawnAfterDelay(GameObject instance, float delay) {
            await Awaitable.WaitForSecondsAsync(delay);

            if (instance != null && instance.activeInHierarchy) {
                ResetVFX(instance);
                PoolManager.Instance?.Despawn(instance);
            }
        }

        /// <summary>
        /// VFX 인스턴스의 파티클 시스템 및 시각 상태를 초기화합니다.
        /// </summary>
        /// <param name="instance">초기화할 VFX 오브젝트</param>
        private void ResetVFX(GameObject instance) {
            var effect = instance.GetComponent<ExplosionEffect>();
            if (effect != null) {
                effect.ResetEffect();
            } else {
                ParticleSystem[] particleSystems = instance.GetComponentsInChildren<ParticleSystem>();
                foreach (var ps in particleSystems) {
                    ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                }
            }
        }
    }
}
