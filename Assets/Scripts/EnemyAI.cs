using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    private Transform player;

    public LayerMask IsGround, IsPlayer;

    public float health;

    //Patrol
    private Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attack
    public float timeBetweenAttacks;
    bool attacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSight, playerInAttack;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Check if Player is in sight/attack range
        playerInSight = Physics.CheckSphere(transform.position, sightRange, IsPlayer);
        playerInAttack = Physics.CheckSphere(transform.position, attackRange, IsPlayer);

        //Change Enemy State 
        if (!playerInSight && !playerInAttack) Patrol();
        if (playerInSight && !playerInAttack) Chase();
        if (!playerInSight && playerInAttack) Attack();
    }

    private void Patrol()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        } else
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        //Check that random point is ON the map
        if (Physics.Raycast(walkPoint, -transform.up, 2f, IsGround))
        {
            walkPointSet = true;
        }
    }

    private void Chase()
    {
        agent.SetDestination(player.position);
        transform.LookAt(player);
    }

    private void Attack()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!attacked)
        {
            //insert atk code

            attacked = true;
            Invoke(nameof(ResetAtk), timeBetweenAttacks);
        }
    }

    private void ResetAtk()
    {
        attacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0 )
        {
            Invoke(nameof(DestroyEnemy), 2f);
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
