using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// 일정 주기마다 적을 플레이어 주변 반경 내에 생성하는 유틸리티 클래스입니다.
/// </summary>
public class EnemySpawner : MonoBehaviour {
    [FormerlySerializedAs("_enemyPrefab")]
    [SerializeField] private GameObject enemyPrefab;

    [FormerlySerializedAs("_spawnRadius")]
    [SerializeField] private float spawnRadius = 25f;

    [FormerlySerializedAs("_spawnRate")]
    [SerializeField] private float spawnInterval = 2f;

    private void Start() {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine() {
        while (true) {
            Vector3 pos = Random.insideUnitCircle.normalized * spawnRadius;
            pos.z = pos.y;
            pos.y = Random.Range(1f, 5f);

            if (enemyPrefab != null) {
                Instantiate(enemyPrefab, pos, Quaternion.identity);
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
