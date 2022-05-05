using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimManager : MonoBehaviour
{
    int randnum;
    public Animator animator { get; private set; }

    public AgentNav agentNav;

    public AgentChase agentChase;

    public AIManager aiMan;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        agentChase = GetComponent<AgentChase>();
        agentNav = GetComponent<AgentNav>();
    }

    // Update is called once per frame
    void Update()
    {
        SetParameters();

        int randnum = UnityEngine.Random.Range(1, 2);
    }

    private void SetParameters()
    {

        animator.SetBool("isWandering", agentNav.wandering);

        animator.SetBool("isChasing", agentChase.chasing);

        animator.SetBool("isAttacking1", agentChase.attacking);

        animator.SetBool("isDead", aiMan.dead);

    }
}
