using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentNav : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    public Transform[] waypoints;

    public bool wandering;


    public float walkSpeed;

    public float patrolTimer = 5f;//A variable for how long the object would be moving to the destination

    public float timerCount;//A variable for a timer to see how much time has passed since a new patrol started
    public int currentWaypointIndex;

    public void Awake()
    {
        //Gets the component of the nav agent attached the game object
        navMeshAgent = GetComponent<NavMeshAgent>();
        timerCount = patrolTimer;
    }

    private void Start()
    {
        Wander();
    }

    public void Wander()
    {
        wandering = true;
        navMeshAgent.speed = walkSpeed;
        navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
        if(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            if (timerCount <=0)
            {
                SetNewRandomDestination();
                navMeshAgent.speed = walkSpeed;
                timerCount = patrolTimer;
            }

            else
            {
                navMeshAgent.speed = 0;
                timerCount -= Time.deltaTime;
                wandering = false;
            }
        }
    }

    public void SetNewRandomDestination()
    {
        currentWaypointIndex = UnityEngine.Random.Range(0, waypoints.Length - 1);
        navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
    }
}
