using System.Collections;
using UnityEngine;

public class EnemyNoiseManager : MonoBehaviour
{
    public AudioClip[] enemyNoises;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayRandomNoise());
    }

    IEnumerator PlayRandomNoise()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(7f, 15f));

            int randomIndex = Random.Range(0, enemyNoises.Length);
            audioSource.clip = enemyNoises[randomIndex];
            audioSource.Play();

            yield return null;
        }
    }
}
