using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    WeaponMaster weaponMaster;

    public Transform player;
    public NavMeshAgent agent;

    LayerMask groundMask;
    LayerMask playerMask;

    public float health;

    public float sightRange;
    public float attackRange;
    public bool playerInSightRange;
    public bool playerInAttackRange;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Char").transform;
        weaponMaster = GetComponent<WeaponMaster>();
    }

    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);

        if(playerInSightRange == false && playerInAttackRange == false)
        {
            Patrolling();
        }
        else if(playerInSightRange == true && playerInAttackRange == false)
        {
            ChasePlayer();
        }
        else if(playerInSightRange == true && playerInAttackRange == true)
        {
            AttackPlayer();
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundMask))
        {
            walkPointSet = true;
        }
    }

    void Patrolling()
    {
        if(walkPointSet == false)
        {
            SearchWalkPoint();
        }
        else
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        weaponMaster.Shoot();
    }
}
