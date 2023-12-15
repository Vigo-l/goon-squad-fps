using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    private void Update()
    {
    }


    void YourMethod()
    {
        callanimator.SetBool("donecall", true);
        Destroy(gameObject);
    }

}
