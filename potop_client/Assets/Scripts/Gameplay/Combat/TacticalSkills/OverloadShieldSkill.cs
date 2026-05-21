using UnityEngine;
using System.Collections;
using Potop.Client.Core;

namespace Potop.Client.Gameplay.Combat {
    public class OverloadShieldSkill : TacticalSkillBase {
        private const int INSTANT_KILL_DAMAGE = 999999;

        public override void Execute() {
            StartCoroutine(ShieldRoutine());
        }

        private IEnumerator ShieldRoutine() {
            float duration = _skillData != null ? _skillData.Duration : 10f;
            float radius = _skillData != null ? _skillData.Radius : 3f;
            float endTime = Time.time + duration;

            Collider[] hits = new Collider[64];
            int layerMask = LayerMask.GetMask("Enemy");

            while (Time.time < endTime) {
                if (GameManager.Instance != null && GameManager.Instance.PlayerTransform != null) {
                    Vector3 center = GameManager.Instance.PlayerTransform.position;
                    int count = Physics.OverlapSphereNonAlloc(center, radius, hits, layerMask);
                    for (int i = 0; i < count; i++) {
                        if (hits[i] != null && hits[i].TryGetComponent<IDamageable>(out var dmg)) {
                            // Instant kill contact
                            dmg.TakeDamage(new DamageInfo { Amount = INSTANT_KILL_DAMAGE, HitPoint = center });
                        }
                    }
                }
                yield return null;
            }
        }
    }
}
