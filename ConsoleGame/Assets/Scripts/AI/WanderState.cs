using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderState : State
{
    Transform wanderPoints;

    bool moving = true;

    private Vector3 lastFrameLocation;

    public WanderState(Agent owner) : base(owner)
    {
    }



    public override void Enter()
    {
        Debug.Log("entering wander state");
        Wander();
    }

    public override void Execute()
    {
        agent.m_PlayerNear = false;
        agent.playerLastPosition = Vector3.zero;
        agent.navMeshAgent.SetDestination(agent.waypoints[agent.m_CurrentWaypointIndex].position);
        if (agent.navMeshAgent.remainingDistance <= agent.navMeshAgent.stoppingDistance)
        {
            if (agent.m_WaitTime <= 0)
            {
                agent.NextWaypoint();
                agent.Move(agent.speedWalk);
                agent.m_WaitTime = agent.startWaitTime;
            }

            else
            {
                agent.Stop();
                agent.m_WaitTime -= Time.deltaTime;
            }
        }
        /*Debug.Log("updating wander state");
        if (!moving)
        {
            Debug.Log(wanderPoints);
            agent.navMeshAgent.SetDestination(wanderPoints.transform.position);
            moving = true;
        }

        if((agent.transform.position - wanderPoints.position).magnitude < 2)
        {
            Wander();
        }

        if(Vector3.Distance(lastFrameLocation, agent.transform.position) == 0)
        {
            moving = false;
        }

        lastFrameLocation = agent.transform.position;*/
    }



    public override void Exit()
    {
        Debug.Log("exiting wander state");
    }
    
    void Wander()
    {
        //pick a random waypoint to go
        int nextWaypoint = UnityEngine.Random.Range(0, agent.waypoints.Length - 1);
        wanderPoints = agent.waypoints[nextWaypoint];
        moving = false;
    }


}
