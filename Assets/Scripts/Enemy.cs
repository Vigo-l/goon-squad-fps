using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent enemy;

    public float squareOfMovement = 50f;
    
    public float xMin;
    public float zMin;
    public float xMax;
    public float zMax;

    public float health = 100f;
    public int ekills;

    private float xPosition;
    private float yPosition;
    private float zPosition;

    public float closeEnough = 3f;
    

    // Start is called before the first frame update
    void Start()
    {
        xMin = zMin = -squareOfMovement;
        xMax = zMax = squareOfMovement;
        newLocation();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, new Vector3(xPosition, yPosition, zPosition)) <= closeEnough)
        {
            newLocation();
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
        Destroy(gameObject);
        ekills += 1;
    }
}
