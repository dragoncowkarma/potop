using System.Collections;
using UnityEngine;

namespace Potop.Client.Gameplay {
    /// <summary>
    /// 일정 주기마다 적을 플레이어 주변 반경 내에 생성하는 유틸리티 클래스입니다.
    /// </summary>
    public class EnemySpawner : MonoBehaviour {
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private float _spawnRadius = 25f;
        [SerializeField] private float _spawnInterval = 2f;

        private const float MIN_HEIGHT = 1f;
        private const float MAX_HEIGHT = 5f;

        private void Start() {
            StartCoroutine(SpawnRoutine());
        }

        /// <summary>
        /// 지정된 주기마다 적을 생성하는 코루틴입니다.
        /// </summary>
        /// <returns>코루틴 지연 시간</returns>
        private IEnumerator SpawnRoutine() {
            while (true) {
                if (_enemyPrefab != null) {
                    Vector3 pos = Random.insideUnitCircle.normalized * _spawnRadius;
                    pos.z = pos.y;
                    pos.y = Random.Range(MIN_HEIGHT, MAX_HEIGHT);

                    Instantiate(_enemyPrefab, pos, Quaternion.identity);
                }
                yield return new WaitForSeconds(_spawnInterval);
            }
        }
    }
}
