using System.Collections;
using UnityEngine;

namespace Potop.Client.Gameplay {
    /// <summary>
    /// 일정 주기마다 적을 플레이어 주변 반경 내에 생성하는 유틸리티 클래스입니다.
    /// </summary>
    public class EnemySpawner : MonoBehaviour {
        [SerializeField] private float _spawnRadius = 25f;
        [SerializeField] private Wave.WaveManager _waveManager;

        private const float MIN_HEIGHT = 1f;
        private const float MAX_HEIGHT = 5f;

        private void Start() {
            if (_waveManager == null) {
                _waveManager = FindFirstObjectByType<Wave.WaveManager>();
            }
            StartCoroutine(SpawnRoutine());
        }

        private IEnumerator SpawnRoutine() {
            while (true) {
                if (_waveManager != null && _waveManager.IsWaveActive) {
                    Wave.WaveData currentWave = _waveManager.CurrentWaveData;
                    if (currentWave != null && currentWave.EnemySpawns.Count > 0) {
                        SpawnEnemy(currentWave);
                        
                        float interval;
                        if (_waveManager.IsOverclockActive) {
                            interval = _waveManager.OverclockSpawnInterval;
                        } else {
                            // 진행도에 따른 스폰 간격 계산
                            float progress = _waveManager.CurrentWaveProgress;
                            float intensity = currentWave.SpawnIntensityCurve.Evaluate(progress);
                            interval = currentWave.BaseSpawnInterval / Mathf.Max(0.1f, intensity);
                        }
                        
                        yield return new WaitForSeconds(interval);
                    } else {
                        yield return new WaitForSeconds(1f);
                    }
                } else {
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }

        private void SpawnEnemy(Wave.WaveData waveData) {
            if (waveData == null || waveData.EnemySpawns == null || waveData.EnemySpawns.Count == 0) return;

            float totalWeight = 0f;
            foreach (var spawn in waveData.EnemySpawns) {
                if (spawn.Prefab != null) {
                    var enemy = spawn.Prefab.GetComponent<EnemyBase>();
                    float weight = (enemy != null && enemy.EnemyData != null) ? enemy.EnemyData.SpawnWeight : 1f;
                    totalWeight += Mathf.Max(0f, weight);
                }
            }

            if (totalWeight <= 0f) {
                // Fallback to simple random selection if total weight is 0
                int index = Random.Range(0, waveData.EnemySpawns.Count);
                GameObject fallbackPrefab = waveData.EnemySpawns[index].Prefab;
                if (fallbackPrefab != null) {
                    SpawnPrefab(fallbackPrefab);
                }
                return;
            }

            float randomValue = Random.Range(0f, totalWeight);
            float currentSum = 0f;
            GameObject selectedPrefab = null;

            foreach (var spawn in waveData.EnemySpawns) {
                if (spawn.Prefab != null) {
                    var enemy = spawn.Prefab.GetComponent<EnemyBase>();
                    float weight = (enemy != null && enemy.EnemyData != null) ? enemy.EnemyData.SpawnWeight : 1f;
                    currentSum += Mathf.Max(0f, weight);
                    if (randomValue <= currentSum) {
                        selectedPrefab = spawn.Prefab;
                        break;
                    }
                }
            }

            // Fallback just in case
            if (selectedPrefab == null && waveData.EnemySpawns.Count > 0) {
                selectedPrefab = waveData.EnemySpawns[waveData.EnemySpawns.Count - 1].Prefab;
            }

            if (selectedPrefab != null) {
                SpawnPrefab(selectedPrefab);
            }
        }

        private void SpawnPrefab(GameObject prefab) {
            Vector3 pos = Random.insideUnitCircle.normalized * _spawnRadius;
            pos.z = pos.y;
            pos.y = Random.Range(MIN_HEIGHT, MAX_HEIGHT);

            Potop.Client.Core.Pooling.PoolManager.Instance.Spawn(prefab, pos, Quaternion.identity);
        }
    }
}
