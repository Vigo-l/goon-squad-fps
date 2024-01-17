using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class Call : MonoBehaviour
{
    public AudioClip audioClip;
    Animator callAnimator;

    void Start()
    {

        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();
        callAnimator = GameObject.FindGameObjectWithTag("telegoon").GetComponent<Animator>();


        Invoke("Done", 6f);
    }

    private void Update()
    {
    }

    void Done()
    {

        callAnimator.SetBool("donecall", true);
        Destroy(gameObject);

    }
}

