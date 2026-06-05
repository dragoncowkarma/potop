using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using Potop.Client.Gameplay.Weapons;
using Potop.Client.Data;
using Potop.Client.Gameplay;

namespace Potop.Client.Editor {
    public static class BalanceDataImporter {
        private const string WEAPON_CSV_PATH = "Assets/Data/Balance/WeaponBalanceData.csv";
        private const string ENEMY_CSV_PATH = "Assets/Data/Balance/EnemyBalanceData.csv";

        [MenuItem("Tools/Potop/Import Balance Data")]
        public static void ImportBalanceData() {
            try {
                Debug.Log("Starting balance data import...");
                ImportWeapons();
                ImportEnemies();
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                Debug.Log("Balance data import completed successfully!");
            } catch (Exception ex) {
                Debug.LogError($"Import failed: {ex.Message}");
                throw;
            }
        }

        private static void ImportWeapons() {
            if (!File.Exists(WEAPON_CSV_PATH)) {
                throw new FileNotFoundException($"Weapon CSV not found at {WEAPON_CSV_PATH}");
            }

            string[] lines = File.ReadAllLines(WEAPON_CSV_PATH);
            ParseAndValidateWeapons(lines, (assetName, assetPath, baseDamage, baseFireRate, baseProjectileSpeed, spreadAngle, spreadProjectileCount, launchAngle, aoERadius, basePierce, knockbackForce) => {
                // Load or create asset
                WeaponData weaponData = AssetDatabase.LoadAssetAtPath<WeaponData>(assetPath);
                bool isNew = false;
                if (weaponData == null) {
                    string dir = Path.GetDirectoryName(assetPath);
                    if (!AssetDatabase.IsValidFolder(dir)) {
                        Directory.CreateDirectory(dir);
                        AssetDatabase.Refresh();
                    }
                    weaponData = ScriptableObject.CreateInstance<WeaponData>();
                    isNew = true;
                }

                weaponData.InitializeFromBalance(
                    baseDamage, baseFireRate, baseProjectileSpeed, spreadAngle,
                    spreadProjectileCount, launchAngle, aoERadius, basePierce, knockbackForce
                );

                if (isNew) {
                    AssetDatabase.CreateAsset(weaponData, assetPath);
                    Debug.Log($"Created new WeaponData asset at {assetPath}");
                } else {
                    EditorUtility.SetDirty(weaponData);
                    Debug.Log($"Updated WeaponData asset at {assetPath}");
                }
            });
        }

        public static void ParseAndValidateWeapons(string[] lines, Action<string, string, float, float, float, float, int, float, float, int, float> onRowParsed) {
            if (lines == null || lines.Length <= 1) {
                throw new Exception("Weapon CSV is empty or has only header.");
            }

            // Header line validation
            string header = lines[0];
            string[] expectedHeaders = {
                "AssetName", "AssetPath", "BaseDamage", "BaseFireRate", "BaseProjectileSpeed",
                "SpreadAngle", "SpreadProjectileCount", "LaunchAngle", "AoERadius", "BasePierce", "KnockbackForce"
            };
            string[] headerCols = header.Split(',');
            for (int i = 0; i < expectedHeaders.Length; i++) {
                if (i >= headerCols.Length || headerCols[i].Trim() != expectedHeaders[i]) {
                    throw new Exception($"Weapon CSV header mismatch. Expected column: {expectedHeaders[i]} at index {i}.");
                }
            }

            for (int r = 1; r < lines.Length; r++) {
                string line = lines[r].Trim();
                if (string.IsNullOrEmpty(line)) continue;

                string[] cols = line.Split(',');
                if (cols.Length < expectedHeaders.Length) {
                    throw new Exception($"Weapon CSV row {r + 1} has insufficient columns (found {cols.Length}, expected {expectedHeaders.Length}).");
                }

                string assetName = cols[0].Trim();
                string assetPath = cols[1].Trim();

                if (string.IsNullOrEmpty(assetName) || string.IsNullOrEmpty(assetPath)) {
                    throw new Exception($"Weapon CSV row {r + 1} has empty AssetName or AssetPath.");
                }

                // Numeric parsing & validation
                if (!float.TryParse(cols[2], out float baseDamage) || baseDamage < 0) {
                    throw new Exception($"Weapon CSV row {r + 1}: BaseDamage must be a non-negative float.");
                }
                if (!float.TryParse(cols[3], out float baseFireRate) || baseFireRate < 0) {
                    throw new Exception($"Weapon CSV row {r + 1}: BaseFireRate must be a non-negative float.");
                }
                if (!float.TryParse(cols[4], out float baseProjectileSpeed) || baseProjectileSpeed < 0) {
                    throw new Exception($"Weapon CSV row {r + 1}: BaseProjectileSpeed must be a non-negative float.");
                }
                if (!float.TryParse(cols[5], out float spreadAngle) || spreadAngle < 0) {
                    throw new Exception($"Weapon CSV row {r + 1}: SpreadAngle must be a non-negative float.");
                }
                if (!int.TryParse(cols[6], out int spreadProjectileCount) || spreadProjectileCount <= 0) {
                    throw new Exception($"Weapon CSV row {r + 1}: SpreadProjectileCount must be a positive integer.");
                }
                if (!float.TryParse(cols[7], out float launchAngle) || launchAngle < 0) {
                    throw new Exception($"Weapon CSV row {r + 1}: LaunchAngle must be a non-negative float.");
                }
                if (!float.TryParse(cols[8], out float aoERadius) || aoERadius < 0) {
                    throw new Exception($"Weapon CSV row {r + 1}: AoERadius must be a non-negative float.");
                }
                if (!int.TryParse(cols[9], out int basePierce) || basePierce < 0) {
                    throw new Exception($"Weapon CSV row {r + 1}: BasePierce must be a non-negative integer.");
                }
                if (!float.TryParse(cols[10], out float knockbackForce) || knockbackForce < 0) {
                    throw new Exception($"Weapon CSV row {r + 1}: KnockbackForce must be a non-negative float.");
                }

                onRowParsed?.Invoke(
                    assetName, assetPath, baseDamage, baseFireRate, baseProjectileSpeed,
                    spreadAngle, spreadProjectileCount, launchAngle, aoERadius, basePierce, knockbackForce
                );
            }
        }

        private static void ImportEnemies() {
            if (!File.Exists(ENEMY_CSV_PATH)) {
                throw new FileNotFoundException($"Enemy CSV not found at {ENEMY_CSV_PATH}");
            }

            string[] lines = File.ReadAllLines(ENEMY_CSV_PATH);
            ParseAndValidateEnemies(lines, (assetName, assetPath, enemyName, maxHealth, moveSpeed, scoreValue, energyReward, baseDamage, spawnWeight) => {
                // Load or create asset
                EnemyData enemyData = AssetDatabase.LoadAssetAtPath<EnemyData>(assetPath);
                bool isNew = false;
                if (enemyData == null) {
                    string dir = Path.GetDirectoryName(assetPath);
                    if (!AssetDatabase.IsValidFolder(dir)) {
                        Directory.CreateDirectory(dir);
                        AssetDatabase.Refresh();
                    }
                    enemyData = ScriptableObject.CreateInstance<EnemyData>();
                    isNew = true;
                }

                enemyData.InitializeFromBalance(
                    enemyName, maxHealth, moveSpeed, scoreValue, energyReward, baseDamage, spawnWeight
                );

                if (isNew) {
                    AssetDatabase.CreateAsset(enemyData, assetPath);
                    Debug.Log($"Created new EnemyData asset at {assetPath}");
                } else {
                    EditorUtility.SetDirty(enemyData);
                    Debug.Log($"Updated EnemyData asset at {assetPath}");
                }

                // Bind asset back to its respective prefab
                string prefabPath = GetPrefabPathForEnemy(enemyName);
                if (!string.IsNullOrEmpty(prefabPath) && File.Exists(prefabPath)) {
                    BindEnemyDataToPrefab(prefabPath, enemyData);
                } else {
                    Debug.LogWarning($"Prefab not found for enemy '{enemyName}' at expected path, skipping binding.");
                }
            });
        }

        public static void ParseAndValidateEnemies(string[] lines, Action<string, string, string, int, float, int, int, int, float> onRowParsed) {
            if (lines == null || lines.Length <= 1) {
                throw new Exception("Enemy CSV is empty or has only header.");
            }

            // Header line validation
            string header = lines[0];
            string[] expectedHeaders = {
                "AssetName", "AssetPath", "EnemyName", "MaxHealth", "MoveSpeed",
                "ScoreValue", "EnergyReward", "BaseDamage", "SpawnWeight"
            };
            string[] headerCols = header.Split(',');
            for (int i = 0; i < expectedHeaders.Length; i++) {
                if (i >= headerCols.Length || headerCols[i].Trim() != expectedHeaders[i]) {
                    throw new Exception($"Enemy CSV header mismatch. Expected column: {expectedHeaders[i]} at index {i}.");
                }
            }

            for (int r = 1; r < lines.Length; r++) {
                string line = lines[r].Trim();
                if (string.IsNullOrEmpty(line)) continue;

                string[] cols = line.Split(',');
                if (cols.Length < expectedHeaders.Length) {
                    throw new Exception($"Enemy CSV row {r + 1} has insufficient columns (found {cols.Length}, expected {expectedHeaders.Length}).");
                }

                string assetName = cols[0].Trim();
                string assetPath = cols[1].Trim();
                string enemyName = cols[2].Trim();

                if (string.IsNullOrEmpty(assetName) || string.IsNullOrEmpty(assetPath) || string.IsNullOrEmpty(enemyName)) {
                    throw new Exception($"Enemy CSV row {r + 1} has empty AssetName, AssetPath, or EnemyName.");
                }

                // Numeric parsing & validation
                if (!int.TryParse(cols[3], out int maxHealth) || maxHealth <= 0) {
                    throw new Exception($"Enemy CSV row {r + 1}: MaxHealth must be a positive integer.");
                }
                if (!float.TryParse(cols[4], out float moveSpeed) || moveSpeed < 0) {
                    throw new Exception($"Enemy CSV row {r + 1}: MoveSpeed must be a non-negative float.");
                }
                if (!int.TryParse(cols[5], out int scoreValue) || scoreValue < 0) {
                    throw new Exception($"Enemy CSV row {r + 1}: ScoreValue must be a non-negative integer.");
                }
                if (!int.TryParse(cols[6], out int energyReward) || energyReward < 0) {
                    throw new Exception($"Enemy CSV row {r + 1}: EnergyReward must be a non-negative integer.");
                }
                if (!int.TryParse(cols[7], out int baseDamage) || baseDamage < 0) {
                    throw new Exception($"Enemy CSV row {r + 1}: BaseDamage must be a non-negative integer.");
                }
                if (!float.TryParse(cols[8], out float spawnWeight) || spawnWeight < 0) {
                    throw new Exception($"Enemy CSV row {r + 1}: SpawnWeight must be a non-negative float.");
                }

                onRowParsed?.Invoke(
                    assetName, assetPath, enemyName, maxHealth, moveSpeed,
                    scoreValue, energyReward, baseDamage, spawnWeight
                );
            }
        }

        private static string GetPrefabPathForEnemy(string enemyName) {
            switch (enemyName) {
                case "Scouter":
                    return "Assets/Prefabs/EnemyBot.prefab";
                case "Blitz":
                    return "Assets/Prefabs/Enemies/BlitzEnemy.prefab";
                case "Armored":
                    return "Assets/Prefabs/Enemies/ArmoredEnemy.prefab";
                case "Swarm Pod":
                    return "Assets/Prefabs/Enemies/SwarmEnemy.prefab";
                case "Titan Core":
                    return "Assets/Prefabs/Enemies/TitanCore.prefab";
                default:
                    return null;
            }
        }

        private static void BindEnemyDataToPrefab(string prefabPath, EnemyData enemyData) {
            GameObject root = PrefabUtility.LoadPrefabContents(prefabPath);
            try {
                var component = root.GetComponent<EnemyBase>();
                if (component != null) {
                    var serializedObject = new SerializedObject(component);
                    var property = serializedObject.FindProperty("_enemyData");
                    if (property != null) {
                        property.objectReferenceValue = enemyData;
                        serializedObject.ApplyModifiedProperties();
                        PrefabUtility.SaveAsPrefabAsset(root, prefabPath);
                        Debug.Log($"Successfully bound EnemyData '{enemyData.name}' to prefab '{prefabPath}'");
                    } else {
                        Debug.LogError($"_enemyData field not found on component of prefab '{prefabPath}'");
                    }
                } else {
                    Debug.LogWarning($"EnemyBase component not found on root of prefab '{prefabPath}'");
                }
            } catch (Exception ex) {
                Debug.LogError($"Failed to bind EnemyData to prefab '{prefabPath}': {ex.Message}");
            } finally {
                PrefabUtility.UnloadPrefabContents(root);
            }
        }
    }
}
