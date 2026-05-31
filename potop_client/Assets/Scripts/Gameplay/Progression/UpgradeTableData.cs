using System.Collections.Generic;
using UnityEngine;

namespace Potop.Client.Gameplay.Progression {
    public enum UpgradeRarity {
        Common,
        Rare,
        Epic
    }

    /// <summary>
    /// Represents a single passive upgrade option that can be offered to the player.
    /// </summary>
    [System.Serializable]
    public struct UpgradeOption {
        public string UpgradeId;
        public string DisplayName;
        public string Description;
        public UpgradeRarity Rarity;
        public Sprite Icon;
        public int RarityWeight;

        [Tooltip("Associated modifier for synergy tracking")]
        public Potop.Client.Gameplay.ModifierType AssociatedModifier;
    }
}

namespace Potop.Client.Gameplay {
    using Potop.Client.Gameplay.Progression;

    /// <summary>
    /// ScriptableObject mapping weights to each UpgradeRarity and storing the master upgrade list.
    /// Used by UpgradePool to randomly draw items based on probability settings.
    /// </summary>
    [CreateAssetMenu(fileName = "UpgradeTableData", menuName = "Potop/Progression/UpgradeTableData")]
    public class UpgradeTableData : ScriptableObject {
        [Tooltip("Weight for Common rarity options")]
        [SerializeField] private float _commonWeight = 70f;

        [Tooltip("Weight for Rare rarity options")]
        [SerializeField] private float _rareWeight = 25f;

        [Tooltip("Weight for Epic rarity options")]
        [SerializeField] private float _epicWeight = 5f;

        [Tooltip("Master list of all available upgrade options in the game")]
        [SerializeField] private List<UpgradeOption> _upgradeOptions = new List<UpgradeOption>();

        public float CommonWeight => _commonWeight;
        public float RareWeight => _rareWeight;
        public float EpicWeight => _epicWeight;
        public List<UpgradeOption> UpgradeOptions => _upgradeOptions;
    }
}
