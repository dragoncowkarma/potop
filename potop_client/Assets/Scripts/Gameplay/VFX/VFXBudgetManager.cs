using System.Collections.Generic;
using UnityEngine;

namespace Potop.Client.Gameplay.VFX {
    /// <summary>
    /// 동시 활성화된 VFX 파티클 수를 모니터링하고 10,000개 제한을 강제하는 매니저입니다.
    /// </summary>
    public class VFXBudgetManager : MonoBehaviour {
        public static VFXBudgetManager Instance { get; private set; }

        [SerializeField] private int _maxParticlesBudget = 10000;

        private readonly List<ExplosionEffect> _activeEffects = new List<ExplosionEffect>();

        public int MaxParticlesBudget => _maxParticlesBudget;

        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void OnDestroy() {
            if (Instance == this) {
                Instance = null;
            }
        }

        /// <summary>
        /// 새로운 VFX 효과를 매니저에 등록합니다.
        /// </summary>
        public void Register(ExplosionEffect effect) {
            if (effect == null) return;
            if (!_activeEffects.Contains(effect)) {
                _activeEffects.Add(effect);
            }
            CheckAndEnforceBudget();
        }

        /// <summary>
        /// VFX 효과의 등록을 해제합니다.
        /// </summary>
        public void Unregister(ExplosionEffect effect) {
            if (effect == null) return;
            _activeEffects.Remove(effect);
        }

        /// <summary>
        /// 현재 활성화된 모든 VFX 파티클 수의 총합을 계산합니다.
        /// </summary>
        public int GetTotalActiveParticles() {
            int total = 0;
            for (int i = _activeEffects.Count - 1; i >= 0; i--) {
                if (_activeEffects[i] != null) {
                    total += _activeEffects[i].GetActiveParticleCount();
                }
            }
            return total;
        }

        /// <summary>
        /// 파티클 예산을 강제 집행합니다. 예산이 초과될 경우 가장 오래된 VFX부터 순차적으로 제거합니다.
        /// </summary>
        public void CheckAndEnforceBudget() {
            int currentParticles = GetTotalActiveParticles();
            if (currentParticles <= _maxParticlesBudget) return;

            // 예산이 초과된 경우, 가장 오래된 효과부터 비활성화/풀 반환하여 예산을 맞춤
            for (int i = 0; i < _activeEffects.Count; i++) {
                if (currentParticles <= _maxParticlesBudget) break;

                var effect = _activeEffects[i];
                if (effect != null && effect.gameObject.activeInHierarchy) {
                    int particlesBefore = effect.GetActiveParticleCount();
                    effect.Prune();
                    currentParticles -= particlesBefore;
                }
            }
        }

        /// <summary>
        /// 신규 스폰 시 파티클 밀도를 조절하기 위한 밀도 멀티플라이어를 반환합니다.
        /// 예산의 80%에 도달하면 신규 스폰 파티클 수가 점차 감소하도록 유도합니다.
        /// </summary>
        public float GetBudgetDensityMultiplier() {
            int current = GetTotalActiveParticles();
            float threshold = _maxParticlesBudget * 0.8f;
            if (current <= threshold) return 1f;

            float excess = current - threshold;
            float range = _maxParticlesBudget - threshold;
            return Mathf.Max(0.1f, 1f - (excess / range));
        }
    }
}
