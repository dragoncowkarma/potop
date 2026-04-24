using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate = 2f;
    public float spawnRadius = 25f;
    private float nextSpawnTime = 0f;

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefab != null)
        {
            float angle = Random.Range(0f, Mathf.PI * 2);
            float height = Random.Range(1f, 5f);
            Vector3 spawnPos = new Vector3(Mathf.Cos(angle) * spawnRadius, height, Mathf.Sin(angle) * spawnRadius);
            
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        }
    }
}
