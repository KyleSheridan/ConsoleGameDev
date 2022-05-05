using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : State
{
    Transform wanderPoints;


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
        agent.playerLastPosition = Vector3.zero;
        agent.navMeshAgent.SetDestination(wanderPoints.position);
        //Play walk anim
        if (agent.navMeshAgent.remainingDistance <= agent.navMeshAgent.stoppingDistance)
        {
            if (agent.m_WaitTime <= 0)
            {
                NextWaypoint();
                agent.Move(agent.speedWalk);
                agent.m_WaitTime = agent.startWaitTime;
            }

            else
            {
                agent.Stop();
                agent.m_WaitTime -= Time.deltaTime;
            }
        }
    }

    public override void Exit()
    {
        Debug.Log("exiting wander state");
    }
    
    void Wander()
    {
        //pick a random waypoint to go
        agent.m_CurrentWaypointIndex = UnityEngine.Random.Range(0, agent.waypoints.Length - 1);
        wanderPoints = agent.waypoints[agent.m_CurrentWaypointIndex];
    }

    void NextWaypoint()
    {
        agent.m_CurrentWaypointIndex = UnityEngine.Random.Range(0, agent.waypoints.Length - 1);
        agent.navMeshAgent.SetDestination(agent.waypoints[agent.m_CurrentWaypointIndex].position);
    }


}
