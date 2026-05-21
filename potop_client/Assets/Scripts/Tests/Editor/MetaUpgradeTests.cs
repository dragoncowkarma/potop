using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using Potop.Client.Gameplay.Meta;
using Potop.Client.Core.Events;

namespace Potop.Client.Tests.EditMode {
    public class MetaUpgradeTests {
        private GameObject _goManager;
        private MetaUpgradeManager _manager;
        private GameObject _goWallet;
        private GemWallet _wallet;
        private List<Object> _createdObjects;

        private Dictionary<string, int> _originalPrefs;
        private readonly string[] _keysToMock = new string[] {
            "meta_upgrade_armor_level",
            "meta_upgrade_motor_level",
            "gem_wallet_balance"
        };

        [SetUp]
        public void Setup() {
            _createdObjects = new List<Object>();
            _originalPrefs = new Dictionary<string, int>();

            // Backup and clear specific keys
            foreach(var key in _keysToMock) {
                if(PlayerPrefs.HasKey(key)) {
                    _originalPrefs[key] = PlayerPrefs.GetInt(key);
                }
                PlayerPrefs.DeleteKey(key);
            }

            EventBroker.ClearAllSubscriptions();

            _goManager = new GameObject();
            _createdObjects.Add(_goManager);
            _manager = _goManager.AddComponent<MetaUpgradeManager>();

            // Manually run Awake to assign MetaUpgradeManager.Instance
            var managerAwake = typeof(MetaUpgradeManager).GetMethod("Awake", BindingFlags.Instance | BindingFlags.NonPublic);
            managerAwake?.Invoke(_manager, null);

            _goWallet = new GameObject();
            _createdObjects.Add(_goWallet);
            _wallet = _goWallet.AddComponent<GemWallet>();

            // Manually run Awake to assign GemWallet.Instance
            var walletAwake = typeof(GemWallet).GetMethod("Awake", BindingFlags.Instance | BindingFlags.NonPublic);
            walletAwake?.Invoke(_wallet, null);
        }

        [TearDown]
        public void Teardown() {
            foreach(var obj in _createdObjects) {
                if(obj != null) {
                    Object.DestroyImmediate(obj);
                }
            }
            _createdObjects.Clear();

            // Clear static instances
            var managerInstanceProperty = typeof(MetaUpgradeManager).GetProperty("Instance", BindingFlags.Static | BindingFlags.Public);
            managerInstanceProperty?.GetSetMethod(true)?.Invoke(null, new object[] { null });

            var walletInstanceProperty = typeof(GemWallet).GetProperty("Instance", BindingFlags.Static | BindingFlags.Public);
            walletInstanceProperty?.GetSetMethod(true)?.Invoke(null, new object[] { null });

            // Restore original prefs
            foreach(var key in _keysToMock) {
                PlayerPrefs.DeleteKey(key); // clear the mock values
                if(_originalPrefs.ContainsKey(key)) {
                    PlayerPrefs.SetInt(key, _originalPrefs[key]);
                }
            }

            EventBroker.ClearAllSubscriptions();
        }

        [Test]
        public void TryPurchase_MaxLevelReached_ReturnsFalse() {
            var data = ScriptableObject.CreateInstance<MetaUpgradeData>();
            _createdObjects.Add(data);
            typeof(MetaUpgradeData).GetField("_upgradeId", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(data, "armor");
            typeof(MetaUpgradeData).GetField("_costPerLevel", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(data, new int[] { 100 });

            PlayerPrefs.SetInt("meta_upgrade_armor_level", 1); // Set to max level

            bool result = _manager.TryPurchase(data);

            Assert.IsFalse(result);
        }

        [Test]
        public void TryPurchase_InsufficientGems_ReturnsFalse() {
            var data = ScriptableObject.CreateInstance<MetaUpgradeData>();
            _createdObjects.Add(data);
            typeof(MetaUpgradeData).GetField("_upgradeId", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(data, "armor");
            typeof(MetaUpgradeData).GetField("_costPerLevel", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(data, new int[] { 100 }); // Cost is 100

            // Wallet has 0 gems by default

            bool result = _manager.TryPurchase(data);

            Assert.IsFalse(result);
        }

        [Test]
        public void TryPurchase_NormalConditions_LevelIncreasesAndGemsDeducted() {
            var data = ScriptableObject.CreateInstance<MetaUpgradeData>();
            _createdObjects.Add(data);
            typeof(MetaUpgradeData).GetField("_upgradeId", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(data, "armor");
            typeof(MetaUpgradeData).GetField("_costPerLevel", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(data, new int[] { 100 });

            _wallet.Earn(150); // Provide enough gems

            bool eventFired = false;
            EventBroker.Subscribe<MetaUpgradePurchasedEvent>(e => {
                eventFired = true;
                Assert.AreEqual("armor", e.UpgradeId);
                Assert.AreEqual(1, e.NewLevel);
            });

            bool result = _manager.TryPurchase(data);

            Assert.IsTrue(result);
            Assert.AreEqual(1, _manager.GetLevel("armor"));
            Assert.AreEqual(50, _wallet.Balance);
            Assert.IsTrue(eventFired);
        }

        [Test]
        public void GetStatBundle_AggregatesAllBonusesCorrectly() {
            var armorData = ScriptableObject.CreateInstance<MetaUpgradeData>();
            _createdObjects.Add(armorData);
            typeof(MetaUpgradeData).GetField("_upgradeId", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(armorData, "armor");
            typeof(MetaUpgradeData).GetField("_effectPerLevel", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(armorData, new float[] { 50f });

            var motorData = ScriptableObject.CreateInstance<MetaUpgradeData>();
            _createdObjects.Add(motorData);
            typeof(MetaUpgradeData).GetField("_upgradeId", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(motorData, "motor");
            typeof(MetaUpgradeData).GetField("_effectPerLevel", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(motorData, new float[] { 0.1f });

            var upgrades = new MetaUpgradeData[] { armorData, motorData };
            typeof(MetaUpgradeManager).GetField("_upgrades", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(_manager, upgrades);

            PlayerPrefs.SetInt("meta_upgrade_armor_level", 1);
            PlayerPrefs.SetInt("meta_upgrade_motor_level", 1);

            var bundle = _manager.GetStatBundle();

            Assert.AreEqual(50, bundle.BonusHp);
            Assert.AreEqual(0.1f, bundle.RotationSpeedMultiplier);
            Assert.AreEqual(0f, bundle.SkillChargeBonus); // Others should be default
        }
    }
}
