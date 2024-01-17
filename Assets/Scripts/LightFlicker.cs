using UnityEngine;
using System.Collections;

public class FlickerScript : MonoBehaviour
{
    public Light flickeringLight;
    public SpriteRenderer flickeringSpriteRenderer;
    public float minFlickerInterval = 5f;
    public float maxFlickerInterval = 10f;
    public float flickerDuration = 0.5f;
    public float minOpacity = 0.5f;
    public float maxOpacity = 1f;

    private bool isFlickering = false;

    void Start()
    {
        // Start the flickering coroutine
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minFlickerInterval, maxFlickerInterval));

            // Make sure we are not already flickering
            if (!isFlickering)
            {
                isFlickering = true;

                // Set the opacity to a random value
                float randomOpacity = Random.Range(minOpacity, maxOpacity);
                flickeringLight.intensity = randomOpacity;
                flickeringSpriteRenderer.color = new Color(1f, 1f, 1f, randomOpacity);

                // Wait for the flicker duration
                yield return new WaitForSeconds(flickerDuration);

                // Reset the opacity
                flickeringLight.intensity = maxOpacity;
                flickeringSpriteRenderer.color = new Color(1f, 1f, 1f, maxOpacity);

                isFlickering = false;
            }
        }
    }
}
