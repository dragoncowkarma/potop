using System.Collections.Generic;
using UnityEngine;
using Potop.Client.Gameplay;

namespace Potop.Client.Gameplay.Progression {
    /// <summary>
    /// Manages the pool of available upgrades and handles weighted probability random extraction and pity mechanisms.
    /// </summary>
    public class UpgradePool : MonoBehaviour {
        [Tooltip("Fallback local list of upgrade options if UpgradeTableData is not assigned")]
        [SerializeField] private List<UpgradeOption> _availableUpgrades = new List<UpgradeOption>();

        [Tooltip("ScriptableObject containing weighted probabilities and the master upgrade list")]
        [SerializeField] private UpgradeTableData _upgradeTable;

        private System.Random _random = new System.Random();
        private int _epicPityCounter = 0;

        /// <summary>
        /// Gets the current pity counter (consecutive option draws without Epic).
        /// </summary>
        public int EpicPityCounter => _epicPityCounter;

        /// <summary>
        /// Sets the pity counter directly (useful for testing and debugging).
        /// </summary>
        public void SetEpicPityCounter(int count) {
            _epicPityCounter = count;
        }

        /// <summary>
        /// Selects a specified number of unique upgrade options based on weights and the pity system.
        /// </summary>
        /// <param name="count">Number of upgrade options to return</param>
        /// <returns>A list of selected UpgradeOption structs</returns>
        public List<UpgradeOption> GetRandomUpgrades(int count) {
            List<UpgradeOption> poolOptions = _upgradeTable != null ? _upgradeTable.UpgradeOptions : _availableUpgrades;

            if (poolOptions == null || poolOptions.Count == 0) {
                Debug.LogWarning("[UpgradePool] Upgrade pool is empty.");
                return new List<UpgradeOption>();
            }

            int extractCount = Mathf.Min(count, poolOptions.Count);
            List<UpgradeOption> selectedOptions = new List<UpgradeOption>();

            float commonWeight = _upgradeTable != null ? _upgradeTable.CommonWeight : 70f;
            float rareWeight = _upgradeTable != null ? _upgradeTable.RareWeight : 25f;
            float epicWeight = _upgradeTable != null ? _upgradeTable.EpicWeight : 5f;
            float totalWeight = commonWeight + rareWeight + epicWeight;

            for (int i = 0; i < extractCount; i++) {
                UpgradeRarity rolledRarity;
                bool isPityTriggered = (_epicPityCounter >= 10);

                if (isPityTriggered) {
                    rolledRarity = UpgradeRarity.Epic;
                } else {
                    float roll = (float)_random.NextDouble() * totalWeight;
                    if (roll < commonWeight) {
                        rolledRarity = UpgradeRarity.Common;
                    } else if (roll < commonWeight + rareWeight) {
                        rolledRarity = UpgradeRarity.Rare;
                    } else {
                        rolledRarity = UpgradeRarity.Epic;
                    }
                }

                // Filter options that match the rolled rarity and are not already selected
                List<UpgradeOption> availableOfRarity = new List<UpgradeOption>();
                for (int j = 0; j < poolOptions.Count; j++) {
                    UpgradeOption opt = poolOptions[j];
                    if (opt.Rarity == rolledRarity) {
                        bool alreadySelected = false;
                        for (int k = 0; k < selectedOptions.Count; k++) {
                            if (selectedOptions[k].UpgradeId == opt.UpgradeId) {
                                alreadySelected = true;
                                break;
                            }
                        }
                        if (!alreadySelected) {
                            availableOfRarity.Add(opt);
                        }
                    }
                }

                // Graceful fallback to any remaining unselected option if rarity is depleted
                if (availableOfRarity.Count == 0) {
                    for (int j = 0; j < poolOptions.Count; j++) {
                        UpgradeOption opt = poolOptions[j];
                        bool alreadySelected = false;
                        for (int k = 0; k < selectedOptions.Count; k++) {
                            if (selectedOptions[k].UpgradeId == opt.UpgradeId) {
                                alreadySelected = true;
                                break;
                            }
                        }
                        if (!alreadySelected) {
                            availableOfRarity.Add(opt);
                        }
                    }
                }

                if (availableOfRarity.Count == 0) {
                    break;
                }

                int randomIndex = _random.Next(availableOfRarity.Count);
                UpgradeOption selectedOption = availableOfRarity[randomIndex];
                selectedOptions.Add(selectedOption);

                // Reset pity counter on Epic, increment on non-Epic
                if (selectedOption.Rarity == UpgradeRarity.Epic) {
                    _epicPityCounter = 0;
                } else {
                    _epicPityCounter++;
                }
            }

            return selectedOptions;
        }
    }
}
