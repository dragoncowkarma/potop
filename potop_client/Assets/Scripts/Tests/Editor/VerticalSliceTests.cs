using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Reflection;
using Potop.Client.Core;
using Potop.Client.Core.Events;
using Potop.Client.Gameplay.Flow;
using Potop.Client.Gameplay.Combat;
using Potop.Client.Gameplay;
using Potop.Client.Gameplay.Meta;

namespace Potop.Client.Tests.Integration {
    public class VerticalSliceTests {
        private GameObject _harnessGo;
        private GameFlowController _flow;
        private GameManager _gm;

        [SetUp]
        public void Setup() {
            EventBroker.ClearAllSubscriptions();
            _harnessGo = new GameObject("VerticalSliceHarness");
            _gm = _harnessGo.AddComponent<GameManager>();
            _flow = _harnessGo.AddComponent<GameFlowController>();
            
            typeof(GameManager).GetMethod("Awake", BindingFlags.NonPublic | BindingFlags.Instance)?.Invoke(_gm, null);
            typeof(GameFlowController).GetMethod("Awake", BindingFlags.NonPublic | BindingFlags.Instance)?.Invoke(_flow, null);

            _flow.UIInstantiator = (path) => new GameObject("MockUI").AddComponent<Potop.Client.UI.ResultUIController>().gameObject;
            _flow.UIDestroyer = (obj) => Object.DestroyImmediate(obj);
        }

        [TearDown]
        public void Teardown() {
            Object.DestroyImmediate(_harnessGo);
            EventBroker.ClearAllSubscriptions();
        }

        [UnityTest]
        public IEnumerator VerticalSlice_FullAutomationLoop_Verification() {
            Debug.Log("[7.6] Starting Vertical Slice Verification...");

            Assert.AreEqual(GameFlowState.Lobby, _flow.CurrentState);
            _flow.TransitionTo(GameFlowState.InGame);

            var bossGo = new GameObject("TitanCore_Test");
            var boss = bossGo.AddComponent<TitanCoreAI>();
            // Use GetComponent because RequireComponent already added it
            var bossHealth = bossGo.GetComponent<Health>();
            bossHealth.InitializeHealth(1000);
            
            typeof(TitanCoreAI).GetMethod("Awake", BindingFlags.NonPublic | BindingFlags.Instance)?.Invoke(boss, null);
            typeof(TitanCoreAI).GetMethod("OnEnable", BindingFlags.NonPublic | BindingFlags.Instance)?.Invoke(boss, null);

            // Position boss and damage dealer to ensure non-frontal attack
            bossGo.transform.position = Vector3.zero;
            bossGo.transform.forward = Vector3.forward;
            var damageDealer = new GameObject("DamageDealer");
            damageDealer.transform.position = new Vector3(0, 0, -10);

            Debug.Log($"[7.6] Boss Spawned. HP: {bossHealth.CurrentHealth}/{bossHealth.MaxHealth}, Phase: {boss.CurrentPhase}");
            Assert.AreEqual(1, boss.CurrentPhase);

            // Phase 1 -> 2
            boss.TakeDamage(new DamageInfo { Amount = 401, Instigator = damageDealer });
            yield return null;
            Debug.Log($"[7.6] After 401 damage: HP={bossHealth.CurrentHealth}, Phase={boss.CurrentPhase}");
            Assert.AreEqual(2, boss.CurrentPhase, "Boss should transition to Phase 2 at 60% HP");

            // Phase 2 -> 3
            boss.TakeDamage(new DamageInfo { Amount = 301, Instigator = damageDealer });
            yield return null;
            Debug.Log($"[7.6] After 301 more damage: HP={bossHealth.CurrentHealth}, Phase={boss.CurrentPhase}");
            Assert.AreEqual(3, boss.CurrentPhase, "Boss should transition to Phase 3 at 30% HP");

            // Defeat
            boss.TakeDamage(new DamageInfo { Amount = 300 });
            yield return null;
            Assert.AreEqual(GameFlowState.Overclock, _flow.CurrentState);

            _gm.TriggerGameOver();
            yield return null;
            Assert.AreEqual(GameFlowState.Result, _flow.CurrentState);
            
            Debug.Log("[7.6] Vertical Slice Verification Completed Successfully.");
            Object.DestroyImmediate(bossGo);
            Object.DestroyImmediate(damageDealer);
        }
    }
}
