using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent enemy;
    public GameObject player;

    public float squareOfMovement = 50f;

    public float xMin;
    public float zMin;
    public float xMax;
    public float zMax;

    public GameObject door;

    public int damage = 10;

    public float health = 100f;
    public Gun gun;

    private float xPosition;
    private float yPosition;
    private float zPosition;

    public float closeEnough = 3f;


    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (player == null)
        {
            Debug.LogError("Player not found!");
        }
    }

    void Update()
    {
        if (player != null)
        {
            // Check if the enemy is close enough to the player
            if (Vector3.Distance(transform.position, player.transform.position) > closeEnough)
            {
                // Update the destination to the player's position
                enemy.SetDestination(player.transform.position);
            }
        }
    }

    public void newLocation()
    {
        xPosition = Random.Range(xMin, xMax);
        yPosition = transform.position.y;
        zPosition = Random.Range(zMin, zMax);
        enemy.SetDestination(new Vector3(xPosition, yPosition, zPosition));
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        gun.enemyCount++;
        Destroy(gameObject);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
    }

}