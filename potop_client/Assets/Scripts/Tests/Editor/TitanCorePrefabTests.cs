using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using Potop.Client.Gameplay;
using Potop.Client.Gameplay.VFX;

namespace Potop.Client.Tests.EditMode {
    public class TitanCorePrefabTests {
        private const string PREFAB_PATH = "Assets/Prefabs/Enemies/TitanCore.prefab";

        [Test]
        public void TitanCorePrefab_HierarchyAndComponents_AreValid() {
            // Load prefab
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(PREFAB_PATH);
            Assert.IsNotNull(prefab, $"Prefab not found at path: {PREFAB_PATH}");

            // Root components assertions
            EnemyBase enemyBase = prefab.GetComponent<EnemyBase>();
            Assert.IsNotNull(enemyBase, "Root must have EnemyBase component.");

            Animator animator = prefab.GetComponent<Animator>();
            Assert.IsNotNull(animator, "Root must have Animator component.");

            Collider rootCollider = prefab.GetComponent<Collider>();
            Assert.IsNotNull(rootCollider, "Root must have a Collider component.");
            Assert.IsTrue(rootCollider.enabled, "Root collider must be active.");

            // Child components assertions
            Transform body = prefab.transform.Find("Body");
            Assert.IsNotNull(body, "Child GameObject 'Body' must exist.");
            Assert.GreaterOrEqual(body.localScale.x, 5f, "Body scale must be 5x larger than normal enemies.");

            Transform shieldRing = prefab.transform.Find("ShieldRing");
            Assert.IsNotNull(shieldRing, "Child GameObject 'ShieldRing' must exist.");
            Assert.IsNotNull(shieldRing.GetComponent<ShieldRingRotator>(), "ShieldRing must have ShieldRingRotator component.");

            Transform laserEmitter = prefab.transform.Find("LaserEmitter");
            Assert.IsNotNull(laserEmitter, "Child GameObject 'LaserEmitter' must exist.");
        }
    }
}
