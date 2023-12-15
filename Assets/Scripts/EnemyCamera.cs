using UnityEngine;

public class EnemyCamera : MonoBehaviour
{
    public Transform target; // The target to follow
    public float followSpeed = 5f; // The speed at which the camera follows the target

    void Update()
    {
        if (target != null)
        {
            // Get the target's position in the world space
            Vector3 targetPosition = target.position;

            // Keep the same Y position for the camera (top-down view)
            targetPosition.y = transform.position.y;

            // Move the camera towards the target based on the follow speed
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
}
