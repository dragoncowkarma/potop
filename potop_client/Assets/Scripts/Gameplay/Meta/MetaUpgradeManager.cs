using UnityEngine;
using Potop.Client.Core.Events;

namespace Potop.Client.Gameplay.Meta {
    /// <summary>
    /// 6개의 영구 강화 항목을 관리합니다.
    /// 각 항목의 레벨을 PlayerPrefs로 저장하고 GemWallet을 통해 비용을 소모합니다.
    /// </summary>
    public class MetaUpgradeManager : MonoBehaviour {
        private const string PREFS_PREFIX = "meta_upgrade_";
        private const string PREFS_SUFFIX = "_level";

        [Header("Upgrade Data (6 slots)")]
        [SerializeField] private MetaUpgradeData[] _upgrades;

        public static MetaUpgradeManager Instance { get; private set; }

        /// <summary>할당된 강화 데이터 배열(읽기 전용)입니다.</summary>
        public MetaUpgradeData[] Upgrades => _upgrades;

        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            if (Application.isPlaying) {
                DontDestroyOnLoad(gameObject);
            }
        }

        /// <summary>
        /// 특정 강화 항목의 현재 레벨을 반환합니다.
        /// </summary>
        public int GetLevel(string upgradeId) {
            return PlayerPrefs.GetInt(BuildKey(upgradeId), 0);
        }

        /// <summary>
        /// 강화 항목을 구매(레벨업)합니다.
        /// 성공 시 true를 반환하고 <see cref="MetaUpgradePurchasedEvent"/>를 발행합니다.
        /// </summary>
        public bool TryPurchase(MetaUpgradeData data) {
            if (data == null) {
                Debug.LogWarning("[MetaUpgradeManager] TryPurchase: data is null.");
                return false;
            }

            int currentLevel = GetLevel(data.UpgradeId);
            if (currentLevel >= data.MaxLevel) {
                Debug.Log($"[MetaUpgradeManager] {data.DisplayName} is already at max level.");
                return false;
            }

            int cost = data.GetCost(currentLevel + 1);
            if (GemWallet.Instance == null) {
                Debug.LogWarning("[MetaUpgradeManager] GemWallet.Instance is null.");
                return false;
            }

            if (!GemWallet.Instance.TrySpend(cost)) {
                Debug.Log($"[MetaUpgradeManager] Not enough gems. Need {cost}, have {GemWallet.Instance.Balance}.");
                return false;
            }

            int newLevel = currentLevel + 1;
            PlayerPrefs.SetInt(BuildKey(data.UpgradeId), newLevel);
            PlayerPrefs.Save();

            EventBroker.Publish(new MetaUpgradePurchasedEvent {
                UpgradeId = data.UpgradeId,
                NewLevel = newLevel
            });

            return true;
        }

        /// <summary>
        /// 모든 강화 항목의 현재 효과값을 집약하여 반환하는 헬퍼입니다.
        /// 인게임 시스템이 구독하여 실제 스탯에 반영합니다.
        /// </summary>
        public MetaStatBundle GetStatBundle() {
            var bundle = new MetaStatBundle();
            if (_upgrades == null) return bundle;

            foreach (var data in _upgrades) {
                if (data == null) continue;
                int level = GetLevel(data.UpgradeId);
                float effect = data.GetEffect(level);

                switch (data.UpgradeId) {
                    case "armor":       bundle.BonusHp += (int)effect;        break;
                    case "motor":       bundle.RotationSpeedMultiplier += effect; break;
                    case "energy":      bundle.SkillChargeBonus += effect;    break;
                    case "magnet":      bundle.GemRadiusMultiplier += effect;  break;
                    case "cooling":     bundle.OverchargeBonus += effect;     break;
                    case "scanner":     bundle.HazardRateBonus += effect;     break;
                    default:
                        Debug.LogWarning($"[MetaUpgradeManager] Unknown upgradeId: {data.UpgradeId}");
                        break;
                }
            }

            return bundle;
        }

        private static string BuildKey(string upgradeId) {
            return PREFS_PREFIX + upgradeId + PREFS_SUFFIX;
        }
    }

    /// <summary>
    /// 모든 메타 강화 효과를 집약한 데이터 구조체입니다.
    /// </summary>
    public struct MetaStatBundle {
        public int BonusHp;
        public float RotationSpeedMultiplier;
        public float SkillChargeBonus;
        public float GemRadiusMultiplier;
        public float OverchargeBonus;
        public float HazardRateBonus;
    }
}
