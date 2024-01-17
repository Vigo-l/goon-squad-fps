using System.Collections;
using UnityEngine;

public class ScaryTV : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        StartCoroutine(TvOn());
    }

    // Coroutine for the pop-up behavior
    IEnumerator TvOn()
    {
        while (true)
        {
            spriteRenderer.enabled = true;


            yield return new WaitForSeconds(1f);


            spriteRenderer.enabled = false;

            yield return new WaitForSeconds(7f);
        }
    }
}
