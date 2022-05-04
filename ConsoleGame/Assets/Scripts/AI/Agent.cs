using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour
{
    public StateManager sm;

    public Transform[] waypoints;
    public int m_CurrentWaypointIndex;


    public Animator Anim;

    public GameObject audio;

    public NavMeshAgent navMeshAgent;

    public float startWaitTime = 4f;
    public float timeToRotate = 2f;
    public float speedWalk = 6f;
    public float speedRun = 9f;

    public float viewRadius = 15f;
    public float viewAngle = 90f;
    public LayerMask playerMask;
    public LayerMask obstacleMask;

    public Vector3 playerLastPosition = Vector3.zero;
    public Vector3 m_PlayerPosition;

    public float m_WaitTime;
    public float m_TimeToRotate;
    public bool m_PlayerInRange;
    public bool m_PlayerNear;
    public bool m_IsPatrol;
    public bool m_CaughtPlayer;

   

    //sensors s;

    //public StatsSystem statsys;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerPosition = Vector3.zero;
        navMeshAgent = GetComponent<NavMeshAgent>();

        m_IsPatrol = true;
        m_CaughtPlayer = false;
        m_WaitTime = startWaitTime;
        m_TimeToRotate = timeToRotate;
        m_PlayerInRange = false;

        m_CurrentWaypointIndex = 0;

        //Get references to scripts called
        sm = new StateManager();
        //set the default state to idle
        sm.changeState(new IdleState(this));

    }

    // Update is called once per frame
    void Update()
    {
        sm.Update();

        if(m_IsPatrol && (sm.currentState.GetType() != typeof(WanderState)))
        {
            sm.changeState(new WanderState(this));
        }

        else if(!m_IsPatrol && (sm.currentState.GetType() != typeof(ChaseState)))
        {
            //sm.changeState(new ChaseState(this));
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
                    m_PlayerInRange = true;
                    m_IsPatrol = false;
                }
                else
                {
                    m_PlayerInRange = false;
                }
            }
            if (Vector3.Distance(transform.position, player.position) > viewRadius)
            {
                m_PlayerInRange = false;
            }

            if (m_PlayerInRange)
            {
                m_PlayerPosition = player.transform.position;
            }
        }
    }
}
