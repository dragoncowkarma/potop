using UnityEngine;
using System.Collections;

namespace Potop.Client.Gameplay.Combat {
    public class OrbitalStrikeSkill : TacticalSkillBase {
        public override void Execute() {
            StartCoroutine(StrikeRoutine());
        }

        private IEnumerator StrikeRoutine() {
            int strikes = _skillData != null ? _skillData.ExecuteCount : 10;
            float radius = _skillData != null ? _skillData.Radius : 2f;
            int damage = _skillData != null ? _skillData.Damage : 50;

            Camera mainCam = Camera.main;
            if (mainCam == null) yield break;

            Collider[] hits = new Collider[20];
            int layerMask = LayerMask.GetMask("Enemy");

            for (int i = 0; i < strikes; i++) {
                Vector3 randomViewport = new Vector3(Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f), 10f);
                Ray ray = mainCam.ViewportPointToRay(randomViewport);
                Vector3 hitPoint = Vector3.zero;

                // Assuming the ground is around z=0 or y=0, we just trace the ray
                if (Physics.Raycast(ray, out RaycastHit hit, 100f)) {
                    hitPoint = hit.point;
                } else {
                    hitPoint = mainCam.transform.position + ray.direction * 10f;
                }

                int count = Physics.OverlapSphereNonAlloc(hitPoint, radius, hits, layerMask);
                for (int j = 0; j < count; j++) {
                    if (hits[j].TryGetComponent<IDamageable>(out var dmg)) {
                        dmg.TakeDamage(new DamageInfo { Amount = damage, HitPoint = hitPoint });
                    }
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
