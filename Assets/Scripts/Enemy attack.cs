using UnityEngine;

public class EnemyAttackScript : MonoBehaviour
{
    public Transform player;
    public float attackRange = 6f;

    bool foundPlayer;

    private Enemy enemyScript;
    public Material defaultMaterial;
    public Material alertMaterial;

    private Renderer ren;

    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyScript = GetComponent<Enemy>();
        ren = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            ChangeMaterial(alertMaterial);
            enemyScript.enemy.SetDestination(player.position);
            foundPlayer = true;
        }
        else if (foundPlayer)
        {
            ChangeMaterial(defaultMaterial);
            enemyScript.newLocation();
            foundPlayer = false;
        }
    }

    // Function to change material (and therefore texture)
    private void ChangeMaterial(Material newMaterial)
    {
        if (ren != null)
        {
            ren.material = newMaterial;
        }
    }
}
