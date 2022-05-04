using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    Transform player;

    float speedRun;
    float speedWalk;

    public ChaseState(Agent owner, Transform player) : base(owner)
    {
        this.player = player;
    }

    public override void Enter()
    {
        Debug.Log("entering Chase state");
    }

    public override void Execute()
    {
        agent.m_PlayerNear = false;
        agent.playerLastPosition = Vector3.zero;

        if (!agent.m_CaughtPlayer)
        {
            Move(speedRun);
            agent.navMeshAgent.SetDestination(agent.m_PlayerPosition);
        }

        if (agent.navMeshAgent.remainingDistance <= agent.navMeshAgent.stoppingDistance)
        {
            if (agent.m_WaitTime <= 0 && !agent.m_CaughtPlayer && Vector3.Distance(agent.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 6f)
            {
                agent.m_IsPatrol = true;
                agent.m_PlayerNear = false;
                Move(speedWalk);
                agent.m_TimeToRotate = agent.timeToRotate;
                agent.m_WaitTime = agent.startWaitTime;
                agent.navMeshAgent.SetDestination(agent.waypoints[agent.m_CurrentWaypointIndex].position);
            }

            else
            {
                if (Vector3.Distance(agent.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 2.5f)
                {
                    Stop();
                    agent.m_WaitTime -= Time.deltaTime;
                }
            }
        }

        //In the state the agent calculates the opposite direction of the player till the sensor hit is false
        Debug.Log("updating Chase state");
        agent.navMeshAgent.SetDestination(player.position);
        if (agent.navMeshAgent.remainingDistance <= agent.navMeshAgent.stoppingDistance)
        {
            if (agent.m_WaitTime <= 0)
            {
                //go to wander state
            }

            else
            {
                Stop();
                agent.m_WaitTime -= Time.deltaTime;
            }
        }

    }

    public override void Exit()
    {
        Debug.Log("exiting Flee state");
    }

    void Move(float speed)
    {
        agent.navMeshAgent.isStopped = false;
        agent.navMeshAgent.speed = speed;
    }

    void Stop()
    {
        agent.navMeshAgent.isStopped = true;
        agent.navMeshAgent.speed = 0;
    }
}
