using UnityEngine;

public class OrbSpawner : MonoBehaviour
{
    public GameObject orbPrefab;
    public float spawnRate = 2f;
    public Transform mazeBounds;

    private void Start()
    {
        // Start spawning orbs at a regular interval
        InvokeRepeating("SpawnOrb", 0f, spawnRate);
    }

    private void SpawnOrb()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(mazeBounds.position.x - mazeBounds.localScale.x / 2f, mazeBounds.position.x + mazeBounds.localScale.x / 2f),
            1f, // adjust the Y coordinate as needed
            Random.Range(mazeBounds.position.z - mazeBounds.localScale.z / 2f, mazeBounds.position.z + mazeBounds.localScale.z / 2f)
        );

        // Instantiate the orb at the calculated position
        Instantiate(orbPrefab, spawnPosition, Quaternion.identity);
    }
}
