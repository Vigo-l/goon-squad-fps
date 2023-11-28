using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Call : MonoBehaviour
{
    public AudioClip audioClip;
    Animator callanimator;
    void Start()
    {
        // Play the audio clip
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();
        callanimator = GameObject.FindGameObjectWithTag("telegoon").GetComponent<Animator>();

        // Wait for 11 seconds and then call a method
        Invoke("YourMethod", 11f);
    }

    void YourMethod()
    {
        callanimator.SetBool("donecall", true);
        Destroy(gameObject);
    }

}
