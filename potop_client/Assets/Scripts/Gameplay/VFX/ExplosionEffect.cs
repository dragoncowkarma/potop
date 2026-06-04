using UnityEngine;
using UnityEngine.Rendering.Universal;
using Potop.Client.Core.Pooling;

namespace Potop.Client.Gameplay.VFX {
    /// <summary>
    /// 개별 VFX 프리팹에 부착되어 품질 수준 조절, 파티클 예산 제한, 및 풀 반환 시의 완전한 리셋을 담당하는 컴포넌트입니다.
    /// </summary>
    public class ExplosionEffect : MonoBehaviour {
        private struct CachedParticleSettings {
            public ParticleSystem ParticleSystem;
            public int OriginalMaxParticles;
            public float OriginalRateOverTimeMultiplier;
        }

        private CachedParticleSettings[] _cachedParticles;
        private TrailRenderer[] _trailRenderers;
        private Light[] _lights;
        private DecalProjector[] _decalProjectors;
        private Renderer[] _renderers;
        private bool _isInitialized = false;

        private void Awake() {
            InitializeCache();
        }

        private void OnEnable() {
            if (!_isInitialized) {
                InitializeCache();
            }
            ApplyQualityAndBudgetSettings();
            if (VFXBudgetManager.Instance != null) {
                VFXBudgetManager.Instance.Register(this);
            }
        }

        private void OnDisable() {
            if (VFXBudgetManager.Instance != null) {
                VFXBudgetManager.Instance.Unregister(this);
            }
            ResetEffect();
        }

        /// <summary>
        /// 하위 컴포넌트들을 탐색하고 초기 설정을 캐싱합니다.
        /// </summary>
        private void InitializeCache() {
            if (_isInitialized) return;

            // 파티클 시스템 캐싱
            ParticleSystem[] particleSystems = GetComponentsInChildren<ParticleSystem>(true);
            _cachedParticles = new CachedParticleSettings[particleSystems.Length];
            for (int i = 0; i < particleSystems.Length; i++) {
                var ps = particleSystems[i];
                _cachedParticles[i] = new CachedParticleSettings {
                    ParticleSystem = ps,
                    OriginalMaxParticles = ps.main.maxParticles,
                    OriginalRateOverTimeMultiplier = ps.emission.rateOverTimeMultiplier
                };
            }

            // 트레일, 라이트, 데칼, 렌더러 캐싱
            _trailRenderers = GetComponentsInChildren<TrailRenderer>(true);
            _lights = GetComponentsInChildren<Light>(true);
            _decalProjectors = GetComponentsInChildren<DecalProjector>(true);
            _renderers = GetComponentsInChildren<Renderer>(true);

            _isInitialized = true;
        }

        /// <summary>
        /// 현재 품질 및 파티클 예산 상황에 따라 VFX 설정을 동적으로 조정합니다.
        /// </summary>
        public void ApplyQualityAndBudgetSettings() {
            if (!_isInitialized) InitializeCache();

            VFXQuality quality = VFXQualitySettings.CurrentQuality;
            float qualityScale = 1.0f;
            bool enableTrails = true;
            bool enableLights = true;
            bool enableDecals = true;

            switch (quality) {
                case VFXQuality.Low:
                    qualityScale = 0.3f;
                    enableTrails = false;
                    enableLights = false;
                    enableDecals = false;
                    break;
                case VFXQuality.Medium:
                    qualityScale = 0.6f;
                    enableTrails = true;
                    enableLights = false;
                    enableDecals = false;
                    break;
                case VFXQuality.High:
                    qualityScale = 1.0f;
                    enableTrails = true;
                    enableLights = true;
                    enableDecals = true;
                    break;
            }

            // 예산 매니저의 밀도 멀티플라이어 획득
            float budgetScale = VFXBudgetManager.Instance != null
                ? VFXBudgetManager.Instance.GetBudgetDensityMultiplier()
                : 1.0f;

            float totalScale = qualityScale * budgetScale;

            // 파티클 시스템 조정
            foreach (var cache in _cachedParticles) {
                if (cache.ParticleSystem != null) {
                    var main = cache.ParticleSystem.main;
                    main.maxParticles = Mathf.Max(1, (int)(cache.OriginalMaxParticles * totalScale));

                    var emission = cache.ParticleSystem.emission;
                    emission.rateOverTimeMultiplier = cache.OriginalRateOverTimeMultiplier * totalScale;
                }
            }

            // 트레일 렌더러 제어
            foreach (var trail in _trailRenderers) {
                if (trail != null) {
                    trail.enabled = enableTrails;
                }
            }

            // 라이트 제어
            foreach (var light in _lights) {
                if (light != null) {
                    light.enabled = enableLights;
                }
            }

            // 데칼 프로젝터 제어
            foreach (var decal in _decalProjectors) {
                if (decal != null) {
                    decal.enabled = enableDecals;
                }
            }
        }

        /// <summary>
        /// 이 효과에서 방출된 활성 파티클 수를 반환합니다.
        /// </summary>
        public int GetActiveParticleCount() {
            if (!_isInitialized) return 0;

            int count = 0;
            foreach (var cache in _cachedParticles) {
                if (cache.ParticleSystem != null) {
                    count += cache.ParticleSystem.particleCount;
                }
            }
            return count;
        }

        /// <summary>
        /// 예산 초과 시 호출되어 효과를 안전하게 정지하고 해제(풀 반환)합니다.
        /// </summary>
        public void Prune() {
            ResetEffect();
            if (PoolManager.Instance != null) {
                PoolManager.Instance.Despawn(gameObject);
            } else {
                gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// VFX의 모든 시각 상태(파티클, 트레일, 렌더러 등)를 초기화하여 잔상을 방지합니다.
        /// </summary>
        public void ResetEffect() {
            if (!_isInitialized) return;

            // 파티클 정지 및 즉시 클리어
            foreach (var cache in _cachedParticles) {
                if (cache.ParticleSystem != null) {
                    cache.ParticleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                    cache.ParticleSystem.Clear(true);
                }
            }

            // 트레일 초기화
            foreach (var trail in _trailRenderers) {
                if (trail != null) {
                    trail.Clear();
                }
            }

            // 렌더러 머티리얼 프로퍼티 블록 초기화
            foreach (var renderer in _renderers) {
                if (renderer != null) {
                    renderer.SetPropertyBlock(null);
                }
            }
        }
    }
}
