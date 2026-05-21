using UnityEngine;
using System.Linq;
using Potop.Client.Core.Pooling;

namespace Potop.Client.Gameplay.Combat {
    public class EMPSkill : TacticalSkillBase {
        public override void Execute() {
            float duration = _skillData != null ? _skillData.Duration : 5f;

            Camera mainCam = Camera.main;
            if (mainCam != null) {
                foreach (var enemy in FindObjectsByType<EnemyBase>(FindObjectsInactive.Exclude, FindObjectsSortMode.None)) {
                    if (enemy == null) continue;

                    Vector3 vp = mainCam.WorldToViewportPoint(enemy.transform.position);
                    if (vp.x >= 0 && vp.x <= 1 && vp.y >= 0 && vp.y <= 1 && vp.z > 0) {
                        enemy.Stun(duration);
                    }
                }
            }

            int enemyLayer = LayerMask.NameToLayer("Enemy");
            foreach (var proj in Projectile.ActiveProjectiles.ToList()) {
                if (proj == null) continue;

                if (proj.gameObject.layer == enemyLayer || proj.CompareTag("Enemy")) {
                    if (PoolManager.Instance != null) {
                        PoolManager.Instance.Despawn(proj.gameObject);
                    } else {
                        proj.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
