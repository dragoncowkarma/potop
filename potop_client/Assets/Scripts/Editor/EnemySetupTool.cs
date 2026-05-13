using UnityEditor;
using UnityEngine;
using Potop.Client.Gameplay.AI.Variants;
using System.IO;

namespace Potop.Client.Editor {
    public static class EnemySetupTool {
        [MenuItem("Tools/Potop/Setup Enemy Visuals")]
        public static void SetupEnemyVisuals() {
            string materialsDir = "Assets/Materials/Enemies";
            string prefabsDir = "Assets/Prefabs/Enemies";

            if (!AssetDatabase.IsValidFolder(materialsDir)) {
                Directory.CreateDirectory(materialsDir);
                AssetDatabase.Refresh();
            }

            if (!AssetDatabase.IsValidFolder(prefabsDir)) {
                Directory.CreateDirectory(prefabsDir);
                AssetDatabase.Refresh();
            }

            // Create Materials
            Material blitzMat = CreateMaterial(materialsDir + "/BlitzMaterial.mat", new Color(1f, 1f, 0f, 1f), 0f, 0.5f);
            Material armoredMat = CreateMaterial(materialsDir + "/ArmoredMaterial.mat", new Color(0.2f, 0.2f, 0.2f, 1f), 1f, 0.5f);
            Material swarmMat = CreateMaterial(materialsDir + "/SwarmMaterial.mat", new Color(0.5f, 0f, 0.5f, 1f), 0f, 0.5f);

            // Load Base Prefab
            GameObject basePrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/EnemyBot.prefab");
            if (basePrefab == null) {
                Debug.LogError("Base prefab EnemyBot not found!");
                return;
            }

            // Create Prefab Variants
            CreatePrefabVariant(basePrefab, prefabsDir + "/BlitzEnemy.prefab", blitzMat, typeof(BlitzEnemy));
            CreatePrefabVariant(basePrefab, prefabsDir + "/ArmoredEnemy.prefab", armoredMat, typeof(ArmoredEnemy));
            CreatePrefabVariant(basePrefab, prefabsDir + "/SwarmEnemy.prefab", swarmMat, typeof(SwarmEnemy));

            Debug.Log("Enemy visuals setup complete!");
        }

        private static Material CreateMaterial(string path, Color color, float metallic, float smoothness) {
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);
            if (mat == null) {
                mat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
                mat.color = color;
                mat.SetFloat("_Metallic", metallic);
                mat.SetFloat("_Smoothness", smoothness);
                AssetDatabase.CreateAsset(mat, path);
            } else {
                mat.color = color;
                mat.SetFloat("_Metallic", metallic);
                mat.SetFloat("_Smoothness", smoothness);
                EditorUtility.SetDirty(mat);
            }
            return mat;
        }

        private static void CreatePrefabVariant(GameObject basePrefab, string path, Material mat, System.Type componentType) {
            GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(basePrefab);
            
            // Assign material
            Renderer[] renderers = instance.GetComponentsInChildren<Renderer>();
            foreach (var r in renderers) {
                r.sharedMaterial = mat;
            }

            // Remove missing scripts
            GameObjectUtility.RemoveMonoBehavioursWithMissingScript(instance);

            // Remove EnemyBase and add variant script
            var oldComponent = instance.GetComponent<Potop.Client.Gameplay.EnemyBase>();
            if (oldComponent != null && oldComponent.GetType() != componentType) {
                Object.DestroyImmediate(oldComponent, true);
                instance.AddComponent(componentType);
            } else if (oldComponent == null) {
                instance.AddComponent(componentType);
            }

            PrefabUtility.SaveAsPrefabAsset(instance, path);
            Object.DestroyImmediate(instance);
        }
    }
}
