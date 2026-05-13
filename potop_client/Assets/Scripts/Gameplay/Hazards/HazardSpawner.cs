using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Potop.Client.Core.Pooling;

namespace Potop.Client.Gameplay.Hazards {
    /// <summary>
    /// 게임 내 환경 요소(Hazard)들의 초기 배치와 리스폰을 관리하는 매니저 클래스입니다.
    /// 정해진 스폰 포인트에 환경 요소를 생성하고, 파괴되면 일정 시간 후 재스폰합니다.
    /// </summary>
    public class HazardSpawner : MonoBehaviour {
        [System.Serializable]
        public class HazardSpawnConfig {
            public GameObject HazardPrefab;
            public Transform[] SpawnPoints;
            public float RespawnTime = 30f;
        }

        [SerializeField] private HazardSpawnConfig[] _spawnConfigs;

        // 스폰 포인트와 현재 활성화된 인스턴스
        private Dictionary<Transform, GameObject> _activeHazards = new Dictionary<Transform, GameObject>();
        // 현재 리스폰 대기 중인 스폰 포인트를 추적하여 중복 코루틴 실행을 방지합니다.
        private HashSet<Transform> _respawningPoints = new HashSet<Transform>();

        private void Start() {
            InitializeSpawners();
        }

        /// <summary>
        /// 모든 설정된 스폰 포인트에 대해 초기 스폰을 수행합니다.
        /// </summary>
        private void InitializeSpawners() {
            if (_spawnConfigs == null) return;

            foreach (var config in _spawnConfigs) {
                if (config.HazardPrefab == null || config.SpawnPoints == null) continue;

                foreach (Transform spawnPoint in config.SpawnPoints) {
                    if (spawnPoint != null) {
                        SpawnHazard(config, spawnPoint);
                    }
                }
            }
        }

        /// <summary>
        /// 특정 스폰 포인트에 환경 요소를 생성합니다.
        /// </summary>
        private void SpawnHazard(HazardSpawnConfig config, Transform spawnPoint) {
            if (PoolManager.Instance != null) {
                GameObject hazardInstance = PoolManager.Instance.Spawn(config.HazardPrefab, spawnPoint.position, spawnPoint.rotation);
                _activeHazards[spawnPoint] = hazardInstance;
                _respawningPoints.Remove(spawnPoint);
            } else {
                Debug.LogWarning("PoolManager is missing. HazardSpawner cannot spawn hazards.");
            }
        }

        private void Update() {
            CheckAndRespawnHazards();
        }

        /// <summary>
        /// 매 프레임 활성화된 환경 요소들의 상태를 확인하고, 비활성화(파괴/풀 반환)된 요소에 대해 리스폰 타이머를 시작합니다.
        /// </summary>
        private void CheckAndRespawnHazards() {
            if (_spawnConfigs == null) return;

            foreach (var config in _spawnConfigs) {
                if (config.HazardPrefab == null || config.SpawnPoints == null) continue;

                foreach (Transform spawnPoint in config.SpawnPoints) {
                    if (spawnPoint == null) continue;

                    if (_respawningPoints.Contains(spawnPoint)) continue;

                    // 스폰 포인트에 할당된 오브젝트가 없거나 비활성화(Despawn) 상태인 경우 리스폰 진행
                    if (_activeHazards.TryGetValue(spawnPoint, out GameObject instance)) {
                        if (instance == null || !instance.activeInHierarchy) {
                            StartCoroutine(RespawnRoutine(config, spawnPoint));
                        }
                    } else {
                        // 초기화가 안되었거나 인스턴스가 아예 없는 경우
                        StartCoroutine(RespawnRoutine(config, spawnPoint));
                    }
                }
            }
        }

        /// <summary>
        /// 지정된 시간 대기 후 환경 요소를 재스폰하는 코루틴입니다.
        /// </summary>
        private IEnumerator RespawnRoutine(HazardSpawnConfig config, Transform spawnPoint) {
            _respawningPoints.Add(spawnPoint);
            _activeHazards.Remove(spawnPoint);

            yield return new WaitForSeconds(config.RespawnTime);

            SpawnHazard(config, spawnPoint);
        }
    }
}
