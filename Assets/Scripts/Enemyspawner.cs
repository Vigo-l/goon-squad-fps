using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;  // The enemy prefab to spawn
    public int numberOfEnemies = 10; // Number of enemies to spawn
    public float spawnDistance = 5f; // Distance between each enemy
    public float spawnDelay = 1f;    // Delay between spawns

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = transform.position + Random.onUnitSphere * spawnDistance;
        spawnPosition.y = 0f; // Ensure enemies spawn at the same height (adjust as needed)

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
