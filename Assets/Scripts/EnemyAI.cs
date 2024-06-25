/*
 * Author: Alfred Kang Jing Rui
 * Date Created: 22/06/2024
 * Date Modified: 25/06/2024
 * Description: Script Attached to Enemies for NavMesh and Health
 */

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    private Transform player;

    public LayerMask IsGround, IsPlayer;

    public float maxHealth;
    private float health;
    [SerializeField] private Image healthbar;
    [SerializeField] private GameObject healthBarCanvas;
    private Camera cam;
    private Animator animator;

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
        if (TryGetComponent(out Flying flying))
        {
            agent.baseOffset = 1.2f;
        }

        cam = Camera.main;
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        health = maxHealth;
    }

    private void Update()
    {
        if (health >= 0)
        {
            //Check if Player is in sight/attack range
            playerInSight = Physics.CheckSphere(transform.position, sightRange, IsPlayer);
            playerInAttack = Physics.CheckSphere(transform.position, attackRange, IsPlayer);

            //Change Enemy State 
            if (!playerInSight && !playerInAttack) Patrol();
            if (playerInSight && !playerInAttack) Chase();
            if (!playerInSight && playerInAttack) Attack();
        }

        //Rotate Healthbar
        healthBarCanvas.transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
    }

    public void UpdateHealthBar(float health, float maxHealth)
    {
        healthbar.fillAmount = health / maxHealth;
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
            animator.Play("Attack");
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
        animator.Play("GetHit");
        health -= damage;
        UpdateHealthBar(health, maxHealth);

        if (health <= 0 )
        {
            agent.SetDestination(transform.position);
            Destroy(GetComponent<SphereCollider>());
            animator.Play("Die");
            Invoke(nameof(DestroyEnemy), 1f);
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
