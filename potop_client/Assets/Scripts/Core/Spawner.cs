using UnityEngine;
using UnityEngine.Serialization;

public class Spawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [FormerlySerializedAs("enemyPrefab")]
    public GameObject prefabToSpawn;
    public float minSpawnRate = 2f;
    public float maxSpawnRate = 5f;
    public float spawnRadius = 10f;
    public float spawnHeight = 10f;

    private float nextSpawnTime = 0f;

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnObject();
            nextSpawnTime = Time.time + Random.Range(minSpawnRate, maxSpawnRate);
        }
    }

    void SpawnObject()
    {
        if (prefabToSpawn != null)
        {
            float angle = Random.Range(0f, Mathf.PI * 2);
            Vector3 spawnPos = new Vector3(Mathf.Cos(angle) * spawnRadius, spawnHeight, Mathf.Sin(angle) * spawnRadius);

            Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
        }
    }
}
