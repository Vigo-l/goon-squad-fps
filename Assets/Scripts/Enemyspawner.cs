using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemyspawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int enemyCount;
    void Start()
    {
       for (int i  = 0 ; i < enemyCount; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        }

    }

    // Update is called once per frame
}
