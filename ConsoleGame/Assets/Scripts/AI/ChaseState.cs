using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    Transform player;

    public ChaseState(Agent owner, Transform player) : base(owner)
    {
        this.player = player;
    }

    public override void Enter()
    {
        Debug.Log("entering Chase state");
        agent.playerLastPosition = Vector3.zero;
    }

    public override void Execute()
    {
        Debug.Log("updating Chase state");



        if (!agent.m_CaughtPlayer)
        {
            Move(agent.speedRun);
            agent.navMeshAgent.SetDestination(agent.m_PlayerPosition);
            //Agent Animation Run
        }

        if (agent.navMeshAgent.remainingDistance <= agent.navMeshAgent.stoppingDistance)
        {
            if (agent.m_WaitTime <= 0 && !agent.m_CaughtPlayer && Vector3.Distance(agent.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 6f)
            {
                agent.m_IsPatrol = true;
                Move(agent.speedWalk);
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
                    agent.m_CaughtPlayer = true;
                }
            }
        }

    }

    public override void Exit()
    {
        Debug.Log("exiting Chase state");
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
