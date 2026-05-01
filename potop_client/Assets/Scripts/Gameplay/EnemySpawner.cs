using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// 일정 주기마다 적을 플레이어 주변 반경 내에 생성하는 유틸리티 클래스입니다.
/// </summary>
public class EnemySpawner : MonoBehaviour {
    [SerializeField, FormerlySerializedAs("enemyPrefab")] private GameObject _enemyPrefab;
    [SerializeField, FormerlySerializedAs("spawnRate")] private float _spawnRate = 2f;
    [SerializeField, FormerlySerializedAs("spawnRadius")] private float _spawnRadius = 25f;

    private float _nextSpawnTime = 0f;

    private const float MIN_HEIGHT = 1f;
    private const float MAX_HEIGHT = 5f;

    private void Update() {
        if (Time.time >= _nextSpawnTime) {
            SpawnEnemy();
            _nextSpawnTime = Time.time + _spawnRate;
        }
    }

    private void SpawnEnemy() {
        if (_enemyPrefab != null) {
            float angle = Random.Range(0f, Mathf.PI * 2);
            float height = Random.Range(MIN_HEIGHT, MAX_HEIGHT);
            Vector3 spawnPos = new Vector3(Mathf.Cos(angle) * _spawnRadius, height, Mathf.Sin(angle) * _spawnRadius);

            Instantiate(_enemyPrefab, spawnPos, Quaternion.identity);
        }
    }
}
