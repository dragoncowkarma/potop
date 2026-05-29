using NUnit.Framework;
using UnityEngine;
using System.Reflection;
using Potop.Client.Gameplay;
using Potop.Client.Gameplay.Combat;
using Potop.Client.Data;
using Potop.Client.Core.Events;

namespace Potop.Client.Tests.EditMode {
    /// <summary>
    /// TitanCoreAI의 체력 임계값 기반 페이즈 전환 및 정면 실드 반사 로직을 검증하는 EditMode 단위 테스트 클래스입니다.
    /// </summary>
    public class TitanCoreAITests {
        private GameObject _bossGo;
        private GameObject _playerGo;
        private Health _health;
        private TitanCoreAI _bossAI;
        private TitanCoreData _data;

        [SetUp]
        public void Setup() {
            _bossGo = new GameObject("Boss");
            _health = _bossGo.AddComponent<Health>();
            _bossAI = _bossGo.AddComponent<TitanCoreAI>();

            _playerGo = new GameObject("Player");
            _playerGo.tag = "Player";

            _data = ScriptableObject.CreateInstance<TitanCoreData>();

            // Reflection to set MaxHealth on ScriptableObject
            typeof(EnemyData).GetField("_maxHealth", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(_data, 100);
            typeof(EnemyData).GetField("_moveSpeed", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(_data, 5f);

            // Reflection to assign data references
            typeof(EnemyBase).GetField("_enemyData", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(_bossAI, _data);
            typeof(TitanCoreAI).GetField("_titanCoreData", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(_bossAI, _data);

            _bossGo.SetActive(true);

            EventBroker.ClearAllSubscriptions();
        }

        [TearDown]
        public void Teardown() {
            Object.DestroyImmediate(_bossGo);
            Object.DestroyImmediate(_playerGo);
            Object.DestroyImmediate(_data);
            EventBroker.ClearAllSubscriptions();
        }

        [Test]
        public void HPThresholds_TransitionsExactlyAt60And30Percent() {
            Assert.AreEqual(1, _bossAI.CurrentPhase);

            int phaseReceived = 0;
            EventBroker.Subscribe<BossPhaseChangedEvent>(e => {
                phaseReceived = e.Phase;
            });

            // Position player to the side to bypass shield
            _playerGo.transform.position = new Vector3(5f, 0f, 0f);
            _bossGo.transform.position = new Vector3(0f, 0f, 0f);
            _bossGo.transform.rotation = Quaternion.identity;

            DamageInfo damageToSide = new DamageInfo {
                Amount = 40,
                Instigator = _playerGo
            };

            _bossAI.TakeDamage(damageToSide);

            Assert.AreEqual(60, _bossAI.Health);
            Assert.AreEqual(2, _bossAI.CurrentPhase);
            Assert.AreEqual(2, phaseReceived);

            DamageInfo damageToSidePhase3 = new DamageInfo {
                Amount = 30,
                Instigator = _playerGo
            };

            _bossAI.TakeDamage(damageToSidePhase3);

            Assert.AreEqual(30, _bossAI.Health);
            Assert.AreEqual(3, _bossAI.CurrentPhase);
            Assert.AreEqual(3, phaseReceived);
        }

        [Test]
        public void FrontShield_AbsorbsAndReflectsFrontalDamage_SideTakesNormalDamage() {
            _bossGo.transform.position = Vector3.zero;
            _bossGo.transform.rotation = Quaternion.identity;

            // Player in front
            _playerGo.transform.position = new Vector3(0f, 0f, 5f);

            bool reflectEventFired = false;
            int reflectedDamage = 0;
            EventBroker.Subscribe<PlayerTakeDamageEvent>(e => {
                reflectEventFired = true;
                reflectedDamage = e.Damage;
            });

            DamageInfo frontalDmg = new DamageInfo {
                Amount = 15,
                Instigator = _playerGo
            };

            _bossAI.TakeDamage(frontalDmg);

            Assert.AreEqual(100, _bossAI.Health);
            Assert.IsTrue(reflectEventFired);
            Assert.AreEqual(15, reflectedDamage);

            // Player to the side
            _playerGo.transform.position = new Vector3(5f, 0f, 0f);
            reflectEventFired = false;

            DamageInfo sideDmg = new DamageInfo {
                Amount = 15,
                Instigator = _playerGo
            };

            _bossAI.TakeDamage(sideDmg);

            Assert.AreEqual(85, _bossAI.Health);
            Assert.IsFalse(reflectEventFired);
        }
    }
}
