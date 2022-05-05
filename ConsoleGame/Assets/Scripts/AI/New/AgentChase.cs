using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentChase : MonoBehaviour
{
    public GameObject player;

    public AgentNav agentNav;

    public float attackDistance = 2f;

    public float viewRadius = 15f;
    public float viewAngle = 90f;
    public LayerMask playerMask;
    public LayerMask obstacleMask;

    Vector3 playerPosition;

    bool playerInRange;
    public bool m_IsPatrol;

    public float chaseDistance;

    private NavMeshAgent navMeshAgent;

    public bool chasing;
    public bool attacking;



    public int runSpeed;
    private bool m_CaughtPlayer;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        agentNav = GetComponent<AgentNav>();
        m_IsPatrol = true;
    }

    // Update is called once per frame
    void Update()
    {
        EnvironmentView(); 
        if (m_IsPatrol)
        {
            agentNav.Wander();
        }

        else
        {
            Chase();
        }
    }



    private void Chase()
    {
        if (!m_CaughtPlayer)
        {
            chasing = true;
            navMeshAgent.speed = runSpeed;
            navMeshAgent.SetDestination(playerPosition);
        }

        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            chasing = false;
            attacking = true;
            //Attack
            //Functionality
        }

    }

    

    void EnvironmentView()
    {
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);

        for (int i = 0; i < playerInRange.Length; i++)
        {
            Transform player = playerInRange[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
            {
                float disToPlayer = Vector3.Distance(transform.position, player.position);
                if (!Physics.Raycast(transform.position, dirToPlayer, disToPlayer, obstacleMask))
                {
                    this.playerInRange = true;
                    m_IsPatrol = false;
                }
                else
                {
                    this.playerInRange = false;
                }
            }
            if (Vector3.Distance(transform.position, player.position) > viewRadius)
            {
                this.playerInRange = false;
            }

            if (this.playerInRange)
            {
                playerPosition = player.transform.position;
            }
        }
    }

}
