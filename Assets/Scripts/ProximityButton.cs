using UnityEngine;

public class ProximityButton: MonoBehaviour
{
    public float activationDistance = 3f; // Adjust the distance as needed
    public GameObject button; // Reference to your button

    private void Update()
    {
        // Looks if the player has the player tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);

            if (distance < activationDistance)
            {
                button.SetActive(true); // Activate the button
            }
            else
            {
                button.SetActive(false); // Deactivate the button
            }
        }
    }
}
