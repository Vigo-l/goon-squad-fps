using UnityEngine;

public class OrbController : MonoBehaviour
{
    public Gun gun;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gun.orbCount++;
            Destroy(gameObject);
        }
    }
}
